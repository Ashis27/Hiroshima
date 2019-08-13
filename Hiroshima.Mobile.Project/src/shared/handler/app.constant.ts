import { Injectable } from '@angular/core';
import { IServiceList } from '../interfaces/IServiceList';
import { IAppConfiguration } from '../interfaces/IAppConfiguration';
import { ILanguageInfo } from '../interfaces/ILanguage';

/*
  Generated class for the ApiServicesHandlerService handler.
*/
export abstract class ApiServicesHandlerService {

  /**
   Value of the app name
   */
    static readonly _app_name: string = "HiroshimaQRApp";

      /**
   Value of the app version
   */
    static readonly _app_version:string="1.0.0"
/**
  Value of the current payment API URL
 */
private _paymentAPIURL:string;

      /**
   Value of all API services
   */
    static readonly _api_services = {

        //Configuration services
        languageContent: { controller: "Configuration", method: "GetLanguageContent", api: "api/Configuration/LanguageContent" },
        activeLanguage: { controller: "Configuration", method: "GetActiveLanguages", api: "api/Configuration/ActiveLanguages" },
        getActiveCurrency: { controller: "Configuration", method: "GetActiveCurriencies", api: "api/Configuration/ActiveCurriencies" },
        getBookingIngo: { controller: "Configuration", method: "GetBookingInformation", api: "api/Configuration/BookingInformation" },
        getAppConfiguration: { controller: "Configuration", method: "GetAppConfiguration", api: "api/Configuration/AppConfiguration" },
       
        //Pass services
        getPasses: { controller: "Pass", method: "GetAllActivePasses", api: "api/Pass/GetAllPass" },
        getPass: { controller: "Pass", method: "GetActivePass", api: "api/Pass/GetPass" },
        getPassLanguages: { controller: "Pass", method: "GetAvailableLanguages", api: "api/Pass/PassInformation" },

        //User services
        getAvailablePasses: { controller: "Traveller", method: "GetAvailablePass", api: "api/Traveller/GetAvailablePass" },
        getInUsePasses: { controller: "Traveller", method: "GetInUsePasses", api: "api/Traveller/GetInUsePass" },
        getExpiredPasses: { controller: "Traveller", method: "GetExpiredPasses", api: "api/Traveller/GetExpiredPass" },
        activatePass: { controller: "Traveller", method: "ActivatePass", api: "api/Traveller/ActivatePass" },
        bookPass: { controller: "Traveller", method: "BookPass", api: "api/Traveller/BookingInformation" },
        submitFeedback: { controller: "Traveller", method: "SubmitFeedback", api: "api/Traveller/TravellerFeedback" },
        getbookingInfoById: { controller: "Traveller", method: "GetBookingInformation", api: "api/Traveller/BookingInfo" },
      
    };

      /**
   Value of app confogurations
   */
    static readonly _app_config: Array<IAppConfiguration> = [
        {
            envName: "LOCAL",
            apiServices:
                {
                    remoteServiceBaseUrl: "http://localhost:21021/",
                },
            version: "1.0.0",
            paymentAPIURL:"http://localhost:63321/"
        },
        {
            envName: "UAT",
            apiServices:
                {
                    remoteServiceBaseUrl: "http://54.84.255.41:8080/",
                },
            version: "1.0.0",
            paymentAPIURL:"http://54.84.255.41:8097/"
        },
        {
            envName: "STAG",
            apiServices:
                {
                    remoteServiceBaseUrl: "http://kare4u-lb-bpms-ind-prod2-consume-2031991078.us-east-1.elb.amazonaws.com:8120/",
                },
            version: "1.0.0",
            paymentAPIURL:"http://localhost:63321/"
        },
        {
            envName: "PROD",
            apiServices:
                {
                    remoteServiceBaseUrl: "http://kare4u-lb-bpms-ind-prod2-consume-2031991078.us-east-1.elb.amazonaws.com:8120/",
                },
            version: "1.0.0",
            paymentAPIURL:"http://localhost:63321/"
        }
    ];

    /**
     Value of the cache keys
     */
    static readonly _cache_key_value = {
        appAuthTokenName: 'app_auth_token',
        activeLanguageInfo: 'active_lang_info',
        activeCurriencies: 'active_currencies',
        loggedInUser: 'logged_in_user_info',
        userDeviceId: 'user_device_id',
        defaultLanguage: 'isLanguageSelected',
        appConfiguration: 'app_configuration',
    };

    /**
     Value of default language
     */
    static readonly _default_lang: ILanguageInfo = {
        id: 1,
        name: "English",
        displayName: "en",
        icon: "en.svg"
    }

}