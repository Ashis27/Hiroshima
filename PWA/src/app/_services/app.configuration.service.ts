import { HttpClient } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { IAppConfiguration } from '../shared/interfaces/IAppConfiguration';
import { IServiceList } from '../shared/interfaces/IServiceList';
import { Enviroment } from '../shared/enum/environment';
import { ApiServicesHandlerService } from '../shared/handler/app.constant';
import { StorageHandlerService } from '../shared/handler/storage.service';
/*
  Generated class for the AppConfigurationServiceProvider provider.
*/
@Injectable({ providedIn: 'root' })
export class AppConfigurationServiceProvider {

  /**
 App configuraion DI
 */
  private readonly _app_config: IAppConfiguration;
  /**
   Api service list
  */
  private _api_services: any;
  /**
  Value of the current enviroment variable
  */
  private envName: number;
  /**
  *@ignore
   */
  constructor() {
    this.envName = Enviroment.UAT;
  }

  /**
 * To remote service base url used for this app
 * @example
 * getRemoteServiceBaseUrl()
 * @returns {string} api url
 */
  getRemoteServiceBaseUrl() {
    return ApiServicesHandlerService._app_config[this.envName].apiServices.remoteServiceBaseUrl;
  }

  /**
   * To get active controller with API Url based on required component
   * @example
   * getApiService()
   * @returns {string} current api name along eith controller and method name
   */
  getApiService(value) {
    return ApiServicesHandlerService._api_services[value].api;
  }
  /**
 * To Get application name
 * @example
 * getAppName()
 * @returns {string} application name
 */
  getAppName() {
    return ApiServicesHandlerService._app_name;
  }
  /**
* To Get cache key url
* @example
* getCacheKeyUrl()
* @param {string} key cache key name
* @returns {string} current cache url
*/
  getCacheKeyUrl(key) {
    return this.getRemoteServiceBaseUrl() + key;
  }
}
