import { Observable } from "rxjs";
import { isActvated, uId } from "../mockmodels/bookingmodel";
import { BookedPassInformation } from "../models/BookedPassInformation";
import { languages, language_info, languageMockContents } from "../mockmodels/languagemockmodel";



/**
 * Value of the language mock serive
 */
export class LanguageServiceStub {

    
/**
 * Value of the get language mock serive
 */
    GetActiveLanguages(): Observable<any> {
        return Observable.of(language_info);    
    }
    GetActiveLanguageContents(): Observable<any> {
        return Observable.of(languageMockContents);    
    }
}