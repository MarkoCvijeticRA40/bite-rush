import React from 'react';
import { Product } from '../../types/Product';
import './ProductCard.css';

interface Props {
  product: Product;
  onBuy: (priceId: string) => void;
}

const ProductCard: React.FC<Props> = ({ product, onBuy }) => {
  return (
    <div className="product-card">
      <img src={product.imageUrl} alt={product.name} />
      <h3>{product.name}</h3>
      <p className="description">{product.description}</p>
      <p className="price">${(product.price / 100).toFixed(2)}</p>
      <button onClick={() => onBuy(product.priceId)}>Buy</button>
    </div>
  );
};

export default ProductCard;