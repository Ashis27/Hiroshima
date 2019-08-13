import { IBookedPassInformation } from "../interfaces";
import { PassModel } from ".";

/**
 * @ignore
 */
export class BookedPassInformation implements IBookedPassInformation {
    /**
     * Pass information id
     */
    passInformationId: number;
    /**
     * Traveller device id
     */
    travellerDeviceId: string;
    /**
     * Traveller id
     */
    travellerId?: number;
    /**
     * Unique referrence number
     */
    uniqueReferrenceNumber?: string;
    /**
     * Transaction number
     */
    transactionNumber?: number;
    /**
     * Rider information
     */
    riderInformation?: string;
    /**
     * Booking date
     */
    bookingDate: string;
    /**
     * Payment Status
     */
    paymentStatus?: boolean;
    /**
     * Payment response message
     */
    paymentResponse?: string;
    /**
     * Total amount
     */
    totalAmout: number;
    /**
     * Number of child
     */
    child: number;
    /**
     * Number of adult
     */
    adult: number;
    /**
     * Remark value
     */
    remark?: string;
    /**
     * Pass model information  
     */
    passInformation?: PassModel;
    /**
     * PG transaction model information
     */
    pgTransactionInfo?: any;
    /**
     * QR code model information
     */
    qrCode?: any

}