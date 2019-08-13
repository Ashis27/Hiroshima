import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { PassInformationPage } from './pass-information';

@NgModule({
  declarations: [
    PassInformationPage,
  ],
  imports: [
    IonicPageModule.forChild(PassInformationPage),
  ],
})
export class PassInformationPageModule {}
