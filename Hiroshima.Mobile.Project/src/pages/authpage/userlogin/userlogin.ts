import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { CustomValidationHandlerService } from '../../../shared/handler/customvalidation.service';
/**
 * Generated class for the UserloginPage page.
 */

@IonicPage()
@Component({
  selector: 'page-userlogin',
  templateUrl: 'userlogin.html',
})
export class UserloginPage {

  /**
*@ignore
*/
  constructor(
    public navCtrl: NavController,
    public navParams: NavParams,
    private _custom_validation: CustomValidationHandlerService
  ) {
  }

  /**
* Authentication
* @example
* onUserLogin()
*/
  onUserLogin() {

  }
}
