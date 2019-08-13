import { IPTODescriptionModel, IPassActivePTOModel } from ".";

/**
 * PTO model interface 
 */
export interface IPTOModel{
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
    ptoDescription:Array<IPTODescriptionModel>;
    /**
     * Pass having acstive PTOs information
     */
    passActivePTOs?:Array<IPassActivePTOModel>;
}