import { Injectable, Injector } from '@angular/core';
import { ToastController } from 'ionic-angular';
import { AppConfigurationServiceProvider } from '../../providers/app.configuration.service';
import { ApiServicesHandlerService } from './app.constant';
import { LanguageService } from '../interfaces/ILanguage';
import { Observable } from 'rxjs';
import { LanguageServiceProvider } from '../../providers/_services/language.service';
import { tap } from 'rxjs/operators';
import { CustomValidationHandlerService } from './customvalidation.service';

/*
  Generated class for the LanguageHandlerService provider.
*/
export abstract class LanguageHandlerService {
    /**
     * App configuration service provider injector
     */
    _app_config_service: AppConfigurationServiceProvider;
   
    /**
     * Custom validation message heandler service DI
     */
    protected _custom_message_handler: CustomValidationHandlerService;
    
     /**
     * Language service provider DI
     */
    _language_service_provider: LanguageServiceProvider;

    /**
    Value of the LanguageService which contain selected language with all static label contents and all active languages
    */
    _lang_service: LanguageService = {
        languages: [],
        currentLanguage: Object.assign({}),
        currentLangContents: []
    };

    /**
   *@ignore 
   */
    constructor(injector: Injector) {
        this._app_config_service = injector.get(AppConfigurationServiceProvider);
        this._language_service_provider = injector.get(LanguageServiceProvider);
        this._custom_message_handler = injector.get(CustomValidationHandlerService);
    }

    /**
     * To get specific content based on selected language key
     * @param {string} key 
     */
    lang(key) {
        this._lang_service = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.activeLanguageInfo));
        if (this._lang_service && this._lang_service.currentLangContents != null) {
            if (this._lang_service.currentLangContents[key])
                return this._lang_service.currentLangContents[key];
            else
                return key;
        }
        else
            return key;
    }

    /**
     * To validate only number
     * @example
     * onlyNumber(eventInfo)
     * @param {object} event
     * @returns {boolean} True | False
     */
    onlyNumber(event) {
        return this._custom_message_handler.validateOnlyNumber(event);
    }
}
