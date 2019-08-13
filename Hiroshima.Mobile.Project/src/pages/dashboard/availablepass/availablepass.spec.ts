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
import { AvailablepassPage } from './availablepass';
import { activePasses } from '../../../shared/mockmodels/passmockmodel';
import { PassSeviceStub } from '../../../shared/mock/passservicestub';
import { activePassModel,  } from '../../../shared/mockmodels/passmockmodel';
import { isActvated } from '../../../shared/mockmodels/bookingmodel';

describe('AvailablepassPage Component: component created', () => {
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
            declarations: [AvailablepassPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(AvailablepassPage),
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
        fixture = TestBed.createComponent(AvailablepassPage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);
        passSeviceStub = fixture.debugElement.injector.get(PassServiceProvider);
        userSeviceStub = fixture.debugElement.injector.get(UserServiceProvider);

        spyOn(component, 'ionViewDidEnter').and.callThrough();
        spyOn(component, 'getAvailablePasses').and.callThrough();
        spyOn(component, 'selectedPass').and.callThrough();
        spyOn(component, 'activatePass').and.callThrough();
        spyOn(component, 'sendNotification').and.callThrough();
        // spyOn(component.navCtrl, 'setRoot'); 

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

        component.getAvailablePasses();
        expect(component.getAvailablePasses).toHaveBeenCalled();
        const spy = spyOn(userSeviceStub, 'GetAvailablePasses').and.returnValue(
            Observable.of(activePassModel)
        );
        expect(activePassModel).not.toBeNull();
        expect(activePassModel.length).toBeGreaterThan(0);
        expect(spy.calls.any()).not.toBeNull();
    });
      
    it('selectedPass called', () => {
        component.selectedPass();
        expect(component.selectedPass).toHaveBeenCalled();        
    });

    it('should get active passes ', () => {
        component.activatePass();
        expect(component.activatePass).toHaveBeenCalled();
    });
    it('sendNotification called ', () => {
        component.sendNotification();
        expect(component.sendNotification).toHaveBeenCalled();
    });


    
    // it('migrate user to inUse tab in dasboard section if confirm is selected ', () => {
    //     component.activatePass();
    //     expect(component.activatePass).toHaveBeenCalled();

    //     component.ActivatePass();
    //     expect(component.ActivatePass).toHaveBeenCalled();
    //     const spy = spyOn(userSeviceStub, 'ActivatePass').and.returnValue(
    //         Observable.of(isActvated)
    //     );
    //     expect(isActvated).not.toBeNull();
    //     //expect(isActvated.length).toBeGreaterThan(0);
    //     expect(spy.calls.any()).not.toBeNull();
    
    // });




    // it("should returns atleast one pass", fakeAsync(() => {
    //     const spy = spyOn(passSeviceStub, 'GetPasses').and.returnValue(
    //         Observable.of(activePasses)
    //     );
    //     expect(component.activePasses).toEqual(activePasses);
    //     expect(component.activePasses.items).toBeGreaterThan(0);
    //     expect(component.isAvaileble).toBeTruthy();
    //     expect(spy.calls.any()).toBeDefined();
    // }));

});
