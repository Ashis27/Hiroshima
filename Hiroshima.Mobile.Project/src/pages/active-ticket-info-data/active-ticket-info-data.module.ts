import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ActiveTicketInfoDataPage } from './active-ticket-info-data';

@NgModule({
  declarations: [
    ActiveTicketInfoDataPage,
  ],
  imports: [
    IonicPageModule.forChild(ActiveTicketInfoDataPage),
  ],
})
export class ActiveTicketInfoDataPageModule {}
