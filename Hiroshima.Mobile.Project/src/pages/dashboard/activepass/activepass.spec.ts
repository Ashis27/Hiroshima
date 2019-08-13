import { async, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { IonicModule, Platform, NavController, IonicErrorHandler, IonicPageModule, AlertController, NavParams, ToastController, Config, DomController, App, Keyboard, Content } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { DebugElement, ElementRef } from '@angular/core';
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
import { ActivepassPage } from './activepass';
import { activePasses, activeMockPassModel } from '../../../shared/mockmodels/passmockmodel';
import { PassSeviceStub } from '../../../shared/mock/passservicestub';



describe('ActivepassPage Component: component created', () => {
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
    beforeEach(async(() => {


        TestBed.configureTestingModule({
            declarations: [ActivepassPage],
            imports: [
                HttpClientModule,
                IonicPageModule.forChild(ActivepassPage),
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
                //ElementRef,
                Content               
            ]
        }).compileComponents()
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ActivepassPage);
        component = fixture.componentInstance;
        alertCtrl = AlertControllerMock.instance();
        customMessageStub = fixture.debugElement.injector.get(CustomValidationHandlerService);
        passSeviceStub = fixture.debugElement.injector.get(PassServiceProvider);

        
        spyOn(component, 'ionViewDidEnter').and.callThrough();
        spyOn(component, 'getActivePasses').and.callThrough();
        spyOn(component, 'selectedPass').and.callThrough();
        spyOn(component, 'bookPass').and.callThrough();
        spyOn(component, 'doInfinite').and.callThrough();
       // spyOn(component, 'infiniteScroll').and.callThrough();
     //  spyOn(component, 'complete').and.callThrough();

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
        expect(ActivepassPage).toBeTruthy();
        expect(component instanceof ActivepassPage).toBe(true);
    });


    it('should call initial entry point ionViewDidEnter after page loading ', () => {
        component.ionViewDidEnter();
        expect(component.ionViewDidEnter).toHaveBeenCalled();
    });



    it('selectedPass called', () => {
        component.selectedPass();
        expect(component.selectedPass).toHaveBeenCalled();
    });


    it('bookPass called', () => {
        component.bookPass();
        expect(component.bookPass).toHaveBeenCalled();
    });

    it('selectedPass called', () => {
        component.selectedPass();
        expect(component.selectedPass).toHaveBeenCalled();
    });


    it('getActivePasses called and show no pass available if no passes are available', () => {
        component.getActivePasses();
        const noActivePasses = null;
        expect(component.getActivePasses).toHaveBeenCalled();
        const spy = spyOn(passSeviceStub, 'GetPasses').and.returnValue(
            Observable.of(noActivePasses)
        );

        expect(noActivePasses).toBeNull();
        component.isAvaileble = false;
        expect(spy.calls.any()).not.toBeNull();
    });
    it('getActivePasses called and show available passes if pass is available', () => {
        component.getActivePasses();
        expect(component.getActivePasses).toHaveBeenCalled();
        const spy = spyOn(passSeviceStub, 'GetPasses').and.returnValue(
            Observable.of(activePasses)
        );

        expect(activePasses).not.toBeNull();
        expect(activePasses.items.length).toBeGreaterThan(0);
        component.isAvaileble = true;
        component.paginatedPass = {
            items: activePasses.items,
            TotalCount: activePasses.TotalCount,
            TotalPages: activePasses.TotalPages,
            HasPreviousPage: activePasses.HasPreviousPage,
            HasNextPage: activePasses.HasNextPage
        };
        expect(component.paginatedPass.items.length).toBeGreaterThan(0);
        expect(component.paginatedPass.TotalCount).toBeGreaterThan(0);
        expect(component.paginatedPass.TotalPages).toBeGreaterThan(0);
        expect(spy.calls.any()).not.toBeNull();
    });


    // it('doInfinite called', () => {
    //     component.doInfinite();
    //     expect(component.doInfinite).toHaveBeenCalled();
    // });




    
//don't un-commnent these

    // it('should get active passes ', () => {
    //     const spy = spyOn(passSeviceStub, 'ActivatePass').and.returnValue(
    //         Observable.of(activePasses)
    //     );
    //     component.getActivePasses();
    //     expect(component.getActivePasses).toHaveBeenCalled();
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
