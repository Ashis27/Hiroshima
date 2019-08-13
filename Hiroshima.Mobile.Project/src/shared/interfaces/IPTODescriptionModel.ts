
/**
 * PTO description interface 
 */
export interface IPTODescriptionModel{
    /**
     * PTO information id
     */
    PTOInformationId:number;
    /**
     * PTO name
     */
    PTOName:string;
    /**
     * PTO description
     */
    Description?:string;
    /**
     * Selected language for this PTO
     */
    SelectedLanguage:number;
    /**
     * Selected language description for this PTO
     */
    SelectedLanguageDesc?:string;
    /**
     * Is active available
     */
    IsActive:boolean;
}