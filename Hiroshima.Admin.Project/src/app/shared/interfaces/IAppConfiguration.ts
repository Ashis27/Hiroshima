
/**
 * App configuraion interface
 */
export interface IAppConfiguration {
    /**
     * Event name
     */
    envName: string,
    /**
     * Api services
     */
    apiServices: {
        remoteServiceBaseUrl: string
    },
    /**
     * app version
     */
    version: string,
}