import { Component, Injector } from '@angular/core';
import { IonicPage, NavController, NavParams } from 'ionic-angular';
import { first } from 'rxjs/operators';
import { CustomValidationHandlerService } from '../../../shared/handler/customvalidation.service';
import { SearchParams, PassDescriptionModel, PassActivePTOModel, PassModel } from '../../../shared/models';
import { LanguageHandlerService } from '../../../shared/handler/language.service';
import moment from 'moment';
import { UserServiceProvider } from '../../../providers/_services/user.service';
import { ApiServicesHandlerService } from '../../../shared/handler/app.constant';

/**
 * Generated class for the InusepassPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

//@IonicPage()
@Component({
  selector: 'page-inusepass',
  templateUrl: 'inusepass.html',
})
export class InusepassPage extends LanguageHandlerService {

  expiryObject: any;
  currentDateTime: any;
  passExpiryTime: any;
  date: any;
  date1: any;
  theNoOfDaysRemaining: any;
  theNoOfHoursRemaining: any;
  theNoOfMinutesRemaining: any;
  theNoOfSecondsRemaining: any;

  dateTimePart: any;
  dateParts: any;
  timeParts: any;
  dateTimePart1: any;
  dateParts1: any;
  timeParts1: any;
  /**
      Value of the PassDescriptionModel which contain pass decription information
    */
  passDesc: PassDescriptionModel[] = [];

  /**
  Value of the PassActivePTOModel which define how many PTOs are available for a pass
 */
  passActivePTOs: PassActivePTOModel[]

  /**
 Value of the PassModel which contain pass information
 */
  activePasses: PassModel[] = [];

  /**
 Value of the active pass list from API along with paginated information
 */
  paginatedPass = {
    items: this.activePasses,
    TotalCount: 0,
    TotalPages: 0,
    HasPreviousPage: false,
    HasNextPage: false
  }


  /**
 Value of SearchParams model for pagination 
 */
  searchParams: SearchParams = { pageIndex: 1, pageSize: 5, lang: 0 };

  /**
 Value of available pass status 
 */
  isAvaileble: boolean = true;

  /**
  *@ignore
   */
  constructor(injector: Injector, public navCtrl: NavController,
    public navParams: NavParams,
    private _custom_message: CustomValidationHandlerService, private userService: UserServiceProvider) {
    super(injector)
    this.getInUsePasses(true);
  }


  /**
  * Initial entry point to get all in use passes while entering the page
  * @example
  * ionViewDidEnter()
  */
  ionViewDidEnter() {
  }

  /**
 * To get all in use passes
 * @example
 * getInUsePasses(status)
 * @param {boolean} status True | False (Use loader or not)
 * @returns list of  in use passes
 */
  getInUsePasses(status) {
    // this.searchParams.deviceId = "12345yetegdd";
    this.searchParams.deviceId = JSON.parse(localStorage.getItem(ApiServicesHandlerService._cache_key_value.userDeviceId));

    this.userService.GetInUsePasses(this.searchParams, status)
      .pipe(first())
      .subscribe(response => {
        if (!response || response.items.length == 0)
          if (this.paginatedPass.items.length == 0)
            return this.isAvaileble = false;
        //Convert Date
        if (response && response.items.length > 0) {
          //increase pagination index for next available information
          this.searchParams.pageIndex = this.searchParams.pageIndex + 1;
          response.items.forEach(element => {
            if (element.passInformation)
              element.passInformation.passExpiredDate = moment(element.passInformation.passExpiredDate).format("MM/DD/YYYY");
          });
        }
        this.paginatedPass = {
          items: this.paginatedPass.items.concat(response.items),
          TotalCount: response.totalCount,
          TotalPages: response.totalPages,
          HasPreviousPage: response.hasPreviousPage,
          HasNextPage: response.hasNextPage
        };

        //below two lines added for sending data to remainingTime() to calcualte time left for the pass to expire
        //  this.expiryObject = response;
        this.remainingTime();
        console.log(this.paginatedPass);
        //end

        this.isAvaileble = true;
      },
        error => {
          this.isAvaileble = false;
          this._custom_message_handler.errorMessage(this.lang(error));
        });
  }


  remainingTime() {
    console.log(this.paginatedPass);

    //getting current date time using momentum
    var currentDateTime = moment().format('DD/MM/YYYY HH:mm:ss');
    console.log("Current date time is " + currentDateTime);


    //convert current date to time stamp
    this.dateTimePart = currentDateTime.split(' ');
    this.dateParts = this.dateTimePart[0].split('/');
    this.timeParts = this.dateTimePart[1].split(':');
    this.date = new Date(this.dateParts[2], parseInt(this.dateParts[1], 10) - 1, this.dateParts[0], this.timeParts[0], this.timeParts[1]);
    //end

    for (var i = 0; i < this.paginatedPass.items.length; i++) {
      //getting pass expiry date and time from object
      var passExpiryTime = this.paginatedPass.items[i]['qrCode'].activatedPassExpiredDate;
      // console.log(passExpiryTime)


      // converting pass expiry date and time to standard format using momentum
      var passExpiryDateTime = moment(passExpiryTime).format('DD/MM/YYYY HH:mm:ss');
      console.log("pass " + [i] + " expiry date time is " + passExpiryDateTime);


      //convertpass expiry date and time to time stamp
      this.dateTimePart1 = passExpiryDateTime.split(' ');
      this.dateParts1 = this.dateTimePart1[0].split('/');
      this.timeParts1 = this.dateTimePart1[1].split(':');
      this.date1 = new Date(this.dateParts1[2], parseInt(this.dateParts1[1], 10) - 1, this.dateParts1[0], this.timeParts1[0], this.timeParts1[1]);
      //end


      //difference time between two time stamp
      // get total seconds between the times
      var delta = Math.abs(this.date1.getTime() - this.date.getTime()) / 1000;
      var days = Math.floor(delta / 86400);
      delta -= days * 86400;
      console.log(days + " days");

      // calculate (and subtract) whole hours
      var hours = Math.floor(delta / 3600) % 24;
      delta -= hours * 3600;

      console.log(hours + " hours");
      // calculate (and subtract) whole minutes
      var minutes = Math.floor(delta / 60) % 60;
      delta -= minutes * 60;

      console.log(minutes + " minutes");

      //end

      // //calculating difference between two dates using momentum
      // var diffr = moment.utc(moment(currentDateTime, "DD/MM/YYYY HH:mm:ss").diff(moment(passExpiryDateTime, "DD/MM/YYYY HH:mm:ss"))).format(" HH:mm:ss");

      // console.log("Time Remaning using momentum" + diffr);     

      this.paginatedPass.items[i]["theNoOfDaysRemaining"] = days;
      this.paginatedPass.items[i]["theNoOfHoursRemaining"] = hours;
      this.paginatedPass.items[i]["theNoOfMinutesRemaining"] = minutes;
    }
  }
  /**
 * While page scrolling, it will get the active passes for selected page
* @example
 * doInfinite()
 * @param infiniteScroll 
 */
  doInfinite(infiniteScroll) {
    setTimeout(() => {
      this.getInUsePasses(false);
      infiniteScroll.complete();
    }, 500);
  }

  /**
  * To get booked ticket information. Redirect to ticket information page
  * @example
  * selectedPass(information)
  * @param {PassModel} info Pass information
  * @returns void
  */
  selectedPass(info) {
    this.navCtrl.push('ActiveTicketInfoPage', {
      selectedPassInfo: info,
      pageStatus: "inUse",
      

    })
  }

}
