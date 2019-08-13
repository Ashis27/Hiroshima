import { IPTOModel } from "src/app/shared/interfaces";
import { PassActivePTOModel, PTODescriptionModel } from "src/app/shared/models";

/**
 * PTO information class model
 */
export class PTOModel implements IPTOModel{
   /**
     * PTO information id
     */
    Id?:number;
    /**
     * PTO AWS image url
     */
    ImageURL?:string;
     /**
     * PTO information remark
     */
    Remark?:string;
    /**
     * Is active status
     */
    IsActive:boolean;
    /**
     * PTO description information
     */
    PTODescription:Array<PTODescriptionModel>;
    /**
     * Pass having acstive PTOs information
     */
    PassActivePTOs?:Array<PassActivePTOModel>;
}