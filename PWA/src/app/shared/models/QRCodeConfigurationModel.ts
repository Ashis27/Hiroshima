import { IQRCodeConfiguration } from "src/app/shared/interfaces/IQRCodeConfiguration";

/**
 * Config Model
 */
export class QRCodeConfigurationModel implements IQRCodeConfiguration {
    /**
     * Config primary key
     */
    id?: number;
    /**
     * config regeneration time
     */
    regenerationTimeInMin: string
}