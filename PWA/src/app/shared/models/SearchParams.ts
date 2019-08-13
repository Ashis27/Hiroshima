import { ISearchParams } from "src/app/shared/interfaces";

/**
 * Search parameter class model for pagination
 */
export class SearchParams implements ISearchParams{
  /**
     * User device id
     */
    DeviceId?:string;
    /**
     * Current page index
     */
    PageIndex:number;
    /**
     * Current page size
     */
    PageSize:number;
    /**
     * Selected language name
     */
    Lang:number;
    /**
     * Start date and time
     */
    StartDateAndTime?:string;
    /**
     * Start date and time
     */
    EndDateAndTime?:string;
}