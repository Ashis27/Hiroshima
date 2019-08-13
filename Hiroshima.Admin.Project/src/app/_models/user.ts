/**
 * User model
 */
export class User {
    /**
     * User id
     */
    id: number;
    /**
     * User name
     */
    username: string;
    /**
     * User password
     */
    password: string;
    /**
     * User first name
     */
    firstName: string;
    /**
     * User last name
     */
    lastName: string;
    /**
     * User role
     */
    role: string;
    /**
     * User auth token
     */
    authToken?: string;
}