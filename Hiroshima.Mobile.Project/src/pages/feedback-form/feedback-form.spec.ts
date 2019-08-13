import { async, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { IonicModule, Platform, NavController, IonicErrorHandler, IonicPageModule, AlertController, NavParams, ToastController, Config, DomController, App, Keyboard, Form, DeepLinker } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { DebugElement } from '@angular/core';
import { mockApp } from 'ionic-angular/util/mock-providers';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { IonicStorageModule } from '@ionic/storage';
import { CacheModule } from 'ionic-cache';
import { EventsMock, AlertControllerMock, ConfigMock } from 'ionic-mocks';
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
import { FeedbackFormPage } from './feedback-form';

describe('PassInformationPage Component: component created', () => {
    let fixture;
    let component;
    let title: DebugElement;
    let de: DebugElement;
    let el: HTMLElement;
    let pageComponent, SB: any;
    let navCtrl: NavMock;
    let userSeviceStub: UserSeviceStub;
    beforeEach(async(() => {



        TestBed.configureTestingModule({
            declarations: [FeedbackFormPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(FeedbackFormPage),
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
                Keyboard,
                Form,
                { provide: DeepLinker, useFactory: () => ConfigMock.instance() },
            ]
        }).compileComponents()
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(FeedbackFormPage);
        component = fixture.componentInstance;

        spyOn(component, 'ionViewDidEnter').and.callThrough();
        spyOn(component, 'onSubmit').and.callThrough();
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


    it('ionViewDidEnter called', () => {
        component.ionViewDidEnter();
        expect(component.ionViewDidEnter).toHaveBeenCalled();
    });
    it('backToPrevious called', () => {
        component.backToPrevious();
        expect(component.backToPrevious).toHaveBeenCalled();
    });
 



    it('onSubmit called', () => {
        component.onSubmit();
        expect(component.onSubmit).toHaveBeenCalled();
        // expect(component.SubmitFeedback).toBeUndefined();
        // const spy = spyOn(userSeviceStub, 'SubmitFeedback').and.returnValue(
        //     Observable.of(activePassModel)
        // );
        // expect(activePassModel).not.toBeNull();
        // // expect(activePassModel.length).toBeGreaterThan(0);
        // expect(spy.calls.any()).not.toBeNull();
    });

});
