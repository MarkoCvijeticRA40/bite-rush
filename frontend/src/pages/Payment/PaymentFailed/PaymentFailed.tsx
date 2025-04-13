import React from 'react';
import './PaymentFailed.css';

const PaymentFailedPage: React.FC = () => {
  return (
    <div className="failed-card">
      <img
        src="https://cdn-icons-png.flaticon.com/512/753/753345.png"
        alt="Payment Failed"
      />
      <h2>Payment Failed</h2>
      <p className="description">
        Oops! Something went wrong with your payment. Please try again or contact support.
      </p>
      <button onClick={() => window.location.href = '/'}>
        Try Again
      </button>
    </div>
  );
};

export default PaymentFailedPage;
