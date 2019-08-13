import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';
import { CustomValidationHandlerService } from '../../shared/handler/customvalidation.service';
import { PassModel } from '../../shared/models';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { first } from 'rxjs/operators';
import { UserServiceProvider } from '../../providers/_services/user.service';
import { BookedPassInformation } from '../../shared/models/BookedPassInformation';
import { LocalNotifications } from '@ionic-native/local-notifications';
import { ActiveTicketInfoDataPage } from '../active-ticket-info-data/active-ticket-info-data';

/**
 * Generated class for the ActiveTicketInfoPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-active-ticket-info',
  templateUrl: 'active-ticket-info.html',
  providers: [LocalNotifications]
})
export class ActiveTicketInfoPage extends LanguageHandlerService {
  day:any;
  hour:any;
  min:any;
  /**
   * Value of the acivate response from API call
   */
  isActivated: boolean = true;

  /**
  
Value of redirected page status like which page it comes from
*/
  pageStatus: string;

  /**
Value of BookedPassInformation model
*/
  activePasses: BookedPassInformation;
  /**
Value of BookedPassInformation model back up
*/
  activePassesBckUp: any

  /**
*@ignore
*/
  constructor(injector: Injector, public alertCtrl: AlertController, public localNotifications: LocalNotifications, public navCtrl: NavController, public navParams: NavParams, private _custom_message: CustomValidationHandlerService, private userService: UserServiceProvider) {
    super(injector);
    this.pageStatus = this.navParams.get('pageStatus');
    this.activePasses = this.navParams.get('selectedPassInfo');
    this.activePassesBckUp = this.activePasses;


    this.day=this.activePassesBckUp.theNoOfDaysRemaining;
    this.hour=this.activePassesBckUp.theNoOfHoursRemaining;
    this.min=this.activePassesBckUp.theNoOfMinutesRemaining;

    // console.log(this.day);
    // console.log( this.hour);
    // console.log(this.min);
  }


  /**
* Initial entry point to get booking information while entering the page
* @example
* ionViewDidEnter()
*/
  ionViewDidEnter() {
  }
  //top left navigation arrow:by santosh
  backToPrevious() {
    this.navCtrl.pop();

  }

  /**
* Redirect to get ticket info data page that displayes ticket info
* @example
* viewTicketInfo()
* @returns {void}
*/

  viewTicketInfo() {
    this.navCtrl.push('ActiveTicketInfoDataPage', { selectedPassInfo: this.activePasses, });
  }

  /**
* To active pass, once user confirm
* @example
// * activatePass()
* @param {string} uId Unique referrence number
* @returns {void}
*/
  // activatePass() {
  //   let alert = this.alertCtrl.create({
  //     title: this.lang("ConfirmActivation"),
  //     buttons: [
  //       {
  //         text: this.lang("CANCEL"),
  //         role: 'cancel',
  //         handler: () => {

  //         }
  //       },
  //       {
  //         text: this.lang("CONFIRM"),
  //         handler: () => {
  //           this.userService.ActivatePass(this.activePasses.uniqueReferrenceNumber, true)
  //             .pipe(first())
  //             .subscribe(response => {
  //               this.isActivated = response.success;
  //               if (!response.success)
  //                 return this._custom_message.errorMessage(this.lang(response.message));
  //               this._custom_message.successMessage(this.lang(response.message));
  //               this.sendNotification();
  //               this.navCtrl.setRoot('DashboardPage', { selectedTab: "InUsePass" });
  //             },
  //             error => {
  //               return this._custom_message.errorMessage(this.lang(error));
  //             });
  //         }
  //       }
  //     ]
  //   });
  //   alert.present();
  // }

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



  /**
   * To get booking information 
   * @example
   * getBookingInformation()
   * @param {string} uId unique referrence number
   * @returns {Object} booking information
   */
  getBookingInformation() {
    this.userService.GetBookingInformationById(this.activePasses.uniqueReferrenceNumber, false)
      .pipe(first())
      .subscribe(response => {
        this.isActivated = response.success;

        console.log(response);

        if (!response)
          return this._custom_message.errorMessage(this.lang("DefaultErrorMessage"));
        this.activePasses = response;
      },
        error => {
          return this._custom_message.errorMessage(this.lang(error));
        });
  }
  /**
* Spalash screen time out when page loaded
* @example
* ionViewDidLoad()
*/
  ionViewDidLoad() {
    setInterval(() => {
      if (this.pageStatus && this.pageStatus != 'available')
        this.getBookingInformation();
    }, 30000);
  }




















}
