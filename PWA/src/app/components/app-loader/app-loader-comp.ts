import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from "rxjs";
import { LoadingScreenService } from 'src/app/_services/componentservice/loading-screen';
import { debounceTime } from 'rxjs/operators';

/**
 * Generated class for the AppLoaderCompComponent component.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */
@Component({
  selector: 'app-loading-screen',
  templateUrl: './app-loader-comp.html',
  // styleUrls: ['./app-loader-comp.css']
})
export class AppLoaderCompComponent implements OnInit, OnDestroy {
  /**
Value of active loader status
*/
  loading: boolean = false;
  /**
Value of the loader status after subscription
*/
  loadingSubscription: Subscription;

  /**
* @ignore
*/
  constructor(private loadingScreenService: LoadingScreenService) {
  }

  /**
 * Initial entry point while page loading 
 * @example
 * ngOnInit()
 */
  ngOnInit() {
    this.loadingSubscription = this.loadingScreenService.loadingStatus.pipe(
      debounceTime(200)
    ).subscribe((value) => {
      this.loading = value;
    });
  }

  /**
 * call while page leaving
 * @example
 * ngOnDestroy()
 */
  ngOnDestroy() {
    this.loadingSubscription.unsubscribe();
  }

}