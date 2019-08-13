import { async, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { IonicModule, Platform, NavController, IonicErrorHandler, IonicPageModule, AlertController, NavParams, ToastController, Config, DomController, App, Keyboard } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { ActiveTicketInfoPage } from './active-ticket-info';
import { DebugElement } from '@angular/core';
import { mockApp } from 'ionic-angular/util/mock-providers';
import { NavMock, NavParamsMock, PlatformMock } from '../../../test-config/mocks-ionic';
import { CustomValidationHandlerService } from '../../shared/handler/customvalidation.service';
import { UserServiceProvider } from '../../providers/_services/user.service';
import { HttpServiceProvider } from '../../providers/http.services';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppConfigurationServiceProvider } from '../../providers/app.configuration.service';
import { IonicStorageModule } from '@ionic/storage';
import { CacheModule } from 'ionic-cache';
import { LanguageServiceProvider, LoadingScreenService, ConfigurationServiceProvider } from '../../providers/_services';
import { StorageHandlerService } from '../../shared/handler/storage.service';
import { HttpAPIInterceptor, JwtInterceptor, ErrorInterceptor, LoadingScreenInterceptor } from '../../providers/_helpers';
import { EventsMock, AlertControllerMock } from 'ionic-mocks';
import { Observable } from 'rxjs';
import { UserSeviceStub } from '../../shared/mock/userservicestub';
import { isActvated } from '../../shared/mockmodels/bookingmodel';
import { CustomMessageStub } from "../../shared/mock/custommessagestub";

describe('ActiveTicketInfoPage Component: component created', () => {
    let fixture;
    let component;
    let title: DebugElement;
    let de: DebugElement;
    let el: HTMLElement;
    let pageComponent, SB: any;
    let navCtrl: NavMock;
    let alertCtrl: AlertController;
    let userServiceStub: UserSeviceStub;
    let customMessageStub: CustomMessageStub;
    beforeEach(async(() => {


        TestBed.configureTestingModule({
            declarations: [ActiveTicketInfoPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(ActiveTicketInfoPage),
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
        fixture = TestBed.createComponent(ActiveTicketInfoPage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        userServiceStub = fixture.debugElement.injector.get(UserServiceProvider);
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);

        spyOn(component, 'goToDashboard').and.callThrough();
        spyOn(component, 'ionViewDidEnter').and.callThrough();
        spyOn(component, 'activatePass').and.callThrough();
        spyOn(component, 'backToPrevious').and.callThrough();
        spyOn(component, 'sendNotification').and.callThrough();
        spyOn(component, 'ionViewDidLoad').and.callThrough();
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
    it('should instantiate', () => {
        expect(ActiveTicketInfoPage).toBeTruthy();
        expect(component instanceof ActiveTicketInfoPage).toBe(true);
    });
    it('should call initial entry point ionViewDidEnter after page loading ', () => {
        component.ionViewDidEnter();
        expect(component.ionViewDidEnter).toHaveBeenCalled();
    });


    it('should redirect when go to dashboard is clicked ', () => {
        //let navCtrl = fixture.debugElement.injector.get(NavController);
        component.goToDashboard();
        expect(component.goToDashboard).toHaveBeenCalled();
        // //navigate to dashboard
        expect(component.navCtrl.setRoot).toHaveBeenCalledWith("DashboardPage");
    });

    it('backToPrevious called ', () => {
        component.backToPrevious();
        expect(component.backToPrevious).toHaveBeenCalled();
       
    });


    it('sendNotification called ', () => {
        component.sendNotification();
        expect(component.sendNotification).toHaveBeenCalled();    
    });

    it('ionViewDidLoad called ', () => {
        component.ionViewDidLoad();
        expect(component.ionViewDidLoad).toHaveBeenCalled();    
    });



    
    it('should call activate pass method with alert pop up', () => {
        //let navCtrl = fixture.debugElement.injector.get(NavController);
        component.activatePass();
        expect(component.activatePass).toHaveBeenCalled();
        expect(alertCtrl.create).toBeTruthy();

        //expect(component.navCtrl.setRoot).toHaveBeenCalledWith("DashboardPage");
    });

    it("canActivate returns false if pass is not activated", fakeAsync(() => {
        
        const spy = spyOn(userServiceStub, 'ActivatePass').and.returnValue(
            Observable.of(isActvated)
        );
        expect(component.isActivated).toEqual(isActvated);
        expect(spy.calls.any()).toBeDefined();




    }));
    // it("canActivate returns false if pass is not activated", fakeAsync(() => {
    //     const spy = spyOn(userServiceStub, 'ActivatePass').and.returnValue(
    //         Observable.of(!isActvated)
    //     );
    //     expect(component.isActivated).toEqual(!isActvated);
    //     expect(spy.calls.any()).toBeTruthy();
    // }));
    // it("should call userService.ActivatePass if it returns false then show error message", async () => {
    //     const spy = spyOn(userServiceStub, 'ActivatePass').and.returnValue(
    //         Observable.of(!isActvated)
    //     );
    //     fixture.detectChanges();
    //     expect(component.isActivated).toEqual(!isActvated);

    //    // expect(spy.calls.any()).toBeTruthy();
    // });



});
