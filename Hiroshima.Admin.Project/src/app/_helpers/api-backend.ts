import { Injectable } from '@angular/core';
import { HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { delay, mergeMap, materialize, dematerialize } from 'rxjs/operators';

import { User } from 'src/app/_models';

/**
 * API backend interceptor
 */
@Injectable()
export class APIBackendInterceptor implements HttpInterceptor {

    /**
     *Make API call with selected HTTP request
     *@example
     *intercept()
     *@returns {void}
     *///HttpEvent
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
        // wrap in delayed observable to simulate server api call
        return of(null).pipe(mergeMap(() => {
            // pass through any requests not handled above
            if (navigator.onLine)
                return next.handle(request);
            else
                return throwError({ status: 400, error: { Message: "OfflineMessage" } });
        }))
            // call materialize and dematerialize to ensure delay even if an error is thrown (https://github.com/Reactive-Extensions/RxJS/issues/648)
            .pipe(materialize())
            .pipe(delay(500))
            .pipe(dematerialize());

        // private helper functions

        function ok(body) {
            return of(new HttpResponse({ status: 200, body }));
        }

        function unauthorised() {
            return throwError({ status: 401, error: { Message: 'Unauthorised' } });
        }

        function error(message) {
            return throwError({ status: 400, error: { Message: message } });
        }
    }
}

/**
 * Overwrite HTTP interceptor
 */
export let fakeBackendProvider = {
    // use fake backend in place of Http service for backend-less development
    provide: HTTP_INTERCEPTORS,
    useClass: APIBackendInterceptor,
    multi: true
};