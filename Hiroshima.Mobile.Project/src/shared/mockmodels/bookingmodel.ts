import { PassModel } from "../models";
import { activePassModel } from "./passmockmodel";

/**
 * Value of the mock data
 */
export const isActvated = true;

/**
 * Value of the mock data
 */
export const uId = "1234abcd";

/**
 * Value of the mock data
 */
export const bookingInfo = {
    /**
     * Pass information id
     */
    passInformationId: 1,
    /**
     * Traveller device id
     */
    travellerDeviceId: "ahstfag23gjhx",
    /**
     * Traveller id
     */
    travellerId: 0,
    /**
     * Unique referrence number
     */
    uniqueReferrenceNumber: "",
    /**
     * Transaction number
     */
    transactionNumber: 0,
    /**
     * Rider information
     */
    riderInformation: "",
    /**
     * Booking date
     */
    bookingDate: "27-10-2019",
    /**
     * Payment Status
     */
    paymentStatus: true,
    /**
     * Payment response message
     */
    paymentResponse: "",
    /**
     * Total amount
     */
    totalAmout: 1000,
    /**
     * Number of child
     */
    child: 1,
    /**
     * Number of adult
     */
    adult: 1,
    /**
     * Remark value
     */
    remark: "",
    /**
     * Pass model information  
     */
    passInformation: activePassModel[0],
    /**
     * PG transaction model information
     */
    pgTransactionInfo: {},
    /**
     * QR code model information
     */
    qrCode: {}

}

