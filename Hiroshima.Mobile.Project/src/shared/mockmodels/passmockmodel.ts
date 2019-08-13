import { PassModel, PassDescriptionModel } from "../models";

/**
 * Value of the mock data
 */
let passActivePTOs: any = [
    {
        isActive: true,
        passInformationId: 12,
        ptoInformation: [
            {
                imageURL: "",
                isActive: true,
                passActivePTOs: [],
                ptoDescription: [
                    {
                        description: "kjb",
                        isActive: true,
                        ptoInformationId: 10,
                        ptoName: "hkj",
                        selectedLanguage: 1
                    }
                ],
                remark: ""
            }
        ],
        ptoInformationId: 10
    }, {
        isActive: true,
        passInformationId: 12,
        ptoInformation: [
            {
                imageURL: "",
                isActive: true,
                passActivePTOs: [],
                ptoDescription: [
                    {
                        description: "kjb",
                        isActive: true,
                        ptoInformationId: 10,
                        ptoName: "hkj",
                        selectedLanguage: 1
                    }
                ],
                remark: ""
            }
        ],
        ptoInformationId: 10
    }
];

/**
 * Value of the mock data
 */
let passDesc: PassDescriptionModel[] = [
    { passName: "One day train pass", passAreaDescription: 'Hiroshima', passDesc: " It is remorseful to announce that the annual hike will be delayed for a month, due to some unavoidable circumstances. Weregret this inevitable situation and hope you will cooperate with us. We greatly value your work." },
];
export const activeMockPassModel={
    items:[{
        id: 1,
        defaultCurrency: 1,
        passValidityInDays: 2,
        passValidityInHours: 0,
        passExpiredDurationInDays: 0,
        passExpiredDurationInHours: 0,
        passExpiredDate: "2019/10/27",
        adultPrice: 2000,
        childPrice: 200,
        imageURL: "assets/imgs/passdetails/pass-background.jpeg",
        remark: "No refunded will be applicable",
        isActive: true,
        passDescription: passDesc,
        passActivePTOs: passActivePTOs
    },
    {
        id: 2,
        defaultCurrency: 1,
        passValidityInDays: 2,
        passValidityInHours: 0,
        passExpiredDurationInDays: 0,
        passExpiredDurationInHours: 0,
        passExpiredDate: "2019/9/15",
        adultPrice: 1000,
        childPrice: 100,
        imageURL: "assets/imgs/passdetails/pass-back.jpeg",
        remark: "No refunded will be applicable",
        isActive: true,
        passDescription: passDesc,
        passActivePTOs: passActivePTOs
    }]
}
export const activePassModel: PassModel[] = [
    {
        id: 1,
        defaultCurrency: 1,
        passValidityInDays: 2,
        passValidityInHours: 0,
        passExpiredDurationInDays: 0,
        passExpiredDurationInHours: 0,
        passExpiredDate: "2019/10/27",
        adultPrice: 2000,
        childPrice: 200,
        imageURL: "assets/imgs/passdetails/pass-background.jpeg",
        remark: "No refunded will be applicable",
        isActive: true,
        passDescription: passDesc,
        passActivePTOs: passActivePTOs
    },
    {
        id: 2,
        defaultCurrency: 1,
        passValidityInDays: 2,
        passValidityInHours: 0,
        passExpiredDurationInDays: 0,
        passExpiredDurationInHours: 0,
        passExpiredDate: "2019/9/15",
        adultPrice: 1000,
        childPrice: 100,
        imageURL: "assets/imgs/passdetails/pass-back.jpeg",
        remark: "No refunded will be applicable",
        isActive: true,
        passDescription: passDesc,
        passActivePTOs: passActivePTOs
    }
];


/**
 * Value of the paginated mock pass data
 */
let paginatedPass = {
    items: activePassModel,
    TotalCount: 11,
    TotalPages: 1,
    HasPreviousPage: false,
    HasNextPage: false
};


/**
 * Value of the mock data
 */
export const activePasses = paginatedPass;