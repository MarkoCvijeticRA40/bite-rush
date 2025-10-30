import React, { useEffect, useState } from 'react';
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from '@azure/msal-react';
import { useApi } from '../../hooks/useApi';

interface UserProfile {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
}

interface Order {
    id: string;
    productName: string;
    amount: number;
    status: string;
    createdAt: string;
}

export const UserDashboard: React.FC = () => {
    const { instance } = useMsal();
    const activeAccount = instance.getActiveAccount();

    // API hooks
    const profileApi = useApi<UserProfile>();
    const ordersApi = useApi<Order[]>();
    const createOrderApi = useApi<Order>();

    useEffect(() => {
        // Load user data when component mounts and user is authenticated
        if (activeAccount) {
            profileApi.get('/user/profile');
            ordersApi.get('/user/orders');
        }
    }, [activeAccount]);

    const handleCreateOrder = async () => {
        try {
            const newOrder = await createOrderApi.post('/orders', {
                productId: 'some-product-id',
                quantity: 1
            });

            console.log('Order created:', newOrder);
            // Refresh orders list
            ordersApi.get('/user/orders');
        } catch (error) {
            console.error('Failed to create order:', error);
        }
    };

    return (
        <div style={{ padding: '2rem' }}>
            <AuthenticatedTemplate>
                <h1>Welcome, {activeAccount?.name || 'User'}!</h1>

                {/* User Profile Section */}
                <div style={{ marginBottom: '2rem' }}>
                    <h2>Profile</h2>
                    {profileApi.loading && <p>Loading profile...</p>}
                    {profileApi.error && <p style={{ color: 'red' }}>Error: {profileApi.error.message}</p>}
                    {profileApi.data && (
                        <div>
                            <p>Email: {profileApi.data.email}</p>
                            <p>Name: {profileApi.data.firstName} {profileApi.data.lastName}</p>
                        </div>
                    )}
                </div>

                {/* Orders Section */}
                <div style={{ marginBottom: '2rem' }}>
                    <h2>Your Orders</h2>
                    <button
                        onClick={handleCreateOrder}
                        disabled={createOrderApi.loading}
                        style={{
                            padding: '0.5rem 1rem',
                            backgroundColor: '#0078d4',
                            color: 'white',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: 'pointer',
                            marginBottom: '1rem'
                        }}
                    >
                        {createOrderApi.loading ? 'Creating...' : 'Create New Order'}
                    </button>

                    {ordersApi.loading && <p>Loading orders...</p>}
                    {ordersApi.error && <p style={{ color: 'red' }}>Error: {ordersApi.error.message}</p>}
                    {ordersApi.data && (
                        <div>
                            {ordersApi.data.length === 0 ? (
                                <p>No orders found.</p>
                            ) : (
                                <ul>
                                    {ordersApi.data.map(order => (
                                        <li key={order.id}>
                                            <strong>{order.productName}</strong> -
                                            ${order.amount} -
                                            Status: {order.status} -
                                            {new Date(order.createdAt).toLocaleDateString()}
                                        </li>
                                    ))}
                                </ul>
                            )}
                        </div>
                    )}
                </div>

                {/* Token Debug Info */}
                <details style={{ marginTop: '2rem' }}>
                    <summary>Debug: Token Information</summary>
                    <pre style={{ fontSize: '12px', background: '#f5f5f5', padding: '1rem' }}>
                        {JSON.stringify(activeAccount?.idTokenClaims, null, 2)}
                    </pre>
                </details>
            </AuthenticatedTemplate>

            <UnauthenticatedTemplate>
                <h1>Please sign in to access your dashboard</h1>
            </UnauthenticatedTemplate>
        </div>
    );
};