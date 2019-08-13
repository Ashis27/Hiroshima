import { Observable } from "rxjs";
import { isActvated, uId } from "../mockmodels/bookingmodel";
import { BookedPassInformation } from "../models/BookedPassInformation";
import { activePassModel, activeMockPassModel } from "../mockmodels/passmockmodel";


/**
 * Value of the user mock serive
 */
export class UserSeviceStub {


    /**
     * Value of the activate mock serive
     */
    ActivatePass(uId: string, status: boolean): Observable<boolean> {
        return Observable.of(isActvated);
    }

    /**
     * Value of the book mock serive
     */
    BookPass(bookedPassInformation: BookedPassInformation, status: boolean): Observable<any> {
        return Observable.of(uId);
    }
    GetAvailablePasses(): Observable<any> {
        return Observable.of(activeMockPassModel);    
    }

    
    GetExpiredPasses(): Observable<any> {
        return Observable.of(activeMockPassModel);    
    }


    GetInUsePasses(): Observable<any> {
        return Observable.of(activeMockPassModel);    
    }
    

    
    SubmitFeedback(): Observable<any> {
        return Observable.of(activeMockPassModel);    
    }
    

}