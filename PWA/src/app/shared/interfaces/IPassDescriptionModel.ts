/**
 * Pass description interface 
 */
export interface IPassDescriptionModel{
    /**
     * Pass description id
     */
    Id?:number;
    /**
     * Pass information id
     */
    PassInformationId?:number;
    /**
     * Pass name
     */
    PassName:string;
    /**
     * Pass description
     */
    PassDesc:string;
    /**
     * Selected language for pass description
     */
    SelectedLanguage:number;
    /**
     * Selected language description
     */ 
    SelectedLanguageDesc?:string;
    /**
     * Pass area description
     */ 
    PassAreaDescription?:string;
    /**
    * Area route image URL
    */
    PassAreaImageURL?:string;
    /**
     * Is perk available
     */
    IsPerkAvailable?:boolean;
    /**
     * Perk description
     */
    PerkDescription?:string;
    /**
     * Is available status
     */
    IsActive:boolean;
}