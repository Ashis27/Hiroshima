import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { HttpServiceProvider } from '../http.services';
import { AppConfigurationServiceProvider } from '../app.configuration.service';
import { SearchParams } from '../../shared/models';
import { BookedPassInformation } from '../../shared/models/BookedPassInformation';
/*
  Generated class for the user.service provider.
*/
@Injectable()
export class UserServiceProvider {

    /**
    *@ignore
    */
    constructor(private _http: HttpServiceProvider, private _app_config_service: AppConfigurationServiceProvider) {

    }

    /**
   * To get all available passes
   * @example
   * GetAvailablePasses(searchParams,false)
   * @param {SearchParams} searchParams pagination details
   * @param {boolean} status True | False
   * @returns {PassModel[]} Pass information
   */
    GetAvailablePasses = (searchParams: SearchParams, status: boolean): Observable<any> => {
        searchParams.lang = this._app_config_service.getSelectedLangFromStorage();
        return this._http.post(this._app_config_service.getApiService("getAvailablePasses").toString() + "/" + searchParams, searchParams, status)
            .pipe(tap(response => {
                return response;
            }));
    }

   /**
   * To get in use passes
   * @example
   * GetAvailablePasses(searchParams,false)
   * @param {SearchParams} searchParams pagination details
   * @param {boolean} status True | False
   * @returns {List} Pass information
   */
    GetInUsePasses = (searchParams: SearchParams, status: boolean): Observable<any> => {
        searchParams.lang = this._app_config_service.getSelectedLangFromStorage();
        return this._http.post(this._app_config_service.getApiService("getInUsePasses").toString() + "/" + searchParams, searchParams, status)
            .pipe(tap(response => {
                return response;
            }));
    }

   /**
   *To get expired passes
   * @example
   * GetExpiredPasses(searchParams,false)
   * @param {SearchParams} searchParams pagination details
   * @param {boolean} status True | False
   * @returns {List} Pass information
   */
    GetExpiredPasses = (searchParams: SearchParams, status: boolean): Observable<any> => {
        searchParams.lang = this._app_config_service.getSelectedLangFromStorage();
        return this._http.post(this._app_config_service.getApiService("getExpiredPasses").toString() + "/" + searchParams, searchParams, status)
            .pipe(tap(response => {
                return response;
            }));
    }

   /**
   *To get booking information
   * @example
   * GetBookingInfo(searchParams,false)
   * @param {SearchParams} searchParams pagination details
   * @param {boolean} status True | False
   * @returns {object} booking information
   */
    GetBookingInfo = (searchParams: SearchParams, status: boolean): Observable<any> => {
        searchParams.lang = this._app_config_service.getSelectedLangFromStorage();
        return this._http.post(this._app_config_service.getApiService("getExpiredPasses").toString() + "/" + searchParams, searchParams, status)
            .pipe(tap(response => {
                return response;
            }));
    }

    /**
   *To activat selected pass
   * @example
   * ActivatePass(HST-3-2357762634,false)
   * @param {string} uId Unique referrence number
   * @param {boolean} status True | False
   * @returns {boolean} True | False
   */
    ActivatePass = (uId: string, status: boolean): Observable<any> => {
        return this._http.get(this._app_config_service.getApiService("activatePass").toString() + "/" + uId, status)
            .pipe(tap(response => {
                return response;
            }));
    }


   /**
   *To book selected pass
   * @example
   * BookPass(HST-3-2357762634,false)
   * @param {BookedPassInformation} bookingInfo booking information
   * @param {boolean} status True | False
   * @returns {string} uId unique referrence number
   */
    BookPass = (bookingInfo: BookedPassInformation, status: boolean): Observable<any> => {
        return this._http.post(this._app_config_service.getApiService("bookPass").toString() + "/" + bookingInfo, bookingInfo, status)
            .pipe(tap(response => {
                return response;
            }));
    }

   /**
   *To submit user feedback
   * @example
   * BookPass(HST-3-2357762634,false)
   * @param {object} feedbackInfo feedback information
   * @param {boolean} status True | False
   * @returns {boolean} True | False
   */
    SubmitFeedback = (feedbackInfo: any, status: boolean): Observable<any> => {
        return this._http.post(this._app_config_service.getApiService("submitFeedback").toString() + "/" + feedbackInfo, feedbackInfo, status)
            .pipe(tap(response => {
                return response;
            }));
    }

   /**
   *To get booking information
   * @example
   * GetBookingInformationById("HTS-1-547",false)
   * @param {string} uId unique ref id
   * @param {boolean} status True | False
   * @returns {object} booking information
   */
  GetBookingInformationById = (uId:string, status: boolean): Observable<any> => {
     return this._http.get(this._app_config_service.getApiService("getbookingInfoById").toString() + "/" + uId, status)
        .pipe(tap(response => {
            return response;
        }));
}
}
