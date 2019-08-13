import { Injectable, Injector } from '@angular/core';
import { ToastController } from 'ionic-angular';
import { CacheService } from 'ionic-cache';
import { AppConfigurationServiceProvider } from '../../providers/app.configuration.service';

/*
  Generated class for the StorageHandlerService service.
*/
@Injectable()
export class StorageHandlerService {

    /**
     Cache object
     */
    private _cache: CacheService

    /**
  *@ignore 
  */
    constructor(
        // public storage: Storage,
        injector: Injector
    ) {
        this._cache = injector.get(CacheService);
    }

    /**
     * To store the date againt cache key in cache memory
     * @example
     * setStoreDataIncache("https://localhost","Hi",{data})
     * @param {string} url API base URL for unique key
     * @param {string} uKey cache key name
     * @param {string} data data to be stored in cache
     */
    setStoreDataIncache(url, uKey, data) {
        let cacheKey = url;
        let uniqueKey = uKey;
        let ttl = 60 * 60 * 24 * 7 * 30 * 12;
        //      let delayType="all";
        return this._cache.saveItem(cacheKey, data, uniqueKey, ttl);
    }

    /**
     * To get the stored result from cache
     * @example
     * getStoreDataFromCache("Hi")
     * @param {string} key cache key name to get the data from cache memory
     */
    getStoreDataFromCache(key) {
        return this._cache.getItem(key).catch((data) => {
            // fall here if item is expired or doesn't exist
            return false;
        }).then((data) => {
            return data;
        }).catch(error => {
            console.log(error);
        })

    }
}
