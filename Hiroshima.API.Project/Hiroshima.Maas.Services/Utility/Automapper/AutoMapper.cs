using AutoMapper;
using Hiroshima.Maas.DL.Entities.AdminUserModel;
using Hiroshima.Maas.DL.Entities.AppConfigurationModel;
using Hiroshima.Maas.DL.Entities.CurrencyConfigurationModel;
using Hiroshima.Maas.DL.Entities.LanguageConfigurationModel;
using Hiroshima.Maas.DL.Entities.PassInformationModel;
using Hiroshima.Maas.DL.Entities.PGTransactionInformationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using Hiroshima.Maas.DL.Entities.PTOInformationModel;
using Hiroshima.Maas.DL.Entities.QRCodeConfigModel;
using Hiroshima.Maas.DL.Entities.QRCodeModel;
using Hiroshima.Maas.DL.Entities.TravellerFeedbackModel;
using Hiroshima.Maas.DL.Entities.TravellerModel;
using Hiroshima.Maas.DL.Utility;
using Hiroshima.Maas.Services.Utility.Helper;
using Hiroshima.Maas.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.Common.Infrastructure.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Admin_User
            CreateMap<AdminUser, AdminUserViewModel>();
            CreateMap<AdminUserViewModel, AdminUser>();
            #endregion

            #region Pass_Information
            CreateMap<PassInformation, PassInformationViewModel>();
            CreateMap<PassInformationViewModel, PassInformation>();
            CreateMap<PassDescription, PassDescriptionViewModel>();
            CreateMap<PassDescriptionViewModel, PassDescription>();
            CreateMap<PaginatedList<PassInformation>, PagedViewModelResult<PassInformationViewModel>>();
            CreateMap<PagedViewModelResult<PassInformationViewModel>, PaginatedList<PassInformation>>();
            #endregion

            #region PTO_Information
            CreateMap<PTOInformation, PTOInformationViewModel>();
            CreateMap<PTOInformationViewModel, PTOInformation>();
            CreateMap<PTODescriptionViewModel, PTODescription>();
            CreateMap<PTODescription, PTODescriptionViewModel>();
            CreateMap<PaginatedList<PTOInformation>, PagedViewModelResult<PTOInformationViewModel>>();
            CreateMap<PagedViewModelResult<PTOInformationViewModel>, PaginatedList<PTOInformation>>();

            #endregion

            #region PTO_Description
            //CreateMap<PTODescription, PTODescriptionViewModel>();
            CreateMap<PTODescriptionViewModel, PTODescription>();
            #endregion

            #region Pass_PTO_Mapper
            CreateMap<PassActivePTOViewModel, PassActivePTO>();
            CreateMap<PassActivePTO, PassActivePTOViewModel>();
            #endregion

            #region Traveller_Pass_Information
            CreateMap<PaginatedList<FlattenedPassInformation>, PagedViewModelResult<FlattenedPassInformationViewModel>>();
            CreateMap<PagedViewModelResult<FlattenedPassInformationViewModel>, PaginatedList<FlattenedPassInformation>>();
            CreateMap<BookedPassInformation, BookedPassInformationViewModel>();
            #endregion

            #region Traveller_Feedback
            CreateMap<TravellerFeedbackViewModel, TravellerFeedback>();
            CreateMap<TravellerFeedback, TravellerFeedbackViewModel>().ForMember(dest => dest.DeviceId, mo => mo.Ignore());
            CreateMap<PaginatedList<TravellerFeedback>, PagedViewModelResult<TravellerFeedbackViewModel>>();
            CreateMap<PagedViewModelResult<TravellerFeedbackViewModel>, PaginatedList<TravellerFeedback>>();
            #endregion

            #region Search_Params
            CreateMap<SearchParams, PassInfoSearchParams>();
            #endregion

            #region App_Configuration
            CreateMap<AppConfiguration, AppConfigurationViewModel>();
            #endregion

            #region Language
            CreateMap<Language, LanguageViewModel>();
            CreateMap<PaginatedList<Language>, PagedViewModelResult<LanguageViewModel>>();
            CreateMap<PagedViewModelResult<LanguageViewModel>, PaginatedList<Language>>();
            #endregion

            #region Curriency
            CreateMap<CurrencyConfiguration, CurrencyViewModel>();
            CreateMap<PaginatedList<CurrencyConfiguration>, PagedViewModelResult<CurrencyViewModel>>();
            CreateMap<PagedViewModelResult<CurrencyViewModel>, PaginatedList<CurrencyConfiguration>>();
            #endregion

            #region Booking_Information
            CreateMap<BookedPassInformation, BookedPassInformationViewModel>();
            CreateMap<BookedPassInformationViewModel, BookedPassInformation>();
            CreateMap<PaginatedList<BookedPassInformation>, PagedViewModelResult<BookedPassInformationViewModel>>();
            CreateMap<PagedViewModelResult<BookedPassInformationViewModel>, PaginatedList<BookedPassInformation>>();
            #endregion

            #region QR_Code
            CreateMap<QRCodeViewModel, QRCode>();
            CreateMap<QRCode, QRCodeViewModel>();
            #endregion

            #region QR_Code_Configuration
            CreateMap<QRCodeConfiguration, QRCodeConfigurationViewModel>();
            CreateMap<QRCodeConfigurationViewModel, QRCodeConfiguration>();
            #endregion

            #region Add_PG_Transation_information
            CreateMap<PGTransactionInformationViewModel, PGTransactionInformation>();
            CreateMap<PGTransactionInformation, PGTransactionInformationViewModel>();
            #endregion

        }
    }
}
