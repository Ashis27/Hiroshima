import { IPassDescriptionModel } from "src/app/shared/interfaces";
import { IPassActivePTOModel } from "src/app/shared/interfaces/IPassActivePTOModel";
/**
 * Pass model interface 
 */
export interface IPassModel {
    /**
    * Pass information id
    */
    Id?: number;
    /**
     * Default currency for this pass information
     */
    DefaultCurrency: Number;
    /**
     * Pass validity in days
     */
    PassValidityInDays: Number;
    /**
     * Pass validity in hours
     */
    PassValidityInHours: Number;
    /**
    * Pass expired duration in days
    */
    PassExpiredDurationInDays?: Number;
    /**
     * Pass expired duration in hours
     */
    PassExpiredDurationInHours?: number;
    /**
     * Pass expired date
     */
    PassExpiredDate: string;
    /**
     * Adult price for this pass
     */
    AdultPrice: number;
    /**
     * Child price for this pass
     */
    ChildPrice: Number;
    /**
    * Pass image AWS url
    */
    ImageURL?: string;
    /**
    * Pass information remark
    */
    Remark?: string;
    /**
    * Is active status
    */
    IsActive: boolean;
    /**
    * Pass Description information
    */
    PassDescription: Array<IPassDescriptionModel>;
    /**
     * Pass active PTOs information
     */
    PassActivePTOs: Array<IPassActivePTOModel>
}