import { LanguageService } from './../../shared/interfaces/ILanguage';
import { async, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { IonicModule, Platform, IonicErrorHandler, IonicPageModule, AlertController, NavParams, ToastController, Config, DomController, App, Keyboard, NavController, ViewController, GestureController, MenuController } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { DebugElement } from '@angular/core';
import { mockApp } from 'ionic-angular/util/mock-providers';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { IonicStorageModule } from '@ionic/storage';
import { CacheModule } from 'ionic-cache';
import { AlertControllerMock, ViewControllerMock } from 'ionic-mocks';
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
import { PassSeviceStub } from '../../shared/mock/passservicestub';
import { ApplicationStartUpPage } from './app-startup';
import { LanguageServiceStub } from '../../shared/mock/languagemockservice';
import { TransitionController } from 'ionic-angular/transitions/transition-controller';
import { languageInfo, languages, language_info, languageMockContents } from '../../shared/mockmodels/languagemockmodel';

describe('ApplicationStartUpPage Component: component created', () => {
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

    beforeEach(async(() => {


        TestBed.configureTestingModule({
            declarations: [ApplicationStartUpPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(ApplicationStartUpPage),
                IonicStorageModule.forRoot(),
                CacheModule.forRoot()
            ],
            providers: [
                { provide: NavParams, useClass: NavParamsMock },
                { provide: Platform, useClass: PlatformMock, },
                { provide: NavController, useClass: NavMock, },
                { provide: ViewController, useFactory: () => ViewControllerMock.instance() },
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
                GestureController,
                MenuController,
                TransitionController,
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
        fixture = TestBed.createComponent(ApplicationStartUpPage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        passSeviceStub = fixture.debugElement.injector.get(PassServiceProvider);
        userServiceStub = fixture.debugElement.injector.get(UserServiceProvider);
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);
        languageServiceStub = fixture.debugElement.injector.get(LanguageServiceProvider);



        localStorage.setItem("langInfo", JSON.stringify(languageInfo));
        localStorage.setItem("isTACselected", JSON.stringify(false));
        let lang_info = {
            currentLanguage:
            {
                displayName: "en",
                icon: "usa.svg",
                id: 1,
                isActive: true,
                isDefault: true,
                name: "English"
            },
            currentLangContents: {
                Action: "Action",
                Activate: "Activate",
                Active: "Active"
            },
            languages: [
                {
                    displayName: "en",
                    icon: "usa.svg",
                    id: 1,
                    isActive: true,
                    isDefault: true,
                    name: "English"
                },
                {
                    displayName: "jp",
                    icon: "jp.svg",
                    id: 2,
                    isActive: false,
                    isDefault: false,
                    name: "Japaneese"
                }
            ]
        };
        localStorage.setItem("active_lang_info", JSON.stringify(lang_info));


        spyOn(component, 'ionViewDidLoad').and.callThrough();
        spyOn(component, 'ionViewDidEnter').and.callThrough();
        spyOn(component, 'getActiveLanguages').and.callThrough();
        spyOn(component, 'getCurrentLangInfoFromCache').and.callThrough();


    });



    afterEach(() => {
        fixture.destroy();
        component = null;
    });

    it('should not be null component', () => {
        expect(fixture).not.toBeNull();
        expect(component).not.toBeNull();
    });

    it('ionViewDidLoad called ', () => {
        component.ionViewDidLoad();
        expect(component.ionViewDidLoad).toHaveBeenCalled();
    });



    
    it('ionViewDidEnter called and check if Terms and condition has been cheked ', () => {
        component.ionViewDidEnter();
        expect(component.ionViewDidEnter).toHaveBeenCalled();
        component.isTACselected = true;
        component.getCurrentLangInfoFromCache();
        let result = JSON.parse(localStorage.getItem("active_lang_info"));;
        component._languages = {
            currentLanguage: result.currentLanguage,
            currentLangContents: result.currentLangContents,
            languages: result.languages
        }
        expect(component._languages).toBeTruthy();
        expect(component._languages.languages.length).toBeGreaterThan(0);
        expect(component._languages.currentLanguage).not.toBeNull();
        expect(component._languages.currentLangContents).not.toBeNull();
        expect(component.getCurrentLangInfoFromCache).toHaveBeenCalled();

        const spy1 = spyOn(languageServiceStub, 'GetActiveLanguageContents').and.returnValue(
            Observable.of(languageMockContents)
        );
        expect(languageMockContents).not.toBeNull();
        expect(languageMockContents.culture.lang).toEqual("en");
        expect(languageMockContents.languages).not.toBeNull();
        expect(spy1.calls.any()).not.toBeNull();
    });




    it('ionViewDidEnter called and check if Terms and condition has been unchecked ', () => {
        component.ionViewDidEnter();
        expect(component.ionViewDidEnter).toHaveBeenCalled();
      
        component.isTACselected = false;
        component.getActiveLanguages();
        expect(component.getActiveLanguages).toHaveBeenCalled();
       
        const spy = spyOn(languageServiceStub, 'GetActiveLanguages').and.returnValue(
            Observable.of(language_info)
        );
        
        expect(language_info).not.toBeNull();
        expect(language_info.items.length).toBeGreaterThan(0);
        expect(spy.calls.any()).not.toBeNull();

        const spy1 = spyOn(languageServiceStub, 'GetActiveLanguageContents').and.returnValue(
            Observable.of(languageMockContents)
        );
        expect(languageMockContents).not.toBeNull();
        expect(languageMockContents.culture.lang).toEqual("en");
        expect(languageMockContents.languages).not.toBeNull();
        expect(spy1.calls.any()).not.toBeNull();
    });




});
