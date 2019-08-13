import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { LanguageService } from '../../shared/interfaces';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { ApiServicesHandlerService } from '../../shared/handler/app.constant';
import { SearchParams } from '../../shared/models';
import { LanguageServiceProvider } from '../../providers/_services';
//below imported for gsap animation
import { TweenMax, TweenLite } from "gsap/TweenMax";
/**
 * Generated class for the SelectLanguagePage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */
declare var easeOut: any;
@IonicPage()
@Component({
  selector: 'page-select-language',
  templateUrl: 'select-language.html',
})
export class SelectLanguagePage extends LanguageHandlerService {

  /**
Value of the LanguageService which contain selected language with all static label contents and all active languages
*/
  _languages: LanguageService = {
    languages: [],
    currentLanguage: Object.assign({}),
    currentLangContents: []
  };

  /**
Value of the terms and conditions selected status, default is false
*/
  isSelected: boolean = false;

  /**
Value of SearchParams model for pagination 
*/
  searchParams: SearchParams = { pageIndex: 1, pageSize: 20, lang: 0 };

  /**
*@ignore
*/
  constructor(private _langService: LanguageServiceProvider, public navCtrl: NavController, injector: Injector) {
    super(injector);
    this.isSelected = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.defaultLanguage));
  }


  /**
* Initial entry point to get all active languages from cache memory
* @example
* ionViewDidEnter()
*/
  ionViewDidEnter() {
    this.getActiveLanguagesFromCache();
  }


  /**
   * To get all active languages from cache and then get updated from API call and store in cache
   * @example
   *  getActiveLanguagesFromCache()
   * @returns {LanguageService} 
   */
  getActiveLanguagesFromCache() {
    let result: any = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.activeLanguageInfo));
    if (result)
      this._languages = {
        currentLanguage: result.currentLanguage,
        currentLangContents: result.currentLangContents,
        languages: result.languages
      };
    //While changing the language then always get the active languages from server otherwise take it from cache
    if (!this.isSelected)
      this.getActiveLanguages(false);
  }

  /**
 * To get active language and select default if user has already selected default language.
 * @example
 *  getActiveLanguages(status)
 * @param status True | False (Use loader or not)
 * @returns {LanguageService} list of  active languages
 */
  getActiveLanguages(status) {
    this._langService.GetActiveLanguages(this.searchParams, status)
      .subscribe(response => {
        if (response && response.items.length > 0) {
          if (this._languages && this._languages.currentLanguage)
            response.items.forEach(element => {
              element.isDefault = false;
              if (element.id == this._languages.currentLanguage.id)
                element.isDefault = true;
            });
          this._languages.languages = response.items;
          localStorage.setItem(ApiServicesHandlerService._cache_key_value.activeLanguageInfo, JSON.stringify(this._languages));
        }
        else
          this._custom_message_handler.errorMessage(this.lang("FailedToRetrieve"));
      },
        error => {
          this._custom_message_handler.errorMessage(this.lang(error));
        });
  }

  /**
  * To get selected lang information once user change the language.
  * @example
  *  onChangeLanguage(lang)
  * @param lang selected lang information
  */
  //#region  on language change refreh the page
  onChangeLanguage(lang) {
    this._languages.currentLanguage = lang;
    this._languages.languages.forEach(element => {
      element.isDefault = false;
      if (element.id == this._languages.currentLanguage.id)
        element.isDefault = true;
    });
    if (this.isSelected)
      this.hasValueChanged();
  }

  /**
* To get selected terms and conditions selected status.
* @example
*  selectedTAC(lang)
* @returns {void}
*/
  selectedTAC() {
    localStorage.setItem(ApiServicesHandlerService._cache_key_value.defaultLanguage, JSON.stringify(true));
    this.hasValueChanged();
  }

  /**
  * To get terms and conditions selected content.
  * @example
  *  getTermsAndConditions()
  * @returns {string}
  */
  getTermsAndConditions() {

  }

  /**
  * Close page
  * @example
  *  backToPrevious()
  * @returns {void}
  */
  backToPrevious() {
    this.navCtrl.pop();

  }
  /**
  * Call after page loaded
  * @example
  *  ionViewDidLoad()
  * @returns {void}
  */
  ionViewDidLoad() {
    //Tweenmax line is used for gsap animation slide from right in the output
    TweenMax.staggerFrom(".row", 0.7, { opacity: 5, x: 80, delay: 0.1 }, 0.1);
  }

  /**
  * If user has chnaged language then the reove the available langage contents and reload the app to get latest contents.
  * @example
  *  hasValueChanged()
  * @returns {void}
  */
  async hasValueChanged() {
    this._languages.currentLangContents = null;
    await localStorage.setItem(ApiServicesHandlerService._cache_key_value.activeLanguageInfo, JSON.stringify(this._languages));
    this.navCtrl.setRoot("ApplicationStartUpPage"); //this.navCtrl.getActive().component
  }
}
