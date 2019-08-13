import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { SeeAllPTOs } from './see-all-ptos';

@NgModule({
  declarations: [
    SeeAllPTOs,
  ],
  imports: [
    IonicPageModule.forChild(SeeAllPTOs),
  ],
})
export class PaymentSuccessPageModule {}
