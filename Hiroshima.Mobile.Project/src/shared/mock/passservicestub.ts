import { Observable } from "rxjs";
import { isActvated, bookingInfo, uId } from "../mockmodels/bookingmodel";
import { activePasses, activePassModel } from "../mockmodels/passmockmodel";
import { PassModel } from "../models";


/**
 * Value of the pass mock serive
 */
export class PassSeviceStub {
    
/**
 * Value of the get pass mock serive
 */
    public GetPasses(): Observable<any> {
        return Observable.of(activePasses);
    }
}