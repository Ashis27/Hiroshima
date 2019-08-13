import { ActiveTicketInfoPage } from './../active-ticket-info/active-ticket-info';
import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';
import { CustomValidationHandlerService } from '../../shared/handler/customvalidation.service';
import { PassModel } from '../../shared/models';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { first } from 'rxjs/operators';
import { UserServiceProvider } from '../../providers/_services/user.service';
import { BookedPassInformation } from '../../shared/models/BookedPassInformation';
import { LocalNotifications } from '@ionic-native/local-notifications';
/**
 * Generated class for the ActiveTicketInfoDataPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-active-ticket-info-data',
  templateUrl: 'active-ticket-info-data.html',
  providers: [LocalNotifications]
})
export class ActiveTicketInfoDataPage extends LanguageHandlerService {

  isActivated: boolean = true;

  activePasses: any;
  constructor(injector: Injector, public navCtrl: NavController, public navParams: NavParams, private userService: UserServiceProvider, public alertCtrl: AlertController, private _custom_message: CustomValidationHandlerService, public localNotifications: LocalNotifications, ) {
    super(injector);
    this.activePasses = this.navParams.get('selectedPassInfo');
  }

  ionViewDidLoad() {
  }


  /**
  * Back arrow navigates the user to back page 
  * @example
  * backToPrevious()
  */
  backToPrevious() {
    this.navCtrl.pop();
  }




   /**
  * Back arrow navigates the user to back page 
  * @example
  * backToPrevious()
  */
  goToQRScren() {
    this.navCtrl.pop();
  }


  
  /**
* To active pass, once user confirm
* @example
* activatePass()
* @param {string} uId Unique referrence number
* @returns {void}
*/
  activatePass() {
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
            this.userService.ActivatePass(this.activePasses.uniqueReferrenceNumber, true)
              .pipe(first())
              .subscribe(response => {
                this.isActivated = response.success;
                if (!response.success)
                  return this._custom_message.errorMessage(this.lang(response.message));
                this._custom_message.successMessage(this.lang(response.message));
                this.sendNotification();
                this.navCtrl.setRoot('DashboardPage', { selectedTab: "InUsePass" });
              },
                error => {
                  return this._custom_message.errorMessage(this.lang(error));
                });
          }
        }
      ]
    });
    alert.present();
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


  /**
* Redirect to dashboard page
* @example
* goToDashboard()
* @returns {void}
*/
  goToDashboard() {
    this.navCtrl.setRoot('DashboardPage');
  }

}
