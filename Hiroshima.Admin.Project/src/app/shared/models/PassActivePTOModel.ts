import { PTOModel, PassModel } from "src/app/shared/models";
import { IPassActivePTOModel } from "src/app/shared/interfaces";

/**
 * Pass having active PTOs class model
 */
export class PassActivePTOModel implements IPassActivePTOModel {
  /**
    * Pass information id
    */
   PassInformationId?: number;
   /**
    * Pass information details
    */
   PassInformation?: PassModel
   /**
    * PTO information id
    */
   PTOInformationId: number;
   /**
    * PTO information detils
    */
   PTOInformation?: PTOModel;
   /**
    * Is active status
    */
   IsActive?: boolean;
}