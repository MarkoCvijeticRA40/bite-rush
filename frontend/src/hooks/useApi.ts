import { useState, useCallback } from 'react';
import { apiService } from '../services/apiService';

interface UseApiOptions {
    onSuccess?: (data: any) => void;
    onError?: (error: Error) => void;
}

export const useApi = <T = any>(options: UseApiOptions = {}) => {
    const [data, setData] = useState<T | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<Error | null>(null);

    const execute = useCallback(async (
        method: 'get' | 'post' | 'put' | 'delete',
        endpoint: string,
        payload?: any
    ) => {
        setLoading(true);
        setError(null);

        try {
            let result: T;

            switch (method) {
                case 'get':
                    result = await apiService.get<T>(endpoint);
                    break;
                case 'post':
                    result = await apiService.post<T>(endpoint, payload);
                    break;
                case 'put':
                    result = await apiService.put<T>(endpoint, payload);
                    break;
                case 'delete':
                    result = await apiService.delete<T>(endpoint);
                    break;
                default:
                    throw new Error(`Unsupported method: ${method}`);
            }

            setData(result);
            options.onSuccess?.(result);
            return result;
        } catch (err) {
            const error = err instanceof Error ? err : new Error('Unknown error');
            setError(error);
            options.onError?.(error);
            throw error;
        } finally {
            setLoading(false);
        }
    }, [options]);

    const get = useCallback((endpoint: string) => execute('get', endpoint), [execute]);
    const post = useCallback((endpoint: string, payload?: any) => execute('post', endpoint, payload), [execute]);
    const put = useCallback((endpoint: string, payload?: any) => execute('put', endpoint, payload), [execute]);
    const del = useCallback((endpoint: string) => execute('delete', endpoint), [execute]);

    return {
        data,
        loading,
        error,
        get,
        post,
        put,
        delete: del,
        execute,
    };
};

// Specialized hook for fetching data on component mount
export const useFetch = <T = any>(endpoint: string, options: UseApiOptions = {}) => {
    const api = useApi<T>(options);

    useState(() => {
        api.get(endpoint);
    });

    return api;
};