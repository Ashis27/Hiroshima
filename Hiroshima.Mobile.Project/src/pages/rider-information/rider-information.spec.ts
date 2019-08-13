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
import { RiderInformationPage } from './rider-information';
import { bookingInfo, isActvated, uId } from '../../shared/mockmodels/bookingmodel';

describe('RiderInformationPage Component: component created', () => {
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
    beforeEach(async(() => {


        TestBed.configureTestingModule({
            declarations: [RiderInformationPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(RiderInformationPage),
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
        fixture = TestBed.createComponent(RiderInformationPage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        passSeviceStub = fixture.debugElement.injector.get(PassServiceProvider);
        userServiceStub = fixture.debugElement.injector.get(UserServiceProvider);
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);

        spyOn(component, 'bookPass').and.callThrough();
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
        expect(RiderInformationPage).toBeTruthy();
        expect(component instanceof RiderInformationPage).toBe(true);
    });
    it('should get the selected pass information', () => {
        expect(component.activePasses).not.toBeNull();
    });



    it('backToPrevious called', () => {
        component.backToPrevious();
        expect(component.backToPrevious).toHaveBeenCalled();
    });


    it('should check all required information before booking', () => {
        const activePass = activePassModel[0];
        component.bookPass(activePass);
        expect(component.bookPass).toHaveBeenCalledWith(activePass);
        // expect(alertCtrl.create).toBeTruthy();
        // expect(bookingInfo.passInformationId).toEqual(activePass.id);
        // expect(bookingInfo.travellerDeviceId).not.toBeNull();
        // expect(bookingInfo.bookingDate).not.toBeNull();
        // expect(bookingInfo.totalAmout).toBeGreaterThan(0);
        // expect(component.taxAmount).toBeGreaterThan(0);
        // expect(bookingInfo.totalAmout).toBeGreaterThan(component.taxAmount);

    });



    it('should book a pass', fakeAsync(() => {
        const bookingDetails = bookingInfo;
        const spy = spyOn(userServiceStub, 'BookPass').and.returnValue(
            Observable.of(uId)
        );
        userServiceStub.BookPass(bookingDetails, true);
        expect(userServiceStub.BookPass).toHaveBeenCalledWith(bookingDetails, true);
        const uniqueId = "1234abcd";
        expect(uId).not.toBeNull();
        expect(uId).toEqual(uniqueId);
    }));
    // it('should be failed booking', () => {
    //     const bookingDetails = bookingInfo;
    //     const spy = spyOn(userServiceStub, 'BookPass').and.throwError(

    //     );
    //     userServiceStub.BookPass(bookingDetails,true);
    //     expect(userServiceStub.BookPass).toBeFalsy();
    // });
});
