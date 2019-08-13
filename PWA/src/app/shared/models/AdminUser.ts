import { IAdminUser } from "src/app/shared/interfaces/IAdminUser";
/**
 * Admin user class model
 */
export class AdminUser implements IAdminUser{
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