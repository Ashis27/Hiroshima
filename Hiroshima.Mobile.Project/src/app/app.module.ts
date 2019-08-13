import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { MyApp } from './app.component';
import { HttpServiceProvider } from '../providers/http.services';
import { CustomValidationHandlerService } from '../shared/handler/customvalidation.service';
import { IonicStorageModule } from '@ionic/storage';
import { CacheModule } from 'ionic-cache';
import { AppConfigurationServiceProvider } from '../providers/app.configuration.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor, ErrorInterceptor, HttpAPIInterceptor, LoadingScreenInterceptor } from '../providers/_helpers';
import { LanguageServiceProvider } from '../providers/_services/language.service';
import { LoadingScreenService, PassServiceProvider, ConfigurationServiceProvider } from '../providers/_services';
import { UserServiceProvider } from '../providers/_services/user.service';
import { StorageHandlerService } from '../shared/handler/storage.service';
import { ActiveTicketInfoDataPage } from '../pages/active-ticket-info-data/active-ticket-info-data';


@NgModule({
  declarations: [
    MyApp,
    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    IonicModule.forRoot(MyApp),
    IonicStorageModule.forRoot(),
    CacheModule.forRoot()
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoadingScreenInterceptor, multi: true },
    { provide: ErrorHandler, useClass: IonicErrorHandler },
    StatusBar,
    SplashScreen,
    AppConfigurationServiceProvider,
    HttpServiceProvider,
    LanguageServiceProvider,
    CustomValidationHandlerService,
    HttpAPIInterceptor,
    LoadingScreenService,
    UserServiceProvider,
    ConfigurationServiceProvider,
    StorageHandlerService
  ]
})
export class AppModule { }
