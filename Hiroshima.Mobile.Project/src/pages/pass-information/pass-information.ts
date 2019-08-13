import { DashboardPage } from './../dashboard/dashboard';
import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { BookedPassInformation } from '../../shared/models/BookedPassInformation';
import { TweenMax, TweenLite } from "gsap/TweenMax";
import { PassModel } from '../../shared/models';
/**
 * Generated class for the PassInformationPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-pass-information',
  templateUrl: 'pass-information.html',
})
export class PassInformationPage extends LanguageHandlerService {

  /**
Value of BookedPassInformation model information
*/
  activePasses: PassModel;

  /**
*@ignore
*/
  constructor(injector: Injector, public navCtrl: NavController, public navParams: NavParams) {
    super(injector);
    this.activePasses = this.navParams.get('selectedPassInfo');
  }


  /**
* To book selected pass. Redirect to rider information page
* @example
* bookPass(information)
* @param {PassModel} info Pass information
* @returns {void}
*/
  bookPass() {
    this.navCtrl.push('RiderInformationPage', {
      selectedPassInfo: this.activePasses
    })
  }
  /**
  * Initial entry after page loaded
  * @example
  * ionViewDidLoad()
  */
  ionViewDidLoad() {
    TweenMax.staggerFrom(".pass-info-card-style", 0.4, { y: 80, delay: 0.1 }, 0.2);
  }
  /**
  * Pop up page
  * @example
  * backToPrevious()
  */
  backToPrevious() {
    this.navCtrl.pop();
  }
    /**
  * See all active PTOs on a pop up
  * @example
  * seeAllPTOs()
  */
  seeAllPTOs() {
    this.navCtrl.push('SeeAllPTOs', {
      activePTOs: this.activePasses.passActivePTOs
    })
  }
}
