import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { RiderInformationPage } from './rider-information';

@NgModule({
  declarations: [
    RiderInformationPage,
  ],
  imports: [
    IonicPageModule.forChild(RiderInformationPage),
  ],
})
export class RiderInformationPageModule {}
