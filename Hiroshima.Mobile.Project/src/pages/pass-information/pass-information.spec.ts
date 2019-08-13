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
import { activePasses } from '../../shared/mockmodels/passmockmodel';
import { PassSeviceStub } from '../../shared/mock/passservicestub';
import { PassInformationPage } from './pass-information';

describe('PassInformationPage Component: component created', () => {
    let fixture;
    let component;
    let title: DebugElement;
    let de: DebugElement;
    let el: HTMLElement;
    let pageComponent, SB: any;
    let navCtrl: NavMock;
    beforeEach(async(() => {


        
        TestBed.configureTestingModule({
            declarations: [PassInformationPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(PassInformationPage),
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
                AppConfigurationServiceProvider,
                HttpServiceProvider,
                LanguageServiceProvider,
                HttpAPIInterceptor,
                LoadingScreenService,
                ConfigurationServiceProvider,
                StorageHandlerService,
                Config,
                DomController,
                Keyboard
            ]
        }).compileComponents()
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PassInformationPage);
        component = fixture.componentInstance;
        spyOn(component, 'bookPass').and.callThrough();
        spyOn(component, 'ionViewDidLoad').and.callThrough();
        spyOn(component, 'backToPrevious').and.callThrough();
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
        expect(PassInformationPage).toBeTruthy();
        expect(component instanceof PassInformationPage).toBe(true);
    });
    it('should get the selected pass information', () => {
        expect(component.activePasses).not.toBeNull();
    });

    
    it('should redirect rider information screen', () => {
        component.bookPass();
        expect(component.bookPass).toHaveBeenCalled();
        // //navigate to dashboard
        expect(component.navCtrl.push).toHaveBeenCalledWith("RiderInformationPage",{selectedPassInfo:component.activePasses});
    });


    
    it('ionViewDidLoad called', () => {
        component.ionViewDidLoad();
        expect(component.ionViewDidLoad).toHaveBeenCalled();
    });

    
    it('backToPrevious called', () => {
        component.backToPrevious();
        expect(component.backToPrevious).toHaveBeenCalled();
    });



});
