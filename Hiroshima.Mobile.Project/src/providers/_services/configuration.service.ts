import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { HttpServiceProvider } from '../http.services';
import { AppConfigurationServiceProvider } from '../app.configuration.service';
import { SearchParams } from '../../shared/models';

/*
  Generated class for the login.service provider.
*/
@Injectable()
export class ConfigurationServiceProvider {

/**
 * @ignore
 */
  constructor(public _http: HttpServiceProvider, private _app_config_service: AppConfigurationServiceProvider) {

  }

  /**
   * Get app configuration by device type
   * @example
   * GetAppConfiguration(android,false)
   * @param {string} deviceType android | iOS
   * @param {boolean} status True | False
   * @returns {object} app configuration
   */
  GetAppConfiguration = (deviceType: string, status: boolean): Observable<any> => {
    return this._http.get(this._app_config_service.getApiService("getAppConfiguration").toString() + "/" + deviceType, status)
      .pipe(tap(response => {
        return response;
      }));
  }
}
