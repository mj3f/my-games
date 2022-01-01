
/**
 * Utility class used to cache state and other data to localstorage.
 */
export class CacheService {
    public setCacheData(key: string, data: any): void {
        localStorage.setItem(key, JSON.stringify(data));
    }

    public getCacheData(key: string): string | null {
        return localStorage.getItem(key);
    }
    
    public isCached(key: string): boolean {
        return !!localStorage.getItem(key);
    }

    public clearDataFromCache(key: string): void {
        localStorage.setItem(key, '');
    }

    public clearAllFromCache(): void {
        localStorage.clear();
    }
}