
import { Injectable, /* ElementRef */ } from '@angular/core';
import { Component, Injector, ViewChild } from '@angular/core';
import { IonicPage, NavController, NavParams, Platform, AlertController,/* InfiniteScroll */} from 'ionic-angular';
import { PassDescriptionModel, PassModel, PassActivePTOModel, SearchParams } from '../../../shared/models';
import { CustomValidationHandlerService } from '../../../shared/handler/customvalidation.service';
import { PassServiceProvider, ConfigurationServiceProvider } from '../../../providers/_services';
import { first } from 'rxjs/operators';
import { LanguageHandlerService } from '../../../shared/handler/language.service';
import { inject } from '@angular/core/src/render3';
import moment from 'moment';
import { InAppBrowser } from '@ionic-native/in-app-browser';
import { ApiServicesHandlerService } from '../../../shared/handler/app.constant';

/**
 * Generated class for the ActivepassPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

//@IonicPage()
@Component({
  selector: 'page-activepass',
  templateUrl: 'activepass.html',
  providers: [PassServiceProvider]
})


export class ActivepassPage extends LanguageHandlerService {
//  @ViewChild('infiniteScroll', {read: InfiniteScroll}) public infiniteScroll:InfiniteScroll;
  /**
     Value of the PassDescriptionModel which contain pass decription information
   */
  passDesc: PassDescriptionModel[] = [];

  
  /**
  Value of the PassActivePTOModel which define how many PTOs are available for a pass
 */
  passActivePTOs: PassActivePTOModel[]

  /**
 Value of the PassModel which contain pass information
 */
  activePasses: PassModel[] = [];

  /**
 Value of the active pass list from API along with paginated information
 */
  paginatedPass = {
    items: this.activePasses,
    TotalCount: 0,
    TotalPages: 0,
    HasPreviousPage: false,
    HasNextPage: false
  }


  /**
 Value of SearchParams model for pagination 
 */
  searchParams: SearchParams = { pageIndex: 1, pageSize: 5, lang: 0 };

  /**
 Value of available pass status 
 */
  isAvaileble: boolean = true;

  /**
  *@ignore
  */
  constructor(
    injector: Injector,
    public navCtrl: NavController,
    public navParams: NavParams,
    private _custom_message: CustomValidationHandlerService,
    private passService: PassServiceProvider) {
    super(injector);
    this.getActivePasses(true);
  }

  /**
* Initial entry point to get all active passes while entering the page
* @example
* ionViewDidEnter()
*/
  ionViewDidEnter() {

  }

  /**
 * To get all active passes
   * @example
    * getActivePasses(status)
    * @param {boolean} status True | False (Use loader or not)
    * @returns list of active passes
    */
  getActivePasses(status) {
    this.searchParams.deviceId = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.userDeviceId));
    this.passService.GetPasses(this.searchParams, status)
      .pipe(first())
      .subscribe(response => {
        if (!response || response.items.length == 0)
          if (this.paginatedPass.items.length == 0)
            return this.isAvaileble = false;
        //Convert Date
        if (response && response.items.length > 0) {
          //increase pagination index for next available information
          this.searchParams.pageIndex = this.searchParams.pageIndex + 1;
          response.items.forEach(element => {
            if (element)
              element.passExpiredDate = moment(element.passExpiredDate).format("MM/DD/YYYY");
          });
        }
        this.paginatedPass = {
          items: this.paginatedPass.items.concat(response.items),
          TotalCount: response.totalCount,
          TotalPages: response.totalPages,
          HasPreviousPage: response.hasPreviousPage,
          HasNextPage: response.hasNextPage
        };
        this.isAvaileble = true;
      },
        error => {
          this.isAvaileble = false;
          this._custom_message_handler.errorMessage(this.lang(error));
        });
  }

  /**
   * While page scrolling, it will get the active passes for selected page
  * @example
   * doInfinite()
   * @param infiniteScroll 
   */



  doInfinite(infiniteScroll) {
    setTimeout(() => {
      this.getActivePasses(false);
      infiniteScroll.complete();
    }, 500);
  }



  /**
* To get selected pass information. Redirect to pass information page
* @example
* selectedPass(information)
* @param {PassModel} info Pass information
* @returns void
*/
  selectedPass(info) {
    this.navCtrl.push('PassInformationPage', {
      selectedPassInfo: info
    });
  }

  /**
* To book selected pass. Redirect to rider information page
* @example
* bookPass(information)
* @param {PassModel} info Pass information
* @returns void
*/
  bookPass(info) {
    this.navCtrl.push('RiderInformationPage', {
      selectedPassInfo: info
    });
  }
}
