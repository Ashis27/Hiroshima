import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { finalize } from "rxjs/operators";
import { LoadingScreenService } from "../_services/loading-screen";

/**
Loading interceptor
*/
@Injectable()
export class LoadingScreenInterceptor implements HttpInterceptor {

     /**
     * Value of the active HTTP request
     */
    activeRequests: number = 0;
    /**
     * URLs for which the loading screen should not be enabled
     */
    skippUrls = [
    ];

    /**
    *@ignore
    */
    constructor(private loadingScreenService: LoadingScreenService) {
    }

    /**
    * @example
    * intercept(request,next)
    * @param request  HttpRequest
    * @param next HttpHandler
    * @returns {void}
    */
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //Check network connection, other wise throw offline exception
        if (navigator.onLine) {
            //Get request params to use loader or not while fetching data
            let displayLoadingScreen = request.params.get('status');
            // if (!request.params.get('status'))
            //     displayLoadingScreen = false;
            // for (const skippUrl of this.skippUrls) {
            //     if (new RegExp(skippUrl).test(request.url)) {
            //         displayLoadingScreen = false;
            //         break;
            //     }
            // }
            if (displayLoadingScreen) {
                if (this.activeRequests === 0)
                    this.loadingScreenService.startLoading();
                this.activeRequests++;
                return next.handle(request).pipe(
                    finalize(() => {
                        this.activeRequests--;
                        if (this.activeRequests === 0)
                            this.loadingScreenService.stopLoading();
                    })
                )
            }
            else
                return next.handle(request);
        }
        else
            return Observable.throw("You are OFFLINE. Please check your network connection!");
    };
}
