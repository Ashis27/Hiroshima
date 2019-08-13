import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DomSanitizer } from '@angular/platform-browser';
import { LoadingController } from 'ionic-angular';

/*
  Generated class for the Loading-screen  provider.
*/
@Injectable()
export class LoadingScreenService {
  /**
   * Value of the pending request
   */
  pendingRequests: number = 0;

  /**
   * value of the image from by pass security trust ttml
   */
  safeImage: any;

  /**
   * Value of the active loader 
   */
  _loadingCtrl: any;
  /**
   * Value of the loading status
   */
  private _loading: boolean = false;
  /**
   * Update the loading value whenever gets changed 
   */
  loadingStatus: BehaviorSubject<any> = new BehaviorSubject<any>(this._loading);

  /**
  *@ignore
   */
  constructor(private loadingCtrl: LoadingController, private sanitizer: DomSanitizer) {

  }

  /**
   * get active loader status
   */
  get loading(): boolean {
    return this._loading;
  }

  /**
   * set loader status
   */
  set loading(value) {
    this._loading = value;
    this.loadingStatus.next(value);
  }

  /**
   * Start loading while API call   
   */
  startLoading() {
    let imgContent = '<div class="spinner1"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>';
    this.safeImage = this.sanitizer.bypassSecurityTrustHtml(imgContent);
    this._loadingCtrl = this.loadingCtrl.create({
      spinner: 'hide',
      content: this.safeImage,
    });
    this._loadingCtrl.present();
  }

  /**
   * Stop loading while API gives the response
   */
  stopLoading() {
    this._loadingCtrl.dismiss().catch(() => { });
  }
}