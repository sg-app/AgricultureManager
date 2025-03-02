using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Persistence;
using AgricultureManager.SharedComponents.Dialogs;
using libfintx.FinTS;
using libfintx.FinTS.Camt;
using libfintx.FinTS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Radzen;

namespace AgricultureManager.Module.Accounting.Features.BankingFeatures
{
    public record GetMouvementsFromAccountCommand : IReq
    {
        public Guid Id { get; set; }
        public string AccountHolder { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public int Blz { get; set; }
        public string Bic { get; set; } = string.Empty;
        public string Iban { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int TanProcess { get; set; } = 944;
    }

    public class GetMouvementsFromAccountCommandHandler(ILogger<GetMouvementsFromAccountCommandHandler> logger, IAccountingDbContextFactory contextFactory, DialogService dialogService) : IReqHandler<GetMouvementsFromAccountCommand>
    {
        public async Task<ResponseLess> Handle(GetMouvementsFromAccountCommand request, CancellationToken cancellationToken)
        {
            
            //var config = await context.Configuration.FirstOrDefaultAsync(cancellationToken);
            //if (config == null)
            //{
            //    logger.LogError($"Configuration from banking context not loaded!");
            //    return Response.Fail("Konfiguration nicht geladen.");
            //}

            var connectionDetails = new ConnectionDetails()
            {
                AccountHolder = request.AccountHolder,
                Account = request.AccountNumber,
                Blz = request.Blz,
                BlzHeadquarter = null,
                Bic = request.Bic,
                Iban = request.Iban,
                Url = request.Url,
                UserId = request.Username,
                Pin = request.Password
            };
            var client = new FinTsClient(connectionDetails)
            {
                HIRMS = request.TanProcess.ToString()
            };

            var sync = await client.Synchronization();

            HBCIOutputLog("sync",sync.Messages);

            if(sync.Messages.Any(m=>m.Code == "9942"))
                return Response.Fail("Anmeldedaten sind ungültig.");

            if (sync.IsSuccess)
            {
                // Setup start and end date.
                var startDate = request.StartDate;
                if (startDate == null || startDate < DateTime.UtcNow.AddDays(-90).Date)
                { startDate = DateTime.UtcNow.AddDays(-90).Date; }

                DateTime? endDate = DateTime.UtcNow.AddDays(-1).Date;
                logger.LogDebug("Bankstatement collection from {startDate} to {endDate}", startDate, endDate);

                // Exit when data are actualy.
                if (startDate == endDate)
                {
                    logger.LogInformation($"Banking statement was already loaded. Please try again tomorrow.");
                    return Response.Fail("Start und Enddatum dürfen nicht gleich sein.");
                }


                var dialog = new TANDialog(WaitForTanAsync);

                // Send transaction.
                var transactions = await client.Transactions_camt(dialog, CamtVersion.Camt052, startDate, endDate);

                HBCIOutputLog("transactions",transactions.Messages);

                if (transactions.IsSuccess && transactions.Data is not null)
                {
                    using var context = contextFactory.CreateDbContext();
                    foreach (var item in transactions.Data)
                    {
                        EntityEntry<AccountMouvement> entry;
                        foreach (var mouvement in item.Transactions)
                        {
                            // Save received data to Database.
                            entry = await context.AccountMouvement.AddAsync(new AccountMouvement
                            {
                                Id = Guid.NewGuid(),
                                AccountId = request.Id,
                                InputDate = mouvement.InputDate,
                                ValueDate = mouvement.ValueDate,
                                TransactionTypeId = mouvement.TransactionTypeId,
                                TypeCode = mouvement.TypeCode,
                                PartnerName = mouvement.PartnerName,
                                Description = mouvement.Description,
                                Amount = mouvement.Amount,
                                Text = mouvement.Text,
                                Pending = mouvement.Pending,
                                Primanota = mouvement.Primanota,
                                TextKeyAddition = mouvement.TextKeyAddition,
                                BankCode = mouvement.BankCode,
                                AccountCode = mouvement.AccountCode,
                                EndToEndId = mouvement.EndToEndId,
                                MandateId = mouvement.MandateId,
                                ProprietaryRef = mouvement.ProprietaryRef,
                                CustomerRef = mouvement.CustomerRef,
                                PaymentInformationId = mouvement.PaymentInformationId,
                                MessageId = mouvement.MessageId,
                                Storno = mouvement.Storno
                            }, cancellationToken);
                            try
                            {
                                var count = await context.SaveChangesAsync(cancellationToken);
                                logger.LogDebug("{count} rows added to context.", count);
                            }
                            catch (Exception ex)
                            {
                                entry.State = EntityState.Unchanged;
                                logger.LogError(ex, "Update not possible. {message}", ex.Message);
                            }
                        }
                    }
                    //config.BankStatementEndDate = endDate;
                    var account = await context.Account.FindAsync([request.Id], cancellationToken);
                    if (account != null)
                    {
                        account.LatestSynchronisation = DateTime.UtcNow;
                        context.Account.Update(account);
                    }
                    var countCfg = await context.SaveChangesAsync(cancellationToken);
                    logger.LogDebug("{countCfg} Configuration updated.", countCfg);
                }
            }
            return Response.Success();
        }

        private async Task<string> WaitForTanAsync(TANDialog dialog)
        {
            var dialogResult = await dialogService.OpenAsync<TextBoxDialog>("TAN eingabe", new Dictionary<string, object> { { "Title", "Bitte TAN eingeben:" } });

            if (!string.IsNullOrWhiteSpace(dialogResult))
            {
                return dialogResult;
            }
            return string.Empty;
        }


        /// <summary>
        /// HBCI-Nachricht loggen
        /// </summary>
        /// <param name="hbcimsg"></param>
        private void HBCIOutputLog(string step, IEnumerable<HBCIBankMessage> hbcimsg)
        {
            foreach (var msg in hbcimsg)
            {
                logger.LogDebug("Step: [{step}] | Code: {code} | Typ: {type} | Nachricht: {message}",step, msg.Code, msg.Type, msg.Message);
            }
        }
    }
}
