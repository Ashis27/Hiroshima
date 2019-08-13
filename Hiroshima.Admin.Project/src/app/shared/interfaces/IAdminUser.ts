/**
 * Admin User Interface
 */
export interface IAdminUser {
    /**
    * Admin user name
    */
    UserName: string;
    /**
     * Admin password
    */
    Password: string;
    /**
    * Confirm password to match with password
    */
    ConfirmPassword?: string;
    /**
    * Admin user role
    */
    Role?: string;
    /**
    * Active status
    */
    IsActive?: boolean;
}