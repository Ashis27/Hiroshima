import { LanguageService } from './../../shared/interfaces/ILanguage';
import { async, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { IonicModule, Platform, NavController, IonicErrorHandler, IonicPageModule, AlertController, NavParams, ToastController, Config, DomController, App, Keyboard, Events } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { DebugElement } from '@angular/core';
import { mockApp } from 'ionic-angular/util/mock-providers';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { IonicStorageModule } from '@ionic/storage';
import { CacheModule } from 'ionic-cache';
import { EventsMock, AlertControllerMock } from 'ionic-mocks';
import { Observable } from 'rxjs';
import { NavMock, NavParamsMock, PlatformMock } from '../../../test-config/mocks-ionic';
import { CustomMessageStub } from '../../shared/mock/custommessagestub';
import { UserSeviceStub } from '../../shared/mock/userservicestub';
import { JwtInterceptor, ErrorInterceptor, LoadingScreenInterceptor, HttpAPIInterceptor } from '../../providers/_helpers';
import { UserServiceProvider } from '../../providers/_services/user.service';
import { CustomValidationHandlerService } from '../../shared/handler/customvalidation.service';
import { AppConfigurationServiceProvider } from '../../providers/app.configuration.service';
import { LanguageServiceProvider, LoadingScreenService, ConfigurationServiceProvider, PassServiceProvider } from '../../providers/_services';
import { HttpServiceProvider } from '../../providers/http.services';
import { StorageHandlerService } from '../../shared/handler/storage.service';
import { activePasses, activePassModel } from '../../shared/mockmodels/passmockmodel';
import { PassSeviceStub } from '../../shared/mock/passservicestub';
import { DashboardPage } from './dashboard';
import { bookingInfo, isActvated, uId } from '../../shared/mockmodels/bookingmodel';
import { languageInfo, languages, language_info } from '../../shared/mockmodels/languagemockmodel';
import { LanguageServiceStub } from '../../shared/mock/languagemockservice';
import { TweenMax, TweenLite } from "gsap/TweenMax";
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('DashboardPage Component: component created', () => {
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
            declarations: [DashboardPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(DashboardPage),
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
                Keyboard,
                 Events 
            ],
            schemas: [CUSTOM_ELEMENTS_SCHEMA]
        }).compileComponents()
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(DashboardPage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        passSeviceStub = fixture.debugElement.injector.get(PassServiceProvider);
        userServiceStub = fixture.debugElement.injector.get(UserServiceProvider);
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);
        languageServiceStub = fixture.debugElement.injector.get(LanguageServiceProvider);


   
     
        // this._langService = TestBed.createComponent(LanguageServiceProvider);
        //skipAlert
        spyOn(component, 'ionViewDidEnter').and.callThrough();
        spyOn(component, 'skipAlert').and.callThrough();
        spyOn(component, 'onChangeLang').and.callThrough();
        spyOn(component, 'gotoFeedback').and.callThrough();
        spyOn(component, 'forceAlert').and.callThrough();
        spyOn(component, 'isNewUpdateAvailable').and.callThrough();
        spyOn(component, 'getCurrentLangInfoFromCache').and.callThrough();

        spyOn(component.navCtrl, 'push');

    });

    afterEach(() => {
        fixture.destroy();
        component = null;
    });

    it('should not be null component', () => {
        expect(fixture).not.toBeNull();
        expect(component).not.toBeNull();
    });

    it('ionViewDidEnter called initially', () => {
        component.ionViewDidEnter();
        expect(component.ionViewDidEnter).toHaveBeenCalled();
    });

    it('skipAlert called', () => {
        component.skipAlert();
        expect(component.skipAlert).toHaveBeenCalled();
    });

    it('onChangeLang called', () => {
        component.onChangeLang();
        expect(component.onChangeLang).toHaveBeenCalled();
    });

    it('gotoFeedback called', () => {
        component.gotoFeedback();
        expect(component.gotoFeedback).toHaveBeenCalled();
    });
    it('forceAlert called', () => {
        component.forceAlert();
        expect(component.forceAlert).toHaveBeenCalled();
    });


    // it('isNewUpdateAvailable called', () => {
    //     component.isNewUpdateAvailable();
    //     expect(component.isNewUpdateAvailable).toHaveBeenCalled();
    // });


    it('getCurrentLangInfoFromCache called', () => {
        component.getCurrentLangInfoFromCache();
        expect(component.getCurrentLangInfoFromCache).toHaveBeenCalled();
    });



    // it('If no language found from cache it should redirect to ApplicationStartupPage', () => {
    //     component.getCurrentLangInfoFromCache();
    //     expect(component.getCurrentLangInfoFromCache).toHaveBeenCalled();
    //     const result =null;
      
    // });
});
