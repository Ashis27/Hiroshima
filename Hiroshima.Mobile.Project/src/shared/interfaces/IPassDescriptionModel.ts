
/**
 * Pass description interface 
 */
export interface IPassDescriptionModel {
    /**
     * Pass description id
     */
    id?: number;
    /**
     * Pass information id
     */
    passInformationId?: number;
    /**
     * Pass name
     */
    passName: string;
    /**
     * Pass description
     */
    passDesc?: string;
    /**
     * Selected language for pass description
     */
    selectedLanguage?: number;
    /**
     * Selected language description
     */
    selectedLanguageDesc?: string;
    /**
     * Pass area description
     */
    passAreaDescription: string;
    /**
    * Area route image URL
    */
    PassAreaImageURL?: string;
    /**
     * Is perk available
     */
    isPerkAvailable?: boolean;
    /**
     * Perk description
     */
    perkDescription?: string;
    /**
     * Is available status
     */
    isActive?: boolean;
}