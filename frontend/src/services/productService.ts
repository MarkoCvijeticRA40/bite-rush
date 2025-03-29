import { Product } from "../types/Product";
import BASE_API_URL from '../config';

export async function getAllProducts(): Promise<Product[]> {
  const response = await fetch(`${BASE_API_URL}/products`);
  if (!response.ok) {
    throw new Error('Failed to fetch products');
  }
  return response.json();
}
