import React from 'react';
import './PaymentSuccess.css';

const SuccessPage: React.FC = () => {
  return (
    <div className="success-card">
      <img
        src="https://cdn-icons-png.flaticon.com/512/845/845646.png"
        alt="Success"
      />
      <h2>Payment Successful</h2>
      <p className="description">Thank you for your purchase! Your order is being processed.</p>
      <button onClick={() => window.location.href = '/'}>
        Go to Homepage
      </button>
    </div>
  );
};

export default SuccessPage;