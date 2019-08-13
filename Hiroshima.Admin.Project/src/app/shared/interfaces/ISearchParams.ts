/**
 * Search params interface 
 */
export interface ISearchParams{
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