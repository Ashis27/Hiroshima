import { AdminLayoutComponent } from "./adminlayout.component";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
// import { HTTP_INTERCEPTORS } from "@angular/common/http";
// import { ErrorInterceptor, JwtInterceptor } from "./_helpers";
import { AdminLayoutRoutes } from "./adminlayout.routing";
import { SidebarComponent } from "./sidenav/sidenav.component";
import { CUSTOM_ELEMENTS_SCHEMA } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { ModalModule } from 'ngx-bootstrap'
import { CreateAdminComponent } from "./pagecomponent/super-admin/create-admin/create-admin.component";
import { AppStartUpComponent } from "./pagecomponent/app-startup";
import { AppCurrencyComponent } from "./pagecomponent/app-currency/app-currency.component";
import { NECUserComponent } from "./pagecomponent/nec-admin/nec-user/nec-user.component";
import { CreateSuperAdminUser } from "./pagecomponent/nec-admin/create-superadmin/create-superadmin.component";
import { EditSuperAdminUser } from "./pagecomponent/nec-admin/edit-superadmin/edit-superadmin.component";
import { AppLanguageComponent } from "./pagecomponent/app-language/app-language.component";
import { EditAdminUserComponent } from "./pagecomponent/super-admin/edit-admin/edit-admin.component";
import { SuperAdminUserComponent } from "./pagecomponent/super-admin/superadmin-user/superadmin-user.component";
import { AppBookingInfoComponent } from "./pagecomponent/app-booking-info/app-booking-info.component";
import { DatepickerModule, BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { PTOComponent } from "./pagecomponent/pto";
import { PassComponent, CreatePassComponent, EditPassComponent, AddPassDescComponent } from "./pagecomponent/pass";
import { CreatePTOComponent } from "./pagecomponent/pto/create-pto/create-pto.component";
import { CreatePTODescComponent } from "./pagecomponent/pto/create-pto-desc/create-pto-desc.component";
import { EditPTOComponent } from "./pagecomponent/pto/edit-pto/edit-pto.component";
import { AppFeedbackComponent } from "./pagecomponent/app-feedback/app-feedback.component";
import { QRCodeConfigurationComponent } from './pagecomponent/qr-configuration';
import { AppTransactionInfoComponent } from 'src/app/hiroshimaadmin/adminlayout/pagecomponent/app-transaction-info/app-transaction-info.component';


@NgModule({
    imports: [
        RouterModule.forChild(AdminLayoutRoutes),
        FormsModule,
        CommonModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        BsDatepickerModule.forRoot()
    ],
    declarations: [
        AdminLayoutComponent,
        AppStartUpComponent,
        SidebarComponent,
        CreateAdminComponent,
        AppCurrencyComponent,
        NECUserComponent,
        CreateSuperAdminUser,
        EditSuperAdminUser,
        AppLanguageComponent,
        CreateAdminComponent,
        EditAdminUserComponent,
        SuperAdminUserComponent,
        AppBookingInfoComponent,
        PTOComponent,
        CreatePTOComponent,
        CreatePTODescComponent,
        EditPTOComponent,
        PassComponent,
        CreatePassComponent,
        EditPassComponent,
        AddPassDescComponent,
        AppFeedbackComponent,
        QRCodeConfigurationComponent,
        AppTransactionInfoComponent
    ],
    providers: [
    ],
    schemas:[CUSTOM_ELEMENTS_SCHEMA]
})

export class AdminLayoutComponentModule { }