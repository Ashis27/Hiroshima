import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { LanguageHandlerService } from '../../shared/handler/language.service';

/**
 * Generated class for the PaymentFailurePage page.
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
  selector: 'page-payment-failure',
  templateUrl: 'payment-failure.html',
})
export class PaymentFailurePage extends LanguageHandlerService {

   /**
*@ignore
*/
  constructor(injector: Injector, public navCtrl: NavController, public navParams: NavParams) {
    super(injector);
  }


/**
* To retry payment once payment has failed
* @example
* retryPayment()
* @returns {void}
*/
  retryPayment() {

  }
  // ionViewDidLoad(){
  //   //thumb animation by gsap
  //  // TweenMax.to(".failure-image",2,{y:80,ease:Elastic.easeOut});
  // }

}
