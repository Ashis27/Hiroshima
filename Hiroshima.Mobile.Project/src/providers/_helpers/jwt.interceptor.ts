import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

/**
Jwt interceptor
*/
@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    /**
    *@ignore
    */
    constructor() { }

    /**
     * @example
     * intercept(request,next)
     * @param request  HttpRequest
     * @param next HttpHandler
     *@returns {void}
     */
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add authorization header with jwt token if available
        // let currentUser = this.authenticationService.currentUserValue;
        // if (currentUser && currentUser.token) {
        if (navigator.onLine) {
            request = request.clone({
                setHeaders: {
                    headers: `Access-Control-Allow-Origin', '*','Accept', 'application/json','Content-Type', 'application/json'`,
                    // Authorization: `Bearer ${currentUser.token}`
                }
            });
            // }
            return next.handle(request);
        }
        else
            return Observable.throw("You are OFFLINE. Please check your network connection!");
    }
}