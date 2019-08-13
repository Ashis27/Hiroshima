import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams, Events, Platform, AlertController } from 'ionic-angular';
import { LanguageService, ILanguageInfo } from '../../shared/interfaces';
import { LanguageHandlerService } from '../../shared/handler/language.service';
import { ApiServicesHandlerService } from '../../shared/handler/app.constant';
import { ConfigurationServiceProvider } from '../../providers/_services';
import { InAppBrowser } from '@ionic-native/in-app-browser';

/**
 * Generated class for the DashboardPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-dashboard',
  templateUrl: 'dashboard.html',
  providers: [InAppBrowser]
})
export class DashboardPage extends LanguageHandlerService {

  /**
    Value of the default selected tab
  */
  selectedTab: string = "BuyPass";

  /**
  Value of the default selected tab name
*/
  tabName: string;

  /**
  Value of the LanguageService which contain selected language with all static label contents and all active languages
*/
  _lang_service: LanguageService = {
    languages: [],
    currentLanguage: Object.assign({}),
    currentLangContents: []
  };

  /**
  Active static tabs which contain different pass information
*/
  tabList = [
    { pageStatus: "BuyPass", title: "Buy", activeIcon: 'assets/imgs/dashboard/buy_active.png', inActiveIcon: 'assets/imgs/dashboard/buy_inactive.svg', defaultImg: 'assets/imgs/dashboard/buy_inactive.svg' },
    { pageStatus: "AvailablePass", title: "Available", activeIcon: 'assets/imgs/dashboard/available_active.svg', inActiveIcon: 'assets/imgs/dashboard/available_inactive.png', defaultImg: 'assets/imgs/dashboard/available_inactive.png' },
    { pageStatus: "InUsePass", title: "InUse", activeIcon: 'assets/imgs/dashboard/portfolio-active.svg', inActiveIcon: 'assets/imgs/dashboard/portfolio.png', defaultImg: 'assets/imgs/dashboard/portfolio.png' },
    { pageStatus: "HistoryPass", title: "History", activeIcon: 'assets/imgs/dashboard/calendar-active.svg', inActiveIcon: 'assets/imgs/dashboard/calendar.png', defaultImg: 'assets/imgs/dashboard/calendar.png' },
  ];

  /**
  Value of the platform which is iOS or not
*/

  isIosPlatform: boolean = false;

  /**
  Value of the update app modal, if use has already skipped or not. default is false.
*/
  isSkipped: boolean = false;


  /**
  * @ignore
  */
  constructor(
    injector: Injector,
    public navCtrl: NavController,
    public navParams: NavParams,
    public events: Events,
    private _configurationService: ConfigurationServiceProvider,
    private iab: InAppBrowser,
    public platform: Platform,
    private alertCtrl: AlertController,
  ) {
    super(injector);
    // this.events.subscribe('selectedLanguage', (data) => {
    //   this.selectedLanguage = data;
    // });
    this._lang_service = this.navParams.get("_lang_service");
    this.tabName = this.navParams.get('selectedTab') ? this.navParams.get('selectedTab') : this.selectedTab;
    this.selectedTab = this.tabName;
    if (this.platform.is('ios')) {
      this.isIosPlatform = true;
    } else {
      this.isIosPlatform = false;
    }
  }

  /**
* Initial entry point to get current lang info from cache and current app version status from cache after page loading
* @example
* ionViewDidEnter()
*/
  ionViewDidEnter() {
    this.tabList.forEach(item => {
      if (this.selectedTab == item.pageStatus)
        this.selectedTabInfo(item);
    });
    this.getCurrentLangInfoFromCache();
    this.getCurrentAppVersionFromCache();
  }

  /**
  * To get current app configuration and skip status if user has already skip the update. 
  * It will no longer show the update modal
  * @example
  * getCurrentAppVersionFromCache()
  */
  getCurrentAppVersionFromCache() {
    this._app_config_service.getDatafromCacheStorage(ApiServicesHandlerService._cache_key_value.appConfiguration)
      .then((result) => {
        if (result)
          this.isSkipped = true;
        else
          this.isSkipped = false;
        this.getAppCurrentVersion();
      });
  }

  /**
* To get current app configuration by device type. 
* Then it will check the condition based on current app version with updated app version
* @example
* getAppCurrentVersion()
* @param {string} deviceType android | iOS
* @returns App configuration object along with config information
*/
  getAppCurrentVersion() {
    this._configurationService.GetAppConfiguration(this.isIosPlatform ? 'iOS' : 'android', false)
      .subscribe(response => {
        this.isNewUpdateAvailable(response);
      },
        error => {
          this._custom_message_handler.errorMessage(this.lang(error));
        });
  }


  /**
* Open update modal if app version is less than updated version
* Otherwise current is up to date 
* Then it will check the condition based on force update
* @example
* isNewUpdateAvailable()
*/
  isNewUpdateAvailable(response) {
    if (Number((this._app_config_service.getAppVersion()).replace('.', '').replace('.', '')) < Number((response.updatedVersion).replace('.', '').replace('.', ''))) {
      if (response && response.isForceUpdate)
        this.forceAlert(response);
      else if (!this.isSkipped)
        this.skipAlert(response);
    }
  }


  /**
* To skip update modal if the app is not force to update
* Then it will store the skiped status in cache memory for not to show the update modal again
* @example
* skipAlert()
* @returns void
*/
  skipAlert(value) {
    let alert = this.alertCtrl.create({
      title: this.lang("UpdateAppTitle"),
      message: this.lang("NewVersionAvailable"),
      enableBackdropDismiss: false,
      buttons: [
        {
          text: this.lang("SKIP"),
          handler: () => {
            this._app_config_service.setDataInCacheStorage(true, ApiServicesHandlerService._cache_key_value.appConfiguration);
          }
        },
        {
          text: this.lang("UPDATE"),
          handler: () => {
            this._app_config_service.setDataInCacheStorage(true, ApiServicesHandlerService._cache_key_value.appConfiguration);
            window.open(value.appStoreURL, '_system');

          }
        }
      ]
    });
    alert.present();
  }


  /**
* To open update modal if the app is force to update and user has to update to access all the functionality
* Then it will store the skiped status in cache memory for not to show the update modal again
* @example
* forceAlert()
* @returns void
*/
  forceAlert(value) {
    this._app_config_service.setDataInCacheStorage(true, ApiServicesHandlerService._cache_key_value.appConfiguration);
    let alert = this.alertCtrl.create({
      title: this.lang("UpdateAppTitle"),
      message: this.lang("NewVersionAvailable"),
      enableBackdropDismiss: false,
      buttons: [
        {
          text: this.lang("UPDATE"),
          handler: () => {
            window.open(value.appStoreURL, '_system');
          }
        }
      ]
    });
    alert.present();
  }


  /**
* To update dashboard tab style and icon if the selected tab is active
* Make all tyhe other tab as inactive
* @example
* selectedTabInfo()
* @param {object} tabInfo 
* @returns dashboard tab list with active information
*/
  selectedTabInfo(tab) {
    this.selectedTab = tab.pageStatus;
    this.tabList.forEach(item => {
      item.defaultImg = item.inActiveIcon;
      if (tab.pageStatus == item.pageStatus)
        item.defaultImg = item.activeIcon;
    });
  }


  /**
* To get all active lang information from cache, if already retrieved through API
* Make all tyhe other tab as inactive
* @example
* getCurrentLangInfoFromCache()
* @returns Language information
*/
  getCurrentLangInfoFromCache() {
    const result = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.activeLanguageInfo));
    if (result && result.currentLanguage && result.currentLanguage.displayName) {
      this._lang_service = {
        currentLanguage: result.currentLanguage,
        currentLangContents: result.currentLangContents,
        languages: result.languages
      };
    }
    else {
      this.navCtrl.setRoot("ApplicationStartUpPage");
    }

  }

  /**
* Go to language selection page
* @example
* onChangeLang()
* @returns void
*/
  onChangeLang() {
    this.navCtrl.push("SelectLanguagePage");
  }

  /**
* Go to feeback screen
* @example
* gotoFeedback() 
* @returns void
*/
  gotoFeedback() {
    this.navCtrl.push("FeedbackFormPage");
  }
}
