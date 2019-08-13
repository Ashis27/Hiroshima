/**
 * Traveller feedback interface
 */
export interface ITravellerFeedback {
    /**
     * Traveller primary key
     */
    Id?: number;
    /**
     * Traveller id
     */
    TravellerId: number;
    /**
     * Traveller name
     */
    FullName: string;
    /**
     * Traveller contact number
     */
    ContactNumber: string;
    /**
     * Feedback description added by user
     */
    FeedbackDescription: string;
    /**
     * Feedback type like booking or any other query
     */
    FeedbackType?: string;
    /**
     * Comment by admin
     */
    CommentedByAdmin?: string
}