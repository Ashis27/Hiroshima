import { ICurrency } from "src/app/shared/interfaces/ICurrency";
/**
 * Currency class model
 */
export class CurrencyModule implements ICurrency
{
     /**
     * Currency information id
     */
    id?:number;
    /**
     * Currency name
     */
    name: string;
    /**
     * Currency display/sort name
     */
    displayName: string;
    /**
     * Currency icon
     */
    icon: string;
    /**
     * Is dafault Currency
     */
    isDefault?: boolean;
    /**
     * Is active Currency
     */
    isActive?: boolean;
}