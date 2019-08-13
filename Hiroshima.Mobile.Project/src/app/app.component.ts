import { Component, ViewChild } from '@angular/core';
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';
import { Nav, Platform, Events, AlertController } from 'ionic-angular';
import { ApiServicesHandlerService } from '../shared/handler/app.constant';
import { SearchParams } from '../shared/models';
import { LanguageService } from '../shared/interfaces';
import { ConfigurationServiceProvider, LanguageServiceProvider } from '../providers/_services';
import { CustomValidationHandlerService } from '../shared/handler/customvalidation.service';
import { Push, PushObject, PushOptions } from '@ionic-native/push';
/**
 * Generated class for the Appcomponent.
 */
@Component({
  templateUrl: 'app.html',
  providers: [StatusBar, Push]
})
export class MyApp {

  /**
   Value of current nav
   */
  @ViewChild(Nav) nav: Nav;

  /**
   Value of the current root page
   */
  rootPage: string = "ApplicationStartUpPage";

  /**
  Value of the LanguageService which contain selected language with all static label contents and all active languages
  */
  _languages: LanguageService = {
    languages: [],
    currentLanguage: Object.assign({}),
    currentLangContents: []
  };

  /**
  Value of SearchParams model for pagination 
  */
  searchParams: SearchParams = { pageIndex: 1, pageSize: 20, lang: 0 };

  /**
   * @ignore
   */
  constructor(private _custom_message_handler: CustomValidationHandlerService,
    private _langService: LanguageServiceProvider, public platform: Platform,
    public statusBar: StatusBar, public events: Events, public splashScreen: SplashScreen,
    public alertCtrl: AlertController,
    private push: Push
  ) {
    this.initialization();
  }

  /**
   App initialization 
   */
  initialization() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.backgroundColorByHexString("#36bb71");
      this.splashScreen.hide();
      // Added by Ashis
      // Note: Please see the below link to get the Typescript code for phonegap-plugin-push
      // https://github.com/phonegap/phonegap-plugin-push/blob/master/docs/TYPESCRIPT.md
      const options: PushOptions = {
        android: {
          senderID: "921974730599",
          vibrate: true,
          sound: false,
        },
        ios: {
          alert: 'true',
          badge: true,
          sound: 'false'
        }
      };

      this.notificationPermission();
      const pushObject: PushObject = this.push.init(options);
      pushObject.on('notification').subscribe((notification: any) =>
        this.showAlert(notification.subject, notification.message)
      );
      pushObject.on('registration').subscribe((registration: any) => {
        //this.storage.set(this.key, registration.registrationId)
        //alert(registration.registrationId),
        this.storeUniqueToken(registration.registrationId);
      }
        // this.commonServices.setStoreDataIncache(this.commonServices.getCacheKeyUrl("getUserDeviceToken"),registration.registrationId)
      );
      pushObject.on('error').subscribe(error => {
        // this.showAlert('Error', error)
      }
      );
    });
  }

  storeUniqueToken(token) {
    localStorage.setItem(ApiServicesHandlerService._cache_key_value.userDeviceId, JSON.stringify(token));
  }
  showAlert(Title, Message) {
    if (Title == undefined || Title == "" || Title == "") {
      Title = "Notification";
    }
    let notification_alert = this.alertCtrl.create({
      subTitle: Title,
      message: Message,
      buttons: ['OK']
    });
    notification_alert.present();
  }
  notificationPermission() {
    // to check if we have permission
    this.push.hasPermission()
      .then((res: any) => {
        if (res.isEnabled) {
          // console.log('We have permission to send push notifications');
        } else {
          // console.log('We do not have permission to send push notifications');
        }
      });
  }
}
