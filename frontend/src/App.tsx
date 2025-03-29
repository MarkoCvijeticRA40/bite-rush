import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import ProductsPage from './pages/ProductsPage/ProductsPage';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Navigate to="/products" />} />
        <Route path="/products" element={<ProductsPage />} />
      </Routes>
    </Router>
  );
};

export default App;
