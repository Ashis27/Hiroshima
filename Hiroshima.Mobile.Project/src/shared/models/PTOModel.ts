import { IPTOModel } from "../interfaces";
import { PTODescriptionModel, PassActivePTOModel } from ".";


/**
 * PTO model
 */
export class PTOModel implements IPTOModel{
   /**
     * PTO information id
     */
    id?:number;
    /**
     * PTO AWS image url
     */
    imageURL?:string;
    /**
     * PTO information remark
     */
    remark?:string;
    /**
     * Is active status
     */
    isActive:boolean;
    /**
     * PTO description information
     */
    ptoDescription:Array<PTODescriptionModel>;
    /**
     * Pass acstive PTOs information
     */
    passActivePTOs?:Array<PassActivePTOModel>;
}