import { IPassDescriptionModel, IPassActivePTOModel } from ".";

/**
 * Pass model interface 
 */
export interface IPassModel {
    /**
    * Pass information id
    */
    id?: number;
    /**
     * Default currency for this pass information
     */
    defaultCurrency: number;
    /**
     * Pass validity in days
     */
    passValidityInDays: number;
    /**
     * Pass validity in hours
     */
    passValidityInHours: number;
    /**
     * Pass expired duration in days
     */
    passExpiredDurationInDays?: number;
     /**
     * Pass expired duration in hours
     */
    passExpiredDurationInHours?: number;
    /**
     * Pass expired date
     */
    passExpiredDate: string;
    /**
     * Adult price for this pass
     */
    adultPrice: number;
    /**
     * Child price for this pass
     */
    childPrice: number;
    /**
     * Pass image AWS url
     */
    imageURL?: string;
    /**
     * Pass information remark
     */
    remark?: string;
    /**
     * Is active status
     */
    isActive: boolean;
    /**
     * Pass Description information
     */
    passDescription: Array<IPassDescriptionModel>;
    /**
     * Pass active PTOs information
     */
    passActivePTOs: Array<IPassActivePTOModel>
}