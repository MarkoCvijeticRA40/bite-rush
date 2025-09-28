import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import ChatHub from './pages/ChatHub/ChatHub';
import PaymentFailedPage from './pages/Payment/PaymentFailed/PaymentFailed';
import SuccessPage from './pages/Payment/PaymentSuccess/PaymentSuccess';
import ProductsPage from './pages/ProductListing/ProductListing';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Navigate to="/products" />} />
        <Route path="/products" element={<ProductsPage />} />
        <Route path="/payment-success" element={<SuccessPage />} />
        <Route path="/payment-failed" element={<PaymentFailedPage />} />
        <Route path="/chat-hub" element={<ChatHub />} />
      </Routes>
    </Router>
  );
};

export default App;
