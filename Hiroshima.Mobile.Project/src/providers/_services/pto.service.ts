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
export class PTOServiceProvider {

    /**
    *@ignore
    */
    constructor(private _http: HttpServiceProvider, private _app_config_service: AppConfigurationServiceProvider) {

    }

    /**
   * To get all active PTOs
   * @example
   * GetPTOs(searchParams,false)
   * @param {SearchParams} searchParams pagination details
   * @param {boolean} status True | False
   * @returns {List} PTO information
   */
    GetPTOs = (searchParams: SearchParams): Observable<any> => {
        return this._http.post(this._app_config_service.getApiService("getPTOs").toString() + "/" + searchParams, searchParams)
            .pipe(tap(response => {
                return response;
            }));
    }

    /**
    * To get selected PTO information by indivisual PTO id
    * @example
    * GetPass(en,1)
    * @param {number} lang language id
    * @param {number} id PTO id
    * @returns {PTOModel} PTO information
    */
    GetPTO = (lang: number, id: number): Observable<any> => {
        return this._http.get(this._app_config_service.getApiService("getPTO").toString() + "/" + id + "/Onlanguage/" + lang)
            .pipe(tap(response => {
                return response;
            }));
    }

}
