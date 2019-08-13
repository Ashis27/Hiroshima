import { IPassActivePTOModel } from "../interfaces";
import { PassModel, PTOModel } from ".";

/**
 * Pass active PTO model
 */
export class PassActivePTOModel implements IPassActivePTOModel {
    /**
     * Pass information id
     */
    passInformationId?:number;
    /**
     * Pass information details
     */
    passInformation?:PassModel
    /**
     * PTO information id
     */
    ptoInformationId:number;
    /**
     * PTO information detils
     */
    ptoInformation?:PTOModel;
    /**
     * Is active status
     */
    isActive?:boolean;
}