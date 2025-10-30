import { PublicClientApplication } from '@azure/msal-browser';
import { msalConfig } from '../authConfig';

const msalInstance = new PublicClientApplication(msalConfig);

class ApiService {
    private baseUrl = process.env.REACT_APP_API_BASE_URL || 'https://localhost:7001/api';

    private async getAccessToken(): Promise<string | null> {
        try {
            const activeAccount = msalInstance.getActiveAccount();
            if (!activeAccount) {
                console.warn('No active account found');
                return null;
            }

            // Try to get token silently first
            const silentRequest = {
                scopes: ['openid', 'profile', 'email'],
                account: activeAccount,
            };

            const response = await msalInstance.acquireTokenSilent(silentRequest);
            return response.accessToken;
        } catch (error) {
            console.error('Error getting access token:', error);
            return null;
        }
    }

    private async makeRequest<T>(
        endpoint: string,
        options: RequestInit = {}
    ): Promise<T> {
        const token = await this.getAccessToken();

        const headers: Record<string, string> = {
            'Content-Type': 'application/json',
            ...(options.headers as Record<string, string>),
        };

        // Add Authorization header if token exists
        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        const response = await fetch(`${this.baseUrl}${endpoint}`, {
            ...options,
            headers,
        });

        if (!response.ok) {
            if (response.status === 401) {
                // Token expired or invalid - could trigger re-authentication
                console.warn('Authentication failed, token may be expired');
            }
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        return response.json();
    }

    // GET request
    async get<T>(endpoint: string): Promise<T> {
        return this.makeRequest<T>(endpoint, { method: 'GET' });
    }

    // POST request
    async post<T>(endpoint: string, data?: any): Promise<T> {
        return this.makeRequest<T>(endpoint, {
            method: 'POST',
            body: data ? JSON.stringify(data) : undefined,
        });
    }

    // PUT request
    async put<T>(endpoint: string, data?: any): Promise<T> {
        return this.makeRequest<T>(endpoint, {
            method: 'PUT',
            body: data ? JSON.stringify(data) : undefined,
        });
    }

    // DELETE request
    async delete<T>(endpoint: string): Promise<T> {
        return this.makeRequest<T>(endpoint, { method: 'DELETE' });
    }
}

export const apiService = new ApiService();