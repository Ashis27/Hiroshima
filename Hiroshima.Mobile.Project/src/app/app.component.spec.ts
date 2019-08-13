import { async, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { IonicModule, Platform, NavController, IonicErrorHandler } from 'ionic-angular';
import { JwtInterceptor, ErrorInterceptor, HttpAPIInterceptor, LoadingScreenInterceptor } from '../providers/_helpers';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { MyApp } from './app.component';
import {
    PlatformMock,
    StatusBarMock,
    SplashScreenMock,
    NavMock
} from '../../test-config/mocks-ionic';
import { DebugElement, ErrorHandler } from '@angular/core';
import { By, BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppConfigurationServiceProvider } from '../providers/app.configuration.service';
import { HttpServiceProvider } from '../providers/http.services';
import { CustomValidationHandlerService } from '../shared/handler/customvalidation.service';
import { LanguageServiceProvider, LoadingScreenService, ConfigurationServiceProvider } from '../providers/_services';
import { StorageHandlerService } from '../shared/handler/storage.service';  
import { UserServiceProvider } from '../providers/_services/user.service';
import { IonicStorageModule } from '@ionic/storage';
import { CacheModule } from 'ionic-cache';


describe('MyApp Component: Root component created', () => {
    let fixture;
    let component;
    let title: DebugElement;
    let de: DebugElement;
    let el: HTMLElement;
    let pageComponent, SB: any;
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [MyApp],
            imports: [
                BrowserModule,
                HttpClientModule,
                IonicModule.forRoot(MyApp),
                IonicStorageModule.forRoot(),
                CacheModule.forRoot()
            ],
            providers: [
                { provide: StatusBar, useClass: StatusBarMock },
                { provide: SplashScreen, useClass: SplashScreenMock },
                { provide: Platform, useClass: PlatformMock, },
                { provide: NavController, useClass: NavMock, },
                { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
                { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
                { provide: HTTP_INTERCEPTORS, useClass: LoadingScreenInterceptor, multi: true },
                { provide: ErrorHandler, useClass: IonicErrorHandler },
                AppConfigurationServiceProvider,
                HttpServiceProvider,
                LanguageServiceProvider,  
                CustomValidationHandlerService,
                HttpAPIInterceptor,
                LoadingScreenService,
                UserServiceProvider,
                ConfigurationServiceProvider,
                StorageHandlerService,
            ]
        })
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(MyApp);
        component = fixture.componentInstance;



        spyOn(component, 'storeUniqueToken').and.callThrough();
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
        expect(MyApp).toBeTruthy();
        expect(component instanceof MyApp).toBe(true);
    });
  
    it('initialises with a root page as app start up', () => {
        expect(component['rootPage']).toBe("ApplicationStartUpPage");

    });



    it('storeUniqueToken called', () => {
   //     component.storeUniqueToken();
       
    });




    // //navigation testing
    // it('should redirect to dashboard once splash screen loaded', () => {
    //     let navCtrl = fixture.debugElement.injector.get(NavController);
    //     // spyOn(navCtrl, 'setRoot');
    //     fixture.detectChanges();
    //     expect(navCtrl.setRoot).toBe("DashboardPage");;
    // });
});
