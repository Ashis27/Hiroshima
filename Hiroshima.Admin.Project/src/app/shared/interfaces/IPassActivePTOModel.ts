import { IPTOModel, IPassModel } from "src/app/shared/interfaces";
/**
 * Pass active PTOs interface 
 */
export interface IPassActivePTOModel {
    /**
    * Pass information id
    */
    PassInformationId?: number;
    /**
     * Pass information details
     */
    PassInformation?: IPassModel
    /**
     * PTO information id
     */
    PTOInformationId: number;
    /**
     * PTO information detils
     */
    PTOInformation?: IPTOModel;
    /**
     * Is active status
     */
    IsActive?: boolean;
}