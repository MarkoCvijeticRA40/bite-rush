import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders Bite Rush app', () => {
  render(<App />);
  const headingElement = screen.getByText(/bite rush/i);
  expect(headingElement).toBeTruthy();
});
