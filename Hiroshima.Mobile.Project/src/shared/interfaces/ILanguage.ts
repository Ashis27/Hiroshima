/**
 * Language information interface
 */
export interface ILanguageInfo {
    /**
     * Language information id
     */
    id?:number;
    /**
     * language name
     */
    name: string;
    /**
     * language display/sort name
     */
    displayName: string;
    /**
     * language icon
     */
    icon: string;
    /**
     * Is dafault language
     */
    isDefault?: boolean;
    /**
     * Is active language
     */
    isActive?: boolean;
}

/**
 * Language service interface
 */
export interface LanguageService {
    /**
     * List of languages
     */
    languages: ILanguageInfo[],
    /**
     * Selected current language
     */
    currentLanguage: ILanguageInfo,
    /**
     * Current language content based on selected language
     */
    currentLangContents: Array<any>
}