import { Injectable, Injector, OnInit } from '@angular/core';
import { ApiServicesHandlerService } from './app.constant';
import { LanguageService } from '../interfaces/ILanguage';
import { Observable } from 'rxjs';
import { AppConfigurationServiceProvider } from 'src/app/_services/app.configuration.service';
import { map, catchError } from 'rxjs/operators';
import { AuthenticationService } from 'src/app/_services';
import { CustomValidationHandlerService } from 'src/app/shared/handler/customvalidation.service';

/*
  Generated class for the LanguageHandlerService provider.
*/
@Injectable({ providedIn: 'root' })
export abstract class LanguageHandlerService {
        /**
     * Authentication service provider injector
     */
    protected _authenticationService: AuthenticationService;
        /**
     * Custom validation service provider injector
     */
    protected _custom_message_handler: CustomValidationHandlerService;

  /**
    Value of the LanguageService which contain selected language with all static label contents and all active languages
    */
    _lang_service: LanguageService = {
        languages: [],
        currentLanguage: Object.assign({}),
        currentLangContents: null
    };
  /**
   *@ignore 
   */
    constructor(injector: Injector) {
        this._authenticationService = injector.get(AuthenticationService);
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
