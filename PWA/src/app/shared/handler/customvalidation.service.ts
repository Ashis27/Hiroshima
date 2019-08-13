import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormGroup } from '@angular/forms';
/**
 * Jquery initialization
 */
declare const $: any;

/*
  Generated class for the CustomValidationHandlerService service.
*/
@Injectable({ providedIn: 'root' })
export class CustomValidationHandlerService {

  
  /**
   *@ignore 
   */
  constructor(private toastr: ToastrService) {

  }
    /**
   * To show the error messages
   * @example
   * errorMessage("Hello")
   * @param {string} message 
   */
  errorMessage(errMessage: string) {
    this.toastr.error(errMessage);
  }
    /**
   * To show the success messages
   * @example
   * errorMessage("Hello")
   * @param {string} message 
   */
  successMessage(successMessage: string) {
    this.toastr.success(successMessage);
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
