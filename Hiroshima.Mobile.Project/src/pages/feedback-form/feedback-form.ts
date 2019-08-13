import { Component, Injector, ChangeDetectorRef } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { UserServiceProvider } from '../../providers/_services/user.service';
import { ApiServicesHandlerService } from '../../shared/handler/app.constant';

/**
 * Generated class for the FeedbackFormPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-feedback-form',
  templateUrl: 'feedback-form.html',
})
export class FeedbackFormPage extends LanguageHandlerService {

  /**
Value of feedbackForm model
*/
  private feedbackForm: FormGroup;

  /**
Value of active feedback types
*/
  feedbackTypes: any = [];

/**
*@ignore
*/
  constructor(injector: Injector, public navCtrl: NavController, private formBuilder: FormBuilder, public navParams: NavParams, private userService: UserServiceProvider) {
    super(injector);
    this.feedbackTypes.push({
      text: "Booking",
      value: "booking"
    });
    this.initUserForm();
  }


  /**
* Initial entry point to initialize feedback form group object while entering the page
* @example
* ionViewDidEnter()
*/
  ionViewDidEnter() {
    this.initUserForm();
  }

  
   //top left navigation arrow :by santosh
   backToPrevious() {
    this.navCtrl.pop();

  }


  /**
* To get initialize feedback form group object
* @example
* initUserForm()
* @returns {FormGroup} feedbackForm object
*/
  initUserForm() {
    this.feedbackForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      contactNumber: ['', [Validators.required, Validators.minLength(10)]],
      feedbackDescription: ['', Validators.required],
      feedbackType: ['', Validators.required],
      deviceId: ['']
    }, );
  }


  /**
* convenience getter for easy access to form fields
* @example
* f()
* @returns {FormGroup} feedbackForm controls
*/
  get f() { return this.feedbackForm.controls; }

  /**
* To submit user feedback information
* @example
* onSubmit()
* @returns {void}
*/
  onSubmit() {
    //this.feedbackForm.value.deviceId = "12345yetegdd";
    this.feedbackForm.value.deviceId = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.userDeviceId));
    if (this.feedbackForm.value.deviceId)
      this.userService.SubmitFeedback(this.feedbackForm.value, true)
        .subscribe(
          data => {
            if (!data.success)
              return this._custom_message_handler.errorMessage(this.lang(data.message));
            this.navCtrl.pop();
            return this._custom_message_handler.successMessage(this.lang(data.message));
          },
          error => {
            this._custom_message_handler.errorMessage(this.lang(error));
          });
    else
      return this._custom_message_handler.errorMessage(this.lang("NoDeviceIdFound"));
  }
}
