import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { LocalNotifications } from '@ionic-native/local-notifications';
// import { TweenMax } from 'gsap/TweenMax';

/**
 * Generated class for the SeeAllPTOs page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */


/**
 * Value of the GSAP animation Tweenmax value
 */
declare var TweenMax: any;
/**
 * Value of the GSAP animation Elastic value
 */
declare var Elastic: any;

@IonicPage()
@Component({
  selector: 'page-see-all-ptos',
  templateUrl: 'see-all-ptos.html',
})
export class SeeAllPTOs extends LanguageHandlerService {
  /**
   * Value of the active PTOs
   */
  activePTOs: any = [];

  /**
*@ignore
*/
  constructor(injector: Injector, public navCtrl: NavController, public navParams: NavParams) {
    super(injector);
    this.activePTOs = this.navParams.get('activePTOs');
    this.activePTOs.forEach(element => {
      element["customClass"] = "pto-sec-left-border-" + Math.floor((Math.random() * 10) + 1);
    });
  }

  /**
  * Initial entry point
  * @example
  * ionViewDidEnter()
  */
  ionViewDidEnter() {
  }
  /**
  * Pop up page
  * @example
  * backToPrevious()
  */
  backToPrevious() {
    this.navCtrl.pop();
  }


}
