import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DashboardPage } from './dashboard';
import { InusepassPage } from './inusepass/inusepass';
import { HistorypassPage } from './historypass/historypass';
import { AvailablepassPage } from './availablepass/availablepass';
import { ActivepassPage } from './activepass/activepass';

@NgModule({
  declarations: [
    DashboardPage,
    ActivepassPage,AvailablepassPage,HistorypassPage,InusepassPage
  ],
  imports: [
    IonicPageModule.forChild(DashboardPage),
  ],
})
export class DashboardPageModule {}
