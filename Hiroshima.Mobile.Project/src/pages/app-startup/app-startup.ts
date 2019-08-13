import { Component, ViewChild, NgZone, Injector } from '@angular/core';
import { App, IonicPage, Events, MenuController, Platform, NavParams, Nav, ViewController, LoadingController, ToastController, NavController, AlertController } from 'ionic-angular';
import { SyncAsync } from '@angular/compiler/src/util';
import { ApiServicesHandlerService } from '../../shared/handler/app.constant';
import { ILanguageInfo, LanguageService } from '../../shared/interfaces/ILanguage';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { inject } from '@angular/core/src/render3';
import { LanguageServiceProvider } from '../../providers/_services';
import { SearchParams } from '../../shared/models';

/**
 * Generated class for the AppStartUpPage page.
 */
@IonicPage()
@Component({
  selector: 'page-app-startup',
  templateUrl: 'app-startup.html',
  providers: []
})
export class ApplicationStartUpPage extends LanguageHandlerService {

  /**
  Value of current nav
  */
  @ViewChild(Nav) nav: Nav;
  /**
  Value of splash loader status
  */
  splash: boolean = true;

  /**
  Value of the Languageinformation along with aditional information of the language
  */
  _lang_info: ILanguageInfo;

  /**
  Value of the LanguageService which contain selected language with all static label contents and all active languages
  */
  _lang_service: LanguageService = {
    languages: [],
    currentLanguage: Object.assign({}),
    currentLangContents: []
  };

  /**
value of the selected terms and conditions status, default is  false
  */
  isTACselected: boolean = false;

  /**
  Value of SearchParams model for pagination 
  */
  searchParams: SearchParams = { pageIndex: 1, pageSize: 20, lang: 0 };

  /**
  Value of the rootpage to set the current root page 
  */
  rootPage: string;


  /**
  *@ignore
  */
  constructor(
    public platform: Platform,
    public viewCtrl: ViewController,
    public navCtrl: NavController,
    public appCtrl: App,
    private ngZone: NgZone,
    private _langService: LanguageServiceProvider,
    injector: Injector

  ) {
    super(injector);
  }

  /**
  * Initial entry point to get all active languages while entering the page
  * @example
  * ionViewDidEnter()
  */
  ionViewDidEnter() {
    // Check if the user has already selected terms & conditions
    this.isTACselected = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.defaultLanguage));
    if (!this.isTACselected) {
      this.getActiveLanguages(false);
    }
    //Get selected language contents from cache and then update contents internally
    else
      this.getCurrentLangInfoFromCache();
  }

  /**
  * To get active languages
  * @example
  * getActiveLanguages(status)
  * @param status True | False (Use loader or not)
  * @returns list of active language
  */
  getActiveLanguages(status) {
    this._langService.GetActiveLanguages(this.searchParams, status)
      .subscribe(response => {
        if (response && response.items.length > 0) {
          let defaultLang: any;
          response.items.forEach(element => {
            if (element.isDefault)
              defaultLang = element
          });
          this._lang_service.currentLanguage = defaultLang;
          this._lang_service.languages = response.items;
          if (!defaultLang)
            return this._custom_message_handler.errorMessage(this.lang("DefaultErrorMessage"));
          this.getActiveLangContents(defaultLang.displayName, false);
        }
        else
          this._custom_message_handler.errorMessage(this.lang("DefaultErrorMessage"));
      },
        error => {
          this._custom_message_handler.errorMessage(this.lang(error));
        });
  }

  /**
  * To get default language name from cache, if not then chhose to default one and fetch contents accordingly
  * @example
  * getCurrentLangInfoFromCache()
  * @returns list of active language
  */
  getCurrentLangInfoFromCache() {
    const result = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.activeLanguageInfo));
    if (result.currentLanguage && result.currentLanguage.displayName) {
      this._lang_service = {
        currentLanguage: result.currentLanguage,
        currentLangContents: result.currentLangContents,
        languages: result.languages
      };
      this.getActiveLangContents(result.currentLanguage.displayName, false);
    }
    //If chache cleared then it will ask user to select a default language
    else
      this.rootPage = "SelectLanguagePage";
  }

  /**
  * To get selected language contents
  * @example
  * getActiveLangContents()
  * @param {string} lang selected lang name
  * @param {boolean} status True | False (Use loader or not)
  * @returns active language contents
  */
  getActiveLangContents(lang, status) {
    this._langService.GetActiveLanguageContents(lang, status)
      .subscribe(response => {
        if (response && response.culture.lang == lang && response.languages) {
          this._lang_service.currentLangContents = response.languages;
          localStorage.setItem(ApiServicesHandlerService._cache_key_value.activeLanguageInfo, JSON.stringify(this._lang_service));
          //To chec check if T & C selected then skip that page and move to dashboard
          if (this.isTACselected) {
            this.nav.setRoot("DashboardPage", { "_lang_service": this._lang_service });
          }
          else
            this.rootPage = "SelectLanguagePage";
        }
        else
          this._custom_message_handler.errorMessage(this.lang("InValidConfiguration"));
      },
        error => {
          this._custom_message_handler.errorMessage(this.lang(error));
        });
  }

  /**
  * Spalash screen time out when page loaded
  * @example
  * ionViewDidLoad()
  */
  ionViewDidLoad() {
    setTimeout(() => {
      this.splash = false
    }, 2000);
  }
}
