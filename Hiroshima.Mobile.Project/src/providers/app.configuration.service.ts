import { HttpClient } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { IAppConfiguration } from '../shared/interfaces/IAppConfiguration';
import { IServiceList } from '../shared/interfaces/IServiceList';
import { Enviroment } from '../shared/enum/environment';
import { ApiServicesHandlerService } from '../shared/handler/app.constant';
import { StorageHandlerService } from '../shared/handler/storage.service';
import { CustomValidationHandlerService } from '../shared/handler/customvalidation.service';
import { SearchParams } from '../shared/models';

/*
  Generated class for the AppConfigurationServiceProvider provider.
*/
@Injectable()
export class AppConfigurationServiceProvider extends StorageHandlerService {

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
  constructor(injector: Injector, private _custom_validation: CustomValidationHandlerService) {
    super(injector);
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

/**
* To get data from cache memory
* @example
* getDatafromCacheStorage()
* @param {string} key cache key name
* @return stored result from cache
*/
  getDatafromCacheStorage(key): Promise<any> {
    return this.getStoreDataFromCache(this.getCacheKeyUrl(key));
  }

/**
* To store data in cache memory
* @example
* setDataInCacheStorage()
* @param {any} data data to be stored in cache
* @param {string} key cache key name
* @returns {void}
*/
  setDataInCacheStorage(data, key): Promise<any> {
    return this.setStoreDataIncache(this.getCacheKeyUrl(key), this.getAppName(), data);
  }

  /**
* To get selected language information from cache
* @example
* getSelectedLangFromStorage()
* @returns {any} data
*/
  getSelectedLangFromStorage(): any {
    const result = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.activeLanguageInfo));
    if (result.currentLanguage && result.currentLanguage.displayName)
      return result.currentLanguage.id
    return 0;
  }

 /**
* To get error message
* @example
* getErrorMessage()
* @param {string} msg message
* @returns {void}
*/
  getErrorMessage(msg) {
    return this._custom_validation.errorMessage(msg);
  }

 /**
* To get success message
* @example
* getErrorMessage()
* @param {string} msg message
* @returns {void}
*/
  getSuccessMessage(msg) {
    return this._custom_validation.successMessage(msg);
  }

 /**
* To get current version of the app
* @example
* getErrorMessage()
* @returns {string} 
*/
  getAppVersion() {
    return ApiServicesHandlerService._app_version;
  }
   /**
* To get current Payment URL
* @example
* getPaymentApiURL()
* @returns {string}
*/
  getPaymentApiURL(){
    return ApiServicesHandlerService._app_config[this.envName].paymentAPIURL;;
  }
}
