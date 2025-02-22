using AgricultureManager.Module.Accounting.Domain;
using libfintx.FinTS;
using libfintx.FinTS.Camt;
using libfintx.FinTS.Data;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data;

namespace AgricultureManager.Module.Accounting.Services
{
    public static class FinTsClientFactory
    {
        public static IFinTsClient Create(ConnectionDetails connectionDetails)
            => new FinTsClient(connectionDetails);
    }

    public class FinTsClientService(ILogger<FinTsClientService> logger)
    {

        private async Task<bool> Synchronize(IFinTsClient client)
        {
            var result = await client.Synchronization();
            return result.IsSuccess;
        }

        public async Task<decimal?> GetAccountBalance(IFinTsClient client)
        {
            if (await Synchronize(client))
            {
                var balance = await client.Balance(GetTanDialog());
                return balance.Data.Balance;
            }
            return null;
        }

        public async Task<ICollection<CamtTransaction>> GetTransactions(IFinTsClient client, DateTime? startDate, DateTime? endDate)
        {
            if (await Synchronize(client))
            {
                var transactions = await client.Transactions_camt(GetTanDialog(), CamtVersion.Camt052, startDate, endDate);
                if (transactions.IsSuccess && transactions.Data is not null)
                {
                    return transactions.Data.SelectMany(s => s.Transactions).ToList();
                }
            }
            return default!;
        }


        private TANDialog GetTanDialog()
        {
            var dialog = new TANDialog(WaitForTaskAsync);
            return dialog;
        }

        private async Task<string> WaitForTaskAsync(TANDialog dialog)
        {
            throw new NotImplementedException();
        }
    }
}
