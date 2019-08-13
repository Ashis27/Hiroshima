import { Injectable } from '@angular/core';
import {
    Router,
    CanActivate,
    ActivatedRouteSnapshot,
    RouterStateSnapshot
} from '@angular/router';
import decode from 'jwt-decode';
import { AuthenticationService } from 'src/app/_services';
import { JwtHelperService } from '@auth0/angular-jwt';
import { throwError } from 'rxjs';


/**
 * Role based guard service
 */
@Injectable({ providedIn: 'root' })
export class RoleGuardService implements CanActivate {
    /**
     * Value of the JWT helper service object
     */
    private jwtHelper = new JwtHelperService();

    /**
     * @ignore
     */
    constructor(public authenticationService: AuthenticationService, public router: Router) { }

    /**
     * To check current role auth status while page redirection
     * @example
     * canActivate()
     * @returns {boolean} True | False
     */
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        // this will be passed from the route config
        // on the data property
        let expectedRole = "";
        if (!route.data || !route.data.expectedRole || route.data.expectedRole.length == 0) {
            this.authenticationService.logout();
            this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
            return false;
        }
        let isAuthenticated = false;
        route.data.expectedRole.forEach(element => {
            expectedRole = element;
            const currentUser = this.authenticationService.currentUserValue;
            const token = currentUser ? currentUser.authToken : null;
            // decode the token to get its payload
            const tokenPayload: any = this.authenticationService.decodeToken();
            if (this.authenticationService.isAuthenticated() && tokenPayload.role == expectedRole)
                isAuthenticated = true;
        });
        if (!isAuthenticated) {
            this.authenticationService.logout();
            this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
            return false;
        }
        return true;
    }
}