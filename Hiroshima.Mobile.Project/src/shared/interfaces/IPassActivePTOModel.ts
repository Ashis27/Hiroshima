import { IPassModel, IPTOModel } from ".";

/**
 * Pass active PTOs interface 
 */
export interface IPassActivePTOModel{
    /**
     * Pass information id
     */
    passInformationId?:number;
    /**
     * Pass information details
     */
    passInformation?:IPassModel
    /**
     * PTO information id
     */
    ptoInformationId:number;
    /**
     * PTO information detils
     */
    ptoInformation?:IPTOModel;
    /**
     * Is active status
     */
    isActive?:boolean;
}