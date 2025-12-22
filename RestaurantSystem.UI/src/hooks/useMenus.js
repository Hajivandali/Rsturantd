import useSWR from 'swr';
import { buildApiUrl, fetchJson, normalizePayload } from '../services/api.js';

function buildPriceMap(prices) {
  return prices.reduce((acc, price) => {
    const productId = price.productReference ?? price.ProductReference ?? price.productId ?? price.ProductId;
    const amount = price.amount ?? price.Amount;
    if (productId != null && amount != null) {
      acc.set(Number(productId), amount);
    }
    return acc;
  }, new Map());
}

function buildProductMap(products) {
  return products.reduce((acc, product) => {
    const id = product.id ?? product.Id;
    if (id != null) {
      acc.set(Number(id), product);
    }
    return acc;
  }, new Map());
}

export function useMenusData() {
  const { data: menusPayload, error: menusError, isLoading: menusLoading } = useSWR(
    buildApiUrl('/api/Menus'),
    fetchJson
  );
  const { data: pricesPayload } = useSWR(buildApiUrl('/api/Prices'), fetchJson);
  const { data: productsPayload } = useSWR(buildApiUrl('/api/Products'), fetchJson);

  const menus = normalizePayload(menusPayload);
  const prices = buildPriceMap(normalizePayload(pricesPayload));
  const products = buildProductMap(normalizePayload(productsPayload));

  return {
    menus,
    prices,
    products,
    status: menusError ? 'error' : menusLoading ? 'loading' : 'ready',
  };
}
