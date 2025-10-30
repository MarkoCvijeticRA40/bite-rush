import React from 'react';
import { AuthenticatedTemplate, useMsal, UnauthenticatedTemplate } from '@azure/msal-react';
import { loginRequest } from '../../authConfig';

// Import the sample components
import { IdTokenData } from '../../components/sample/DataDisplay';
import { PageLayout } from '../../components/sample/PageLayoutSample';

/**
 * Main content component from the CIAM sample
 */
const MainContent = () => {
    const { instance } = useMsal();
    const activeAccount = instance.getActiveAccount();

    const handlePopupLogin = () => {
        instance
            .loginPopup({
                ...loginRequest,
                prompt: 'create',
            })
            .catch((error) => console.log(error));
    };

    return (
        <div className="App">
            <AuthenticatedTemplate>
                {activeAccount ? (
                    <div style={{ padding: '2rem' }}>
                        <IdTokenData idTokenClaims={activeAccount.idTokenClaims} />
                    </div>
                ) : null}
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                <div style={{ padding: '2rem', textAlign: 'center' }}>
                    <h2>CIAM Authentication Sample</h2>
                    <button
                        onClick={handlePopupLogin}
                        style={{
                            padding: '1rem 2rem',
                            backgroundColor: '#0078d4',
                            color: 'white',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: 'pointer',
                            fontSize: '1rem'
                        }}
                    >
                        Sign up
                    </button>
                </div>
            </UnauthenticatedTemplate>
        </div>
    );
};

export const SampleAuth: React.FC = () => {
    return (
        <PageLayout>
            <MainContent />
        </PageLayout>
    );
};