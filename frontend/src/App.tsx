import React, { useEffect } from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { PageLayout } from './components/PageLayout/PageLayout';
import PaymentFailedPage from './pages/Payment/PaymentFailed/PaymentFailed';
import SuccessPage from './pages/Payment/PaymentSuccess/PaymentSuccess';
import ProductsPage from './pages/ProductListing/ProductListing';
import { SampleAuth } from './pages/SampleAuth/SampleAuth';
import { UserDashboard } from './pages/UserDashboard/UserDashboard';
import { useMsal, MsalProvider } from '@azure/msal-react';
import { PublicClientApplication } from '@azure/msal-browser';
import { msalConfig } from './authConfig';

const msalInstance = new PublicClientApplication(msalConfig);

// Redirect component for handling authentication callback
const Redirect: React.FC = () => {
  const { instance } = useMsal();

  useEffect(() => {
    instance.handleRedirectPromise().then((response) => {
      if (response) {
        // Redirect was successful, navigate to sample-auth page
        window.location.href = '/sample-auth';
      }
    }).catch((error) => {
      console.error('Redirect error:', error);
    });
  }, [instance]);

  return (
    <div style={{ padding: '20px', textAlign: 'center' }}>
      <h2>Processing authentication...</h2>
      <p>Please wait while we complete your sign-in.</p>
    </div>
  );
};

const App: React.FC = () => {
  return (
    <MsalProvider instance={msalInstance}>
      <Router>
        <PageLayout>
          <Routes>
            <Route path="/" element={<Navigate to="/products" />} />
            <Route path="/products" element={<ProductsPage />} />
            <Route path="/payment-success" element={<SuccessPage />} />
            <Route path="/payment-failed" element={<PaymentFailedPage />} />
            <Route path="/sample-auth" element={<SampleAuth />} />
            <Route path="/dashboard" element={<UserDashboard />} />
            <Route path="/redirect" element={<Redirect />} />
          </Routes>
        </PageLayout>
      </Router>
    </MsalProvider>
  );
};

export default App;
