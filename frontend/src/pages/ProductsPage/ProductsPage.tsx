import React, { useEffect, useState } from 'react';
import ProductCard from '../../components/ProductCard/ProductCard';
import { getAllProducts } from '../../services/productService';
import { Product } from '../../types/Product';
import './ProductsPage.css';

const ProductsPage: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    getAllProducts()
      .then(setProducts)
      .catch(() => setError("Failed to load products"))
      .finally(() => setLoading(false));
  }, []);

  const handleBuy = (priceId: string) => {
    alert(`Buying product with price ID: ${priceId}`);
  };

  return (
    <div className="products-page">
      <h2 className="products-title">All Products</h2>

      {loading && <p className="products-message">Loading...</p>}
      {error && <p className="products-error">{error}</p>}

      <div className="products-grid">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} onBuy={handleBuy} />
        ))}
      </div>
    </div>
  );
};

export default ProductsPage;
