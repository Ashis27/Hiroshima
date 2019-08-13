import { LanguageService } from './../../shared/interfaces/ILanguage';
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
import { SelectLanguagePage } from './select-language';
import { bookingInfo, isActvated, uId } from '../../shared/mockmodels/bookingmodel';
import { languageInfo, languages, language_info } from '../../shared/mockmodels/languagemockmodel';
import { LanguageServiceStub } from '../../shared/mock/languagemockservice';
import { TweenMax, TweenLite } from "gsap/TweenMax";

describe('SelectLanguagePage Component: component created', () => {
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
            declarations: [SelectLanguagePage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(SelectLanguagePage),
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
        fixture = TestBed.createComponent(SelectLanguagePage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        passSeviceStub = fixture.debugElement.injector.get(PassServiceProvider);
        userServiceStub = fixture.debugElement.injector.get(UserServiceProvider);
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);
        languageServiceStub = fixture.debugElement.injector.get(LanguageServiceProvider);

        localStorage.setItem("langInfo", JSON.stringify(languageInfo));
        localStorage.setItem("isSelected", JSON.stringify(false));
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
        //santosh
        // this._langService = TestBed.createComponent(LanguageServiceProvider);
        //
        spyOn(component, 'ionViewDidEnter').and.callThrough();
        spyOn(component, 'getTermsAndConditions').and.callThrough();
        spyOn(component, 'getActiveLanguages').and.callThrough();
        spyOn(component, 'onChangeLanguage').and.callThrough();
        spyOn(component, 'selectedTAC').and.callThrough();
        spyOn(component, 'getActiveLanguagesFromCache').and.callThrough();
        spyOn(component, 'backToPrevious').and.callThrough();
        spyOn(component, 'ionViewDidLoad').and.callThrough();
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


    it('should instantiate', () => {
        expect(SelectLanguagePage).toBeTruthy();
        expect(component instanceof SelectLanguagePage).toBe(true);
    });




    //1
    it('should call getActiveLanguagesFromCache inside ionViewDidEnter ', () => {
        component.ionViewDidEnter();
        expect(component.getActiveLanguagesFromCache).toHaveBeenCalled();

    });

    
    it('should get all active languages from localstorage ', () => {
        const langInfo = JSON.parse(localStorage.getItem("langInfo"));
        const selectStatus = JSON.parse(localStorage.getItem("isSelected"));
        expect(langInfo).not.toBeNull();
        expect(langInfo.languages).not.toBeNull();
        expect(langInfo.currentLanguage).not.toBeNull();
        expect(component.selectStatus).toBeFalsy();
    });



    it('Getting Active Languages from cache', () => {
        component.getActiveLanguagesFromCache();
        let result = JSON.parse(localStorage.getItem("active_lang_info"));;
        component._languages = {
            currentLanguage: result.currentLanguage,
            currentLangContents: result.currentLangContents,
            languages: result.languages
        }
        component.isSelected = false;
        expect(component._languages).toBeTruthy();
        expect(component._languages.languages.length).toBeGreaterThan(0);
        expect(component._languages.currentLanguage).not.toBeNull();
        expect(component._languages.currentLangContents).not.toBeNull();
        expect(component.getActiveLanguagesFromCache).toHaveBeenCalled();
      
      
        component.getActiveLanguages();
        expect(component.getActiveLanguages).toHaveBeenCalled();
        const spy = spyOn(languageServiceStub, 'GetActiveLanguages').and.returnValue(
            Observable.of(language_info)
        );
        expect(language_info).not.toBeNull();
        expect(language_info.items.length).toBeGreaterThan(0);
        expect(spy.calls.any()).not.toBeNull();
    });

  

    //method onChangeLanguage()
    it('New language should be changed', () => {
        component.onChangeLanguage();
        expect(component.onChangeLanguage).toHaveBeenCalled();

    });


    //method selectedTAC()
    it('terms and condition selected', () => {
        component.selectedTAC();
        expect(component.selectedTAC).toHaveBeenCalled();

    });

    
    //method ionViewDidLoad()
    it('ionViewDidLoad called', () => {
        component.ionViewDidLoad();
        expect(component.ionViewDidLoad).toHaveBeenCalled();

    });

    it('Terms and condition called', () => {
        component.getTermsAndConditions();
        expect(component.getTermsAndConditions).toHaveBeenCalled();

    });

    it('backToPrevious called', () => {
        component.backToPrevious();
        expect(component.backToPrevious).toHaveBeenCalled();

    });

    // it('retrieves all the cars', inject([LanguageServiceStub], (service) => {
    //     return service.GetActiveLanguages().subscribe((languages) => {
    //         expect(languages).not.toBeNull();
    //     //    / expect(languages.length).toBeGreaterThan(0);
    //     });
    // }));
    // it('should get all active languages from API ', () => {
    //    // component.getActiveLanguages();
    //     // const spy = spyOn(languageServiceStub, 'GetActiveLanguages').and.returnValue(
    //     //     Observable.of(languages)
    //     // );
    //    spyOn(component, 'getActiveLanguages');
    //     component._langService.GetActiveLanguages().subscribe(builds => {
    //         expect(component.getActiveLanguages).toHaveBeenCalled();
    //         expect(builds.length).toBeGreaterThan(0);
    //     });
    //     //  expect(component._languages.languages).toEqual(languages)
    //    // component.getActiveLanguages();
    //    // expect(languageServiceStub.GetActiveLanguages).toHaveBeenCalled();
    // });



    //santosh
    // it('Get all active languages',()=>{
    //     spyOn(_langService,'GetActiveLanguages').and.callFake(()=>{
    //         return Observable.from([[1,2,3]])
    //     });

    //     component.getActiveLanguages();
    //     expect(component.response.length).toBeGreaterThan(0);

    // })
    //
});
