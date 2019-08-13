import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';
import { CustomValidationHandlerService } from '../../../shared/handler/customvalidation.service';
import { PassModel, SearchParams, PassDescriptionModel, PassActivePTOModel } from '../../../shared/models';
import { first } from 'rxjs/operators';
import { LanguageHandlerService } from '../../../shared/handler/language.service';
import moment from 'moment';
import { UserServiceProvider } from '../../../providers/_services/user.service';
import { ApiServicesHandlerService } from '../../../shared/handler/app.constant';
import { LocalNotifications } from '@ionic-native/local-notifications';
/**
 * Generated class for the AvailablepassPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

//@IonicPage()
@Component({
  selector: 'page-availablepass',
  templateUrl: 'availablepass.html',
  providers: [LocalNotifications]
})
export class AvailablepassPage extends LanguageHandlerService {

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
    public alertCtrl: AlertController,
    public localNotifications: LocalNotifications,
    private _custom_message: CustomValidationHandlerService, private userService: UserServiceProvider) {
    super(injector);
    this.getAvailablePasses(true);
  }
  /**
  * Initial entry point to get all available active passes while entering the page
  * @example
  * ionViewDidEnter()
  */
  ionViewDidEnter() {

  }

  /**
   * To get all active available passes
   * @example
   * getAvailablePasses(status)
   * @param status True | False (Use loader or not)
   * @returns list of active available passes
   */
  getAvailablePasses(status) {
    // this.searchParams.deviceId = "12345yetegdd";
    this.searchParams.deviceId = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.userDeviceId));

    this.userService.GetAvailablePasses(this.searchParams, status)
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
      this.getAvailablePasses(false);
      infiniteScroll.complete();
    }, 500);
  }

  /**
   * To activate the selected pass, once user confirm
   * @example
   * activatePass(HST-2-4638467)
   * @param {string} uId unique referrence number
   * @returns {boolean} True| False
   */
  activatePass(uId: string) {
    let alert = this.alertCtrl.create({
      title: this.lang("ConfirmActivation"),
      buttons: [
        {
          text: this.lang("CANCEL"),
          role: 'cancel',
          handler: () => {

          }
        },
        {
          text: this.lang("CONFIRM"),
          handler: () => {
            this.userService.ActivatePass(uId, true)
              .pipe(first())
              .subscribe(response => {
                if (!response.success)
                  return this._custom_message.errorMessage(this.lang(response.message));
                this._custom_message.successMessage(response.message);
                this.sendNotification();
                this.navCtrl.setRoot('DashboardPage', { selectedTab: "InUsePass" });
              },
                error => {
                  return this._custom_message_handler.errorMessage(this.lang(error));
                });
          }
        }
      ]
    });
    alert.present();
  }

  /**
  * To get booked ticket information. Redirect to ticket information page
  * @example
  * selectedPass(information)
  * @param {PassModel} info Pass information
  * @returns void
  */
  selectedPass(info) {
    this.navCtrl.push('ActiveTicketInfoDataPage', {
      selectedPassInfo: info,
      pageStatus: "available"
    })
  }

  /**
   * To send activated local notification
   * @example
   * sendNotification()
   */
  sendNotification() {
    this.localNotifications.schedule({
      id: new Date().getMilliseconds(),
      title: this.lang("SuccessfulActivationTitle"),
      text: this.lang("SuccessfulActivationNotification"),
      trigger: { at: new Date(new Date().getTime() + 100) },
      vibrate: true,
      //sound: "file://notification.mp3",
      icon: "file://logo.png",
      //every: "0"
    });
  }

}
