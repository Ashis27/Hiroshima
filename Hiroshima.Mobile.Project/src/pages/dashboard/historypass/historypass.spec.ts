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
import { HistorypassPage } from './historypass';
import { activePasses, activeMockPassModel } from '../../../shared/mockmodels/passmockmodel';
import { PassSeviceStub } from '../../../shared/mock/passservicestub';
import { activePassModel, } from '../../../shared/mockmodels/passmockmodel';
describe('HistorypassPage Component: component created', () => {
    let fixture;
    let component;
    let title: DebugElement;
    let de: DebugElement;
    let el: HTMLElement;
    let pageComponent, SB: any;
    let navCtrl: NavMock;
    let alertCtrl: AlertController;
    let customMessageStub: CustomMessageStub;
    let passSeviceStub: PassSeviceStub;
    let userSeviceStub: UserSeviceStub;
    beforeEach(async(() => {


        TestBed.configureTestingModule({
            declarations: [HistorypassPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(HistorypassPage),
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


        fixture = TestBed.createComponent(HistorypassPage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);
        passSeviceStub = fixture.debugElement.injector.get(PassServiceProvider);

        spyOn(component, 'ionViewDidEnter').and.callThrough();
        spyOn(component, 'goToPassinfoPage').and.callThrough();
        spyOn(component, 'getExpiredPasses').and.callThrough();
        spyOn(component, 'selectedPass').and.callThrough();
        spyOn(component, 'doInfinite').and.callThrough();
        spyOn(component.navCtrl, 'setRoot');

    });

    afterEach(() => {
        fixture.destroy();
        component = null;
    });

    it('should not be null component', () => {
        expect(fixture).not.toBeNull();
        expect(component).not.toBeNull();
    });

    it('should call initial entry point ionViewDidEnter after page loading ', () => {
        component.ionViewDidEnter();
        expect(component.ionViewDidEnter).toHaveBeenCalled();
    });

    it('getExpiredPasses called ', () => {
        component.getExpiredPasses();
        expect(component.getExpiredPasses).toHaveBeenCalled();


        // const spy = spyOn(userSeviceStub, 'GetExpiredPasses').and.returnValue(
        //     Observable.of(activeMockPassModel)
        // );
        // expect(activeMockPassModel).not.toBeNull();
        // expect(activeMockPassModel.items.length).toBeGreaterThan(0);
        // expect(spy.calls.any()).not.toBeNull();
    });

    // it('doInfinite called', () => {
    //     component.doInfinite();
    //     component.setTimeout();
    //     component.complete();
    //     expect(component.doInfinite).toHaveBeenCalled();
    //     expect(component.setTimeout).toHaveBeenCalled();
    // });


    it('goToPassinfoPage called', () => {
        component.goToPassinfoPage();
        expect(component.goToPassinfoPage).toHaveBeenCalled();
    });


    it('selectedPass called', () => {
        component.selectedPass();
        expect(component.selectedPass).toHaveBeenCalled();
    });


});
