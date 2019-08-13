import { IPassModel } from "src/app/shared/interfaces/IPassModel";
import { PassDescriptionModel, PTOModel, PassActivePTOModel } from "src/app/shared/models";

/**
 * Pass information class model
 */
export class PassModel implements IPassModel{
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
   PassDescription: Array<PassDescriptionModel>;
   /**
    * Pass active PTOs information
    */
   PassActivePTOs: Array<PassActivePTOModel>
}