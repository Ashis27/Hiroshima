import { LanguageService } from "../interfaces";

/**
 * Value of the mock data
 */
export const languages = [{
    displayName: "en",
    icon: "usa.svg",
    id: 1,
    isActive: true,
    isDefault: true,
    name: "English"
},
{
    displayName: "jp",
    icon: "jp.svg",
    id: 2,
    isActive: true,
    isDefault: false,
    name: "Japanese"
}];
export const language_info = {
    items: [{
        displayName: "en",
        icon: "usa.svg",
        id: 1,
        isActive: true,
        isDefault: true,
        name: "English"
    },
    {
        displayName: "jp",
        icon: "jp.svg",
        id: 2,
        isActive: true,
        isDefault: false,
        name: "Japanese"
    }]
};

export const languageContents = [{
    Action: "Action",
    Activate: "Activate",
    Active: "Active",
    ActivePassHeader: "Where do you plan to",
    ActivePassSubHeader: "travel today"
}]
export const languageMockContents = {
    "culture": {
      "lang": "en"
    },
    "languages": {
      "AppName": "Hiroshima",
      "MobileAppName": "Hiroshima Ticketing",
      "AppSortName": "HS",
      "DashBoard": "DashBoard",
      "User": "User",
    }
}
export const currentLanguage = {
    id: 1,
    name: "English",
    displayName: "en",
    icon: "en.svg"
}

/**
 * Value of the mock data
 */
export const languageInfo: LanguageService = {
    languages: languages,
    currentLanguage: currentLanguage,
    currentLangContents: languageContents
};
