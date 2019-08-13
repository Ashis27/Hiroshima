import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { HttpServiceProvider } from '../http.services';
import { AppConfigurationServiceProvider } from '../app.configuration.service';
import { SearchParams } from '../../shared/models';

/*
  Generated class for the admin.service provider.
*/
@Injectable()
export class PassServiceProvider {

    /**
    *@ignore
    */
    constructor(private _http: HttpServiceProvider, private _app_config_service: AppConfigurationServiceProvider) {

    }

    /**
* Get active passes
* @example
* GetPasses(searchParams,false)
* @param {SearchParams} searchParams pagination details
* @param {boolean} status True | False
* @returns {List} pass information
*/
    GetPasses = (searchParams: SearchParams, status: boolean): Observable<any> => {
        searchParams.lang = this._app_config_service.getSelectedLangFromStorage();
        return this._http.post(this._app_config_service.getApiService("getPasses").toString() + "/" + searchParams, searchParams, status)
            .pipe(tap(response => {
                return response;
            }));
    }

    /**
* To get selected pass information by indivisual pass id
* @example
* GetPass(en,1)
* @param {number} lang language id
* @param {number} id pass id
* @returns {PassModel} pass information
*/
      GetPass = (lang: number, id: number): Observable<any> => {
        return this._http.get(this._app_config_service.getApiService("getPass").toString() + "/" + id + "/Onlanguage/" + lang)
            .pipe(tap(response => {
                return response;
            }));
    }
}
