import { LanguageService } from './../../../shared/interfaces/ILanguage';
import { async, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { IonicModule, Platform, NavController, IonicErrorHandler, IonicPageModule, AlertController, NavParams, ToastController, Config, DomController, App, Keyboard } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { DebugElement } from '@angular/core';
import { mockApp } from 'ionic-angular/util/mock-providers';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { IonicStorageModule } from '@ionic/storage';
import { CacheModule } from 'ionic-cache';
import { EventsMock, AlertControllerMock } from 'ionic-mocks';
import { Observable } from 'rxjs';
import { NavMock, NavParamsMock, PlatformMock } from '../../../../test-config/mocks-ionic';
import { CustomMessageStub } from '../../../shared/mock/custommessagestub';
import { UserSeviceStub } from '../../../shared/mock/userservicestub';
import { JwtInterceptor, ErrorInterceptor, LoadingScreenInterceptor, HttpAPIInterceptor } from '../../../providers/_helpers';
import { UserServiceProvider } from '../../../providers/_services/user.service';
import { CustomValidationHandlerService } from '../../../shared/handler/customvalidation.service';
import { AppConfigurationServiceProvider } from '../../../providers/app.configuration.service';
import { LanguageServiceProvider, LoadingScreenService, ConfigurationServiceProvider, PassServiceProvider } from '../../../providers/_services';
import { HttpServiceProvider } from '../../../providers/http.services';
import { StorageHandlerService } from '../../../shared/handler/storage.service';
import { activePasses, activePassModel } from '../../../shared/mockmodels/passmockmodel';
import { PassSeviceStub } from '../../../shared/mock/passservicestub';
import { UserloginPage } from './userlogin';
import { bookingInfo, isActvated, uId } from '../../../shared/mockmodels/bookingmodel';
import { languageInfo, languages, language_info } from '../../../shared/mockmodels/languagemockmodel';
import { LanguageServiceStub } from '../../../shared/mock/languagemockservice';
import { TweenMax, TweenLite } from "gsap/TweenMax";

describe('UserloginPage Component: component created', () => {
    let fixture;
    let component;
    let title: DebugElement;
    let de: DebugElement;
    let el: HTMLElement;
    let pageComponent, SB: any;
    let navCtrl: NavMock;
    let alertCtrl: AlertController;
    let passSeviceStub: PassSeviceStub;
    let userServiceStub: UserSeviceStub;
    let customMessageStub: CustomMessageStub;
    let languageServiceStub: LanguageServiceStub;

    //santosh
    // let _langService: LanguageServiceProvider;
    //
    beforeEach(async(() => {


        TestBed.configureTestingModule({
            declarations: [UserloginPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(UserloginPage),
                IonicStorageModule.forRoot(),
                CacheModule.forRoot()
            ],
            providers: [
                { provide: NavParams, useClass: NavParamsMock },
                { provide: Platform, useClass: PlatformMock, },
                { provide: NavController, useClass: NavMock, },
                { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
                { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
                { provide: HTTP_INTERCEPTORS, useClass: LoadingScreenInterceptor, multi: true },
                { provide: AlertController, useFactory: () => AlertControllerMock.instance() },
                { provide: ToastController, useValue: mockApp() },
                { provide: App, useValue: mockApp() },
                { provide: UserServiceProvider, useClass: UserSeviceStub },
                { provide: CustomValidationHandlerService, useClass: CustomMessageStub },
                { provide: PassServiceProvider, useClass: PassSeviceStub },
                { provide: LanguageServiceProvider, useClass: LanguageServiceStub },
                AppConfigurationServiceProvider,
                HttpServiceProvider,
                HttpAPIInterceptor,
                LoadingScreenService,
                ConfigurationServiceProvider,
                StorageHandlerService,
                LanguageServiceStub,
                Config,
                DomController,
                Keyboard
            ]
        }).compileComponents()
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(UserloginPage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        passSeviceStub = fixture.debugElement.injector.get(PassServiceProvider);
        userServiceStub = fixture.debugElement.injector.get(UserServiceProvider);
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);
        languageServiceStub = fixture.debugElement.injector.get(LanguageServiceProvider);

   
        //santosh
        // this._langService = TestBed.createComponent(LanguageServiceProvider);
        //
        spyOn(component, 'onUserLogin').and.callThrough();
        // spyOn(component, 'getTermsAndConditions').and.callThrough();
        // spyOn(component, 'getActiveLanguages').and.callThrough();
        // spyOn(component, 'onChangeLanguage').and.callThrough();
        // spyOn(component, 'selectedTAC').and.callThrough();
        // spyOn(component, 'getActiveLanguagesFromCache').and.callThrough();
      
        spyOn(component.navCtrl, 'push');

    });

    afterEach(() => {
        fixture.destroy();
        component = null;
    });



    //1
    it('onUserLogin called ', () => {
        component.onUserLogin();
        expect(component.onUserLogin).toHaveBeenCalled();
       

    });

    
  


 


});
