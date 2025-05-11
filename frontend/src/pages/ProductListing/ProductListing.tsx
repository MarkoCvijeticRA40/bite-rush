import React, { useEffect, useState } from 'react';
import { getAllProducts } from '../../services/productService';
import { Product } from '../../types/Product';
import { CartItem } from '../../types/CartItem';
import { loadStripe } from '@stripe/stripe-js';
import './ProductListing.css';

const stripePromise = loadStripe('pk_test_51QygQfDUUW08StDnMGHFw7Yy94fPIZmQZiNjfnMHoKN8z2CoyuVDOCRWzglYPHJ61YqZ6YmrStJt7ez7tp4pKzh600Ju0Ph7cw');

const ProductsPage: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [cart, setCart] = useState<CartItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  async function initStripe() {
    const stripe = await stripePromise;
    console.log('Stripe loaded:', stripe); // ✅ Now this logs the actual Stripe instance
  }
  
  initStripe();

  useEffect(() => {
    getAllProducts()
      .then(setProducts)
      .catch(() => setError("Failed to load products"))
      .finally(() => setLoading(false));
  }, []);

  const handleAddToCart = (product: Product) => {
    setCart((prevCart) => {
      const existing = prevCart.find(item => item.product.id === product.id);
      if (existing) {
        return prevCart.map(item =>
          item.product.id === product.id
            ? { ...item, quantity: item.quantity + 1 }
            : item
        );
      } else {
        return [...prevCart, { product, quantity: 1 }];
      }
    });
  };

  const handleCheckout = async () => {
    const stripe = await stripePromise;
  
    const items = cart.map(item => ({
      priceId: item.product.priceId,
      quantity: item.quantity,
      itemId: item.product.id
    }));
  
    console.log('Sending to backend:', { items });
  
    try {
      const response = await fetch('https://localhost:5001/stripe/checkout', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ items, currency: "usd" }) 
      });
  
      const data = await response.json();
      const sessionId = data.value; 
  
      if (stripe && sessionId) {
        await stripe.redirectToCheckout({ sessionId });
      } else {
        console.error('Invalid session ID:', data);
        alert('Stripe session ID missing!');
      }
    } catch (err) {
      console.error('Checkout error:', err);
      alert('Checkout failed.');
    }
  };

  return (
    <div className="products-container">
      <h2 className="products-title">Our Best Products</h2>

      {loading && <p className="loading">Loading...</p>}
      {error && <p className="error">{error}</p>}

      <div className="products-grid">
        {products.map((product) => (
          <div key={product.id} className="product-card">
            <img
              src={product.images?.[0] || 'https://via.placeholder.com/300'}
              alt={product.name}
              className="product-image"
            />
            <div className="product-info">
              <h3 className="product-name">{product.name}</h3>
              <p className="product-desc">{product.description}</p>
              <p className="product-price">{product.currency}{product.price}</p>
              <button className="add-button" onClick={() => handleAddToCart(product)}>
                Add to Cart
              </button>
            </div>
          </div>
        ))}
      </div>

      <div className="cart-container">
        <h3 className="cart-title">🛒 Shopping Cart</h3>
        {cart.length === 0 ? (
          <p className="empty-cart">Your cart is empty.</p>
        ) : (
          <>
            <ul className="cart-list">
              {cart.map(({ product, quantity }) => (
                <li key={product.id} className="cart-item">
                  <span>{product.name}</span>
                  <span>x {quantity}</span>
                </li>
              ))}
            </ul>
            <button className="checkout-button" onClick={handleCheckout}>
              Proceed to Checkout
            </button>
          </>
        )}
      </div>
    </div>
  );
};

export default ProductsPage;
