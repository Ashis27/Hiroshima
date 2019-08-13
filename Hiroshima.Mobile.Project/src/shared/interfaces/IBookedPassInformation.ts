import { IPassModel } from ".";

/**
 * Booked Pass Information interface
 */
export interface  IBookedPassInformation{
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
    passInformation?: IPassModel;
    /**
     * PG transaction model information
     */
    pgTransactionInfo?: any;
    /**
     * QR code model information
     */
    qrCode?: any
}