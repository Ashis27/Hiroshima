import { Injectable } from '@angular/core';
import { HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { delay, mergeMap, materialize, dematerialize } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { of } from 'rxjs/observable/of';

/**
 API backend interceptor
*/
@Injectable()
export class HttpAPIInterceptor implements HttpInterceptor {

    /**
  * @example
  * intercept(request,next)
  * @param request  HttpRequest
  * @param next HttpHandler
  *@returns {void}
  */
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // wrap in delayed observable to simulate server api call
        return of(null).pipe(mergeMap(() => {
            alert();
            // pass through any requests not handled above
            if (navigator.onLine)
                return next.handle(request);
            else
                return Observable.throw({ status: 400, error: { Message: "You are OFFLINE. Please check your network connection!" } });
        }));
    }
}

/**
 * Use ApiBackendProvider to work as a middleware to overwrite for customization
 */
export let ApiBackendProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: HttpAPIInterceptor,
    multi: true
};