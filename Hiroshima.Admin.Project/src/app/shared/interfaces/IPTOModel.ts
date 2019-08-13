import { IPassActivePTOModel } from "src/app/shared/interfaces/IPassActivePTOModel";
import { IPTODescriptionModel } from "src/app/shared/interfaces";
/**
 * PTO model interface 
 */
export interface IPTOModel{
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
    PTODescription:Array<IPTODescriptionModel>;
    /**
     * Pass having acstive PTOs information
     */
    PassActivePTOs?:Array<IPassActivePTOModel>;
}