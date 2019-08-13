import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { tap } from 'rxjs/operators';
import { HttpServiceProvider } from '../http.services';
import { AppConfigurationServiceProvider } from '../app.configuration.service';
import { SearchParams } from '../../shared/models';

/*
  Generated class for the login.service provider.
*/
@Injectable()
export class LanguageServiceProvider {

    /**
     * @ignore
     */
    constructor(public _http: HttpServiceProvider, private _app_config_service: AppConfigurationServiceProvider) {

    }

    /**
 * Get active languages
 * @example
 * GetActiveLanguages(searchParams,false)
 * @param {SearchParams} searchParams pagination details
 * @param {boolean} status True | False
 * @returns {List} language information
 */
    GetActiveLanguages = (searchParams: SearchParams, status: boolean): Observable<any> => {
        return this._http.post(this._app_config_service.getApiService("activeLanguage").toString() + "/" + searchParams, searchParams, status)
            .pipe(tap(response => {
                return response;
            }));
    }

/**
 * Get active languages
 * @example
 * GetActiveLanguageContents(en,false)
 * @param {string} lang language name
 * @param {boolean} status True | False
 * @returns {object} selected langiage contents
 */
    GetActiveLanguageContents = (lang: string, status: boolean): Observable<any> => {
        return this._http.get(this._app_config_service.getApiService("languageContent").toString() + "/OnLanguage/" + lang, status)
            .pipe(tap(response => {
                return response;
            }));
    }

}
