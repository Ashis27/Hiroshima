import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { LocalNotifications } from '@ionic-native/local-notifications';
// import { TweenMax } from 'gsap/TweenMax';

/**
 * Generated class for the PaymentSuccessPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

 
 /**
  * Value of the GSAP animation Tweenmax value
  */
 declare var TweenMax:any;	
 /**
  * Value of the GSAP animation Elastic value
  */	
declare var Elastic:any;

@IonicPage()
@Component({
  selector: 'page-payment-success',
  templateUrl: 'payment-success.html',
  providers:[LocalNotifications]
})
export class PaymentSuccessPage extends LanguageHandlerService {

  /**
*@ignore
*/
  constructor(injector: Injector, public localNotifications: LocalNotifications,public navCtrl: NavController, public navParams: NavParams) {
    super(injector);
  }

   /**
   * Initial entry point
   * @example
   * ionViewDidLoad()
   */
  ionViewDidLoad(){
    this.localNotifications.schedule({
      id: new Date().getMilliseconds(),
      title: this.lang("SuccessfulBookingTitle"),
      text: this.lang("SuccessfulBookingNotification"),
      trigger: { at: new Date(new Date().getTime() + 100) },
      vibrate: true,
      //sound: "file://notification.mp3",
      icon: "file://logo.png",
      //every: "0"
    });
  }

  /**
* To redirect to available pass page once pass booked successfully
* @example
* goToAvailablePass()
* @returns {void}
*/
  goToAvailablePass() {
    this.navCtrl.setRoot('DashboardPage', { selectedTab: "AvailablePass" });
  }
 
}
