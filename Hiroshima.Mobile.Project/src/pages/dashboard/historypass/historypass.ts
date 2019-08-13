import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { CustomValidationHandlerService } from '../../../shared/handler/customvalidation.service';
import { PassDescriptionModel, PassActivePTOModel, PassModel, SearchParams } from '../../../shared/models';
import { first } from 'rxjs/operators';
import { LanguageHandlerService } from '../../../shared/handler/language.service';
import { UserServiceProvider } from '../../../providers/_services/user.service';
import moment from 'moment';
import { ApiServicesHandlerService } from '../../../shared/handler/app.constant';
/**
 * Generated class for the HistorypassPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

//@IonicPage()
@Component({
  selector: 'page-historypass',
  templateUrl: 'historypass.html',
})
export class HistorypassPage extends LanguageHandlerService {

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
  constructor(injector: Injector, public navCtrl: NavController,
    public navParams: NavParams,
    private _custom_message: CustomValidationHandlerService, private userService: UserServiceProvider) {
    super(injector);
    this.getExpiredPasses(true);
  }

  /**
  * Initial entry point to get all expired passes while entering the page
  * @example
  * ionViewDidEnter()
  */
  ionViewDidEnter() {
   
  }

  /**
   * To get all expired passes
   * @example
   * getExpiredPasses(status)
   * @param status True | False (Use loader or not)
   * @returns list of expired passes
   */
  getExpiredPasses(status) {
    // this.searchParams.deviceId = "12345yetegdd";
    this.searchParams.deviceId = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.userDeviceId));
   
    this.userService.GetExpiredPasses(this.searchParams, status)
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
            if (element.passInformation)
              element.passInformation.passExpiredDate = moment(element.passInformation.passExpiredDate).format("MM/DD/YYYY");
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
      this.getExpiredPasses(false);
      infiniteScroll.complete();
    }, 500);
  }

  /**
* To get selected pass information. Redirect to pass information page
* @example
* goToPassinfoPage(information)
* @param {PassModel} info Pass information
* @returns void
*/
  goToPassinfoPage(info) {
    this.navCtrl.push('PassInformationPage', {
      info: info
    })
  }

  /**
  * To get booked ticket information. Redirect to ticket information page
  * @example
  * selectedPass(information)
  * @param {PassModel} info Pass information
  * @returns void
  */
  selectedPass(info) {
    this.navCtrl.push('ActiveTicketInfoPage', {
      selectedPassInfo: info,
      pageStatus: "expired"
    })
  }
}
