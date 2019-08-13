import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams, AlertController } from 'ionic-angular';
import { CustomValidationHandlerService } from '../../shared/handler/customvalidation.service';
import { PassModel } from '../../shared/models';
import { BookedPassInformation } from '../../shared/models/BookedPassInformation';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { UserServiceProvider } from '../../providers/_services/user.service';
import { first } from 'rxjs/operators';
import moment from 'moment';
import { ApiServicesHandlerService } from '../../shared/handler/app.constant';
import { InAppBrowser, InAppBrowserOptions } from '@ionic-native/in-app-browser';

/**
 * Generated class for the RiderInformationPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-rider-information',
  templateUrl: 'rider-information.html',
  providers: [InAppBrowser]
})
export class RiderInformationPage extends LanguageHandlerService {

  /**
Value of the PassModel which contain pass information
*/
  activePasses: PassModel;

  /**
Value of selected adult
*/
  noOfAdult: number = 0;

  /**
Value of the selected child
*/
  noOfChild: number = 0;

  /**
Value of the total amount tax amount
*/
  taxAmount: number = 0;

  /**
Value of the BookedPassInformation which contain booking pass information
*/
  bookedPassInformation: BookedPassInformation = {
    passInformationId: 0,
    travellerDeviceId: "",
    bookingDate: "",
    totalAmout: 0,
    child: 0,
    adult: 0,
  }

  /**
  *@ignore
  */
  constructor(injector: Injector, private iab: InAppBrowser, public alertCtrl: AlertController, public navCtrl: NavController, public navParams: NavParams, private _custom_message: CustomValidationHandlerService, private userService: UserServiceProvider) {
    super(injector);
    this.activePasses = this.navParams.get('selectedPassInfo');
  }

  //top left navigation arrow :by santosh
  backToPrevious() {
    this.navCtrl.pop();

  }

  /**
   * To book selected pass along with user information, once user confirm to book
   * @example 
   * bookPass()
   * @param {BookedPassInformation} bookedPassInformation
   * @returns {void}
   */
  bookPass(data) {
    this.bookedPassInformation.travellerDeviceId = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.userDeviceId));
    this.bookedPassInformation.passInformationId = data.id;
    this.bookedPassInformation.bookingDate = moment().format("MM/DD/YYYY");
    this.bookedPassInformation.totalAmout = ((data.adultPrice * this.noOfAdult) + (data.childPrice * this.noOfChild) + this.taxAmount);
    this.bookedPassInformation.child = this.noOfChild;
    this.bookedPassInformation.adult = this.noOfAdult;
    //this.bookedPassInformation.travellerDeviceId = "12345yetegdd";
    if (this.bookedPassInformation.totalAmout <= this.taxAmount)
      return this._custom_message.errorMessage(this.lang("InValidPassengerInformation"));
    let alert = this.alertCtrl.create({
      title: this.lang("ConfirmProceed"),
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
            this.userService.BookPass(this.bookedPassInformation, true)
              .pipe(first())
              .subscribe(response => {
                if (!response.success)
                  return this._custom_message.errorMessage(this.lang(response.message));
                const uId = response.result;
                // this.navCtrl.push("PaymentSuccessPage");
                this.proceedToPaymentThroughGMO(uId);
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
  proceedToPaymentThroughGMO(uId) {
    let options: InAppBrowserOptions = {
      location: 'no', //Or 'no' 
      //hidden: 'yes', //Or  'yes'
      zoom: 'no', //Android only ,shows browser zoom controls 
      hardwareback: 'yes',
      mediaPlaybackRequiresUserAction: 'yes',
      shouldPauseOnSuspend: 'no', //Android only 
      closebuttoncaption: 'Share', //iOS only
      disallowoverscroll: 'no', //iOS only 
      toolbar: 'yes', //iOS only 
      toolbarposition: 'bottom',
      enableViewportScale: 'no', //iOS only 
      allowInlineMediaPlayback: 'no', //iOS only 
      presentationstyle: 'formsheet', //iOS only 
      fullscreen: 'yes', //Windows only  
      suppressesIncrementalRendering: 'no',
      transitionstyle: 'crossdissolve',

    };
    let refId = uId;
    const browser = this.iab.create(this._app_config_service.getPaymentApiURL() + "paymentGateway/GMOPamentGateway?uniqueId=" + refId, '_blank', options);
    browser.show();
    browser.on('loadstop').subscribe(event => {
      if (event.url == this._app_config_service.getPaymentApiURL() + "PaymentGateway/PaymentSuccessResponse" || event.url == this._app_config_service.getPaymentApiURL() + "PaymentGateway/PaymentPendingResponse") {
        browser.close();
        this.navCtrl.push("PaymentSuccessPage");
      }
      else if (event.url == this._app_config_service.getPaymentApiURL() + "PaymentGateway/PaymentFailureResponse") {
        browser.close();
        this.navCtrl.push("PaymentFailurePage");
      }
    });
  }
}
