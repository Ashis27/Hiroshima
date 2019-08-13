import { IPTODescriptionModel } from "../interfaces";

/**
 * PTO description model
 */
export class PTODescriptionModel implements IPTODescriptionModel{
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