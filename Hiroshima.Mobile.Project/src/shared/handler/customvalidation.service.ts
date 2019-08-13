import { Injectable } from '@angular/core';
import { ToastController, LoadingController } from 'ionic-angular';
import { DomSanitizer } from '@angular/platform-browser';
import * as $ from 'jquery';

/*
  Generated class for the CustomValidationHandlerService service.
*/
@Injectable()
export class CustomValidationHandlerService {

  /**
   *@ignore 
   */
  constructor(public _toastCtrl: ToastController) {

  }

  /**
   * To show the error messages
   * @example
   * errorMessage("Hello")
   * @param {string} message 
   */
  errorMessage(message) {
    let toast = this._toastCtrl.create({
      message: message,
      duration: 2000,
      cssClass: "error",
      showCloseButton: false
    });
    toast.present();
  }

   /**
   * To show the success messages
   * @example
   * successMessage("Hello")
   * @param {string} message 
   */
  successMessage(message) {
    let toast = this._toastCtrl.create({
      message: message,
      duration: 2000,
      cssClass: "success",
      showCloseButton: false
    });
    toast.present();
  }

   /**
   * To validate only number
   * @example
   * validateOnlyNumber(eventInfo)
   * @param {object} event 
   */
  validateOnlyNumber(event) {
    if (event.which == 8 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 46)
      return true;
    else if ((event.which != 46 || $(this).val().toString().indexOf('.') != -1) && (event.which < 48 || event.which > 57))
      event.preventDefault();
  }
}
