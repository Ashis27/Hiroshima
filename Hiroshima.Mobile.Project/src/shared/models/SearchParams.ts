import { ISearchParams } from "../interfaces";

/**
 * Search params model
 */
export class SearchParams implements ISearchParams{
   /**
     * User device id
     */
    deviceId?:string;
    /**
     * Current page index
     */
    pageIndex:number;
    /**
     * Current page size
     */
    pageSize:number;
    /**
     * Selected language name
     */
    lang:number;
    /**
     * Start date and time
     */
    startDateAndTime?:string;
    /**
     * End date and time
     */
    endDateAndTime?:string;
}