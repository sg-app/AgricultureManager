﻿using AgricultureManager.Core.Application.Shared.Extensions;
using AgricultureManager.Module.Accounting.Domain;
using AgricultureManager.Module.Accounting.Features.AccountFeatures;
using AgricultureManager.Module.Accounting.Features.BankingFeatures;
using AgricultureManager.Module.Accounting.Features.BookingFeatures;
using AgricultureManager.Module.Accounting.Features.BookingTypeFeatures;
using AgricultureManager.Module.Accounting.Features.TaxRateFeatures;
using AgricultureManager.Module.Accounting.Models;
using AutoMapper;
using Microsoft.AspNetCore.Server.HttpSys;

namespace AgricultureManager.Module.Accounting.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TaxRate, TaxRateVm>();
            CreateMap<TaxRateVm, TaxRateVm>();
            CreateMap<TaxRateVm, AddTaxRateCommand>();
            CreateMap<TaxRateVm, UpdateTaxRateCommand>();
            CreateMap<AddTaxRateCommand, TaxRate>();
            CreateMap<UpdateTaxRateCommand, TaxRate>();

            CreateMap<BookingType, BookingTypeVm>();
            CreateMap<BookingTypeVm, BookingTypeVm>();
            CreateMap<BookingTypeVm, AddBookingTypeCommand>();
            CreateMap<BookingTypeVm, UpdateBookingTypeCommand>();
            CreateMap<AddBookingTypeCommand, BookingType>();
            CreateMap<UpdateBookingTypeCommand, BookingType>();

            CreateMap<Account, AccountVm>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.Decrypt()));
            CreateMap<AccountVm, AccountVm>();
            CreateMap<AccountVm, AddAccountCommand>();
            CreateMap<AccountVm, UpdateAccountCommand>();
            CreateMap<AddAccountCommand, Account>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.Encrypt()));
            CreateMap<UpdateAccountCommand, Account>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password.Encrypt()));


            CreateMap<AccountMouvement, AccountMouvementVm>();

            CreateMap<Document, DocumentVm>();

            CreateMap<Booking, BookingVm>();
            CreateMap<BookingVm, BookingVm>();
            CreateMap<BookingVm, AddBookingCommand>();
            CreateMap<BookingVm, UpdateBookingCommand>();
            CreateMap<AddBookingCommand, Booking>();
            CreateMap<UpdateBookingCommand, Booking>();

            CreateMap<StatementOfAccountDocument, StatementOfAccountDocumentVm>();
            CreateMap<AccountVm, GetMouvementsFromAccountCommand>();

            CreateMap<CostType, CostTypeVm>();
        }
    }
}
