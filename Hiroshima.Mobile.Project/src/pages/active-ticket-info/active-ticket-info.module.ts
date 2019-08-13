import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ActiveTicketInfoPage } from './active-ticket-info';

@NgModule({
  declarations: [
    ActiveTicketInfoPage,
  ],
  imports: [
    IonicPageModule.forChild(ActiveTicketInfoPage),
  ],
})
export class ActiveTicketInfoPageModule {}
