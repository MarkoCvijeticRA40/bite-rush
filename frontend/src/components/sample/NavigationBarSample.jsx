import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from '@azure/msal-react';
import { loginRequest } from '../../authConfig';

export const NavigationBar = () => {
    const { instance } = useMsal();
    
    const handleLoginRedirect = () => {
        instance.loginRedirect(loginRequest).catch((error) => console.log(error));
    };

    const handleLogoutRedirect = () => {
        instance.logoutRedirect().catch((error) => console.log(error));
    };

    return (
        <nav style={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            padding: '1rem 2rem',
            backgroundColor: '#0078d4',
            color: 'white'
        }}>
            <div>
                <h1 style={{ margin: 0, color: 'white' }}>CIAM Sample</h1>
            </div>
            <div>
                <AuthenticatedTemplate>
                    <button 
                        onClick={handleLogoutRedirect}
                        style={{
                            padding: '0.5rem 1rem',
                            backgroundColor: '#d13438',
                            color: 'white',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: 'pointer'
                        }}
                    >
                        Sign out
                    </button>
                </AuthenticatedTemplate>
                <UnauthenticatedTemplate>
                    <button 
                        onClick={handleLoginRedirect}
                        style={{
                            padding: '0.5rem 1rem',
                            backgroundColor: '#106ebe',
                            color: 'white',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: 'pointer'
                        }}
                    >
                        Sign in
                    </button>
                </UnauthenticatedTemplate>
            </div>
        </nav>
    );
};
