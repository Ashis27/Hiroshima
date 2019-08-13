import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { PaymentFailurePage } from './payment-failure';

@NgModule({
  declarations: [
    PaymentFailurePage,
  ],
  imports: [
    IonicPageModule.forChild(PaymentFailurePage),
  ],
})
export class PaymentFailurePageModule {}
