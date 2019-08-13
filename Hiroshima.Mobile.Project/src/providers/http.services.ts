import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpParams } from '@angular/common/http';
import { AppConfigurationServiceProvider } from './app.configuration.service';
import { DomSanitizer } from '@angular/platform-browser';
import { LoadingController } from 'ionic-angular';

/*
 * Generated class for the http.service provider.
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
*/
@Injectable()
export class HttpServiceProvider {

    /**
     *@ignore
     */
    constructor(private _http: HttpClient, private _app_config_service: AppConfigurationServiceProvider) { }

    /**
     * HTTP get service
     * @param {string} url api url to get data
     * @param {boolean} status True | false (Use loader or not)
     * @returns HTTP response
     */
    get(url: string, status?: any): Observable<any> {
        return this._http.get<any>(this._app_config_service.getRemoteServiceBaseUrl() + url, { params: new HttpParams().set('status', status) });
    }

    
    /**
     * HTTP post service
     * @param {string} url api url to get data
     * @param {object} model data to be stored in DB
     * @param {boolean} status True | false (Use loader or not)
     * @returns HTTP response
     */
    post(url: string, model: any, status?: any): Observable<any> {
        return this._http.post<any>(this._app_config_service.getRemoteServiceBaseUrl() + url, model, { params: new HttpParams().set('status', status) });
    }

     /**
     * HTTP put service
     * @param {string} url api url to get data
     * @param {object} model data to be stored in DB
     * @returns HTTP response
     */
    put(url: string, model: any): Observable<any> {
        return this._http.put<any>(this._app_config_service.getRemoteServiceBaseUrl() + url, model);
    }

     /**
     * HTTP delete service
     * @param {string} url api url to get data
     * @returns HTTP response
     */
    delete(url: string): Observable<Response> {
        return this._http.delete<any>(this._app_config_service.getRemoteServiceBaseUrl() + url);
    }
}
