import React from 'react';
import { useMsal, useIsAuthenticated } from '@azure/msal-react';
import { Link } from 'react-router-dom';

const AuthSuccess: React.FC = () => {
    const { accounts, instance } = useMsal();
    const isAuthenticated = useIsAuthenticated();
    const activeAccount = instance.getActiveAccount();

    const handleLogout = () => {
        instance.logoutRedirect().catch((error) => console.log(error));
    };

    if (!isAuthenticated) {
        return (
            <div style={{ padding: '2rem', textAlign: 'center' }}>
                <h2>Authentication Required</h2>
                <p>You need to be authenticated to view this page.</p>
                <Link to="/sample-auth" style={{ color: '#0078d4', textDecoration: 'none' }}>
                    Go to Login
                </Link>
            </div>
        );
    }

    return (
        <div style={{ padding: '2rem', maxWidth: '800px', margin: '0 auto' }}>
            <div style={{
                backgroundColor: '#d4edda',
                border: '1px solid #c3e6cb',
                borderRadius: '8px',
                padding: '1.5rem',
                marginBottom: '2rem',
                textAlign: 'center'
            }}>
                <h1 style={{ color: '#155724', margin: '0 0 1rem 0' }}>
                    🎉 Welcome to Bite Rush!
                </h1>
                <p style={{ color: '#155724', margin: 0, fontSize: '1.1rem' }}>
                    You have successfully registered and logged in!
                </p>
            </div>

            <div style={{
                backgroundColor: '#f8f9fa',
                border: '1px solid #dee2e6',
                borderRadius: '8px',
                padding: '1.5rem',
                marginBottom: '2rem'
            }}>
                <h3 style={{ marginTop: 0, color: '#495057' }}>Your Account Information</h3>
                {activeAccount && (
                    <div style={{ lineHeight: '1.6' }}>
                        <p><strong>Name:</strong> {activeAccount.name || 'Not provided'}</p>
                        <p><strong>Email:</strong> {activeAccount.username || 'Not provided'}</p>
                        <p><strong>Account ID:</strong> {activeAccount.homeAccountId}</p>
                    </div>
                )}
            </div>

            <div style={{
                display: 'flex',
                gap: '1rem',
                justifyContent: 'center',
                flexWrap: 'wrap'
            }}>
                <Link
                    to="/products"
                    style={{
                        padding: '0.75rem 1.5rem',
                        backgroundColor: '#0078d4',
                        color: 'white',
                        textDecoration: 'none',
                        borderRadius: '4px',
                        fontWeight: '500'
                    }}
                >
                    Browse Products
                </Link>

                <Link
                    to="/sample-auth"
                    style={{
                        padding: '0.75rem 1.5rem',
                        backgroundColor: '#6c757d',
                        color: 'white',
                        textDecoration: 'none',
                        borderRadius: '4px',
                        fontWeight: '500'
                    }}
                >
                    View Auth Details
                </Link>

                <button
                    onClick={handleLogout}
                    style={{
                        padding: '0.75rem 1.5rem',
                        backgroundColor: '#dc3545',
                        color: 'white',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer',
                        fontWeight: '500'
                    }}
                >
                    Sign Out
                </button>
            </div>
        </div>
    );
};

export default AuthSuccess;