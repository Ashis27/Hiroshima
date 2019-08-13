import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { DomSanitizer } from '@angular/platform-browser';
import { LoadingController } from 'ionic-angular';

/**
 Error interceptor
*/
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    /**
    *@ignore
     */
    constructor(private loadingCtrl: LoadingController, private sanitizer: DomSanitizer) { }
    
    /**
     * @example
     * intercept(request,next)
     * @param request  HttpRequest
     * @param next HttpHandler
     *@returns {void}
     */
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //Check network connection, other wise throw offline exception
        if (navigator.onLine) {
            return next.handle(request).pipe(catchError(err => {
                if ([401, 403].indexOf(err.status) !== -1)
                    // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
                    return Observable.throw("SessionExpired");
                if (err.status === 400)
                    return Observable.throw(err.error.message || err.error);
                // if (!err.error || !err.error.Message)
                //     return Observable.throw("DefaultErrorMessage");
                // const error = err.error.Message || err.statusText;
                return Observable.throw("DefaultErrorMessage");
            })).finally(() => { });
        }
        else
            return Observable.throw("OfflineMessage");
    }
}