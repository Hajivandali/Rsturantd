import { useMemo, useState } from 'react';
import useSWR, { useSWRConfig } from 'swr';
import AdminHeader from '../components/AdminHeader.jsx';
import { buildApiUrl, fetchJson, normalizePayload, requestJson } from '../services/api.js';

function AdminPricesPage() {
  const { mutate } = useSWRConfig();
  const { data: pricesPayload, error } = useSWR(buildApiUrl('/api/Prices'), fetchJson);
  const { data: productsPayload } = useSWR(buildApiUrl('/api/Products'), fetchJson);

  const prices = useMemo(() => normalizePayload(pricesPayload), [pricesPayload]);
  const products = useMemo(() => normalizePayload(productsPayload), [productsPayload]);

  const [productId, setProductId] = useState('');
  const [amount, setAmount] = useState('');
  const [editing, setEditing] = useState(null);
  const [message, setMessage] = useState('');

  async function handleCreate(event) {
    event.preventDefault();
    setMessage('');
    if (!productId || !amount) {
      setMessage('Product and amount are required.');
      return;
    }
    await requestJson('/api/Prices', {
      method: 'POST',
      body: JSON.stringify({
        productReference: Number(productId),
        amount: Number(amount),
      }),
    });
    setProductId('');
    setAmount('');
    mutate(buildApiUrl('/api/Prices'));
  }

  async function handleUpdate() {
    if (!editing) return;
    await requestJson(`/api/Prices/${editing.id}`, {
      method: 'PUT',
      body: JSON.stringify({
        amount: Number(editing.amount),
      }),
    });
    setEditing(null);
    mutate(buildApiUrl('/api/Prices'));
  }

  async function handleDelete(priceId) {
    await requestJson(`/api/Prices/${priceId}`, { method: 'DELETE' });
    mutate(buildApiUrl('/api/Prices'));
  }

  return (
    <div className="relative min-h-screen bg-[var(--color-cream)] px-6 py-10">
      <div className="mx-auto flex w-full max-w-4xl flex-col gap-8">
        <AdminHeader />

        <form onSubmit={handleCreate} className="glass-panel flex flex-col gap-3 p-5">
          <label className="text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]">Create price</label>
          <div className="grid gap-3 sm:grid-cols-2">
            <select
              value={productId}
              onChange={(event) => setProductId(event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
            >
              <option value="">Select product</option>
              {products.map((product) => (
                <option key={product.id ?? product.Id} value={product.id ?? product.Id}>
                  {product.name ?? product.Name}
                </option>
              ))}
            </select>
            <input
              value={amount}
              onChange={(event) => setAmount(event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
              placeholder="Amount"
            />
          </div>
          <button
            type="submit"
            className="w-fit rounded-full bg-[var(--color-ink)] px-5 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-cream)]"
          >
            Create
          </button>
          {message ? <p className="text-xs text-[var(--color-terracotta)]">{message}</p> : null}
        </form>

        {error ? (
          <div className="glass-panel p-5 text-sm text-[var(--color-terracotta)]">Failed to load prices.</div>
        ) : (
          <div className="grid gap-4">
            {prices.map((price) => {
              const id = price.id ?? price.Id;
              const productIdValue = price.productId ?? price.ProductId ?? price.productReference ?? price.ProductReference;
              const product = products.find((p) => (p.id ?? p.Id) === productIdValue);
              return (
                <div key={id} className="glass-panel flex flex-col gap-3 p-5">
                  <div className="flex flex-wrap items-center justify-between gap-3">
                    <div>
                      <p className="text-sm font-semibold text-[var(--color-night)]">
                        {product?.name ?? product?.Name ?? 'Product'}
                      </p>
                      <p className="text-xs text-[var(--color-olive)]">Product ID: {productIdValue}</p>
                    </div>
                    <div className="flex gap-2">
                      <button
                        type="button"
                        onClick={() =>
                          setEditing({
                            id,
                            amount: price.amount ?? price.Amount ?? '',
                          })
                        }
                        className="rounded-full border border-[var(--color-olive)] px-3 py-1 text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]"
                      >
                        Edit
                      </button>
                      <button
                        type="button"
                        onClick={() => handleDelete(id)}
                        className="rounded-full border border-[var(--color-terracotta)] px-3 py-1 text-xs uppercase tracking-[0.2em] text-[var(--color-terracotta)]"
                      >
                        Delete
                      </button>
                    </div>
                  </div>

                  {editing?.id === id ? (
                    <div className="flex flex-col gap-2 sm:flex-row">
                      <input
                        value={editing.amount}
                        onChange={(event) => setEditing({ ...editing, amount: event.target.value })}
                        className="flex-1 rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      />
                      <button
                        type="button"
                        onClick={handleUpdate}
                        className="rounded-full bg-[var(--color-ink)] px-4 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-cream)]"
                      >
                        Save
                      </button>
                      <button
                        type="button"
                        onClick={() => setEditing(null)}
                        className="rounded-full border border-[var(--color-olive)] px-4 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]"
                      >
                        Cancel
                      </button>
                    </div>
                  ) : null}
                </div>
              );
            })}
          </div>
        )}
      </div>
    </div>
  );
}

export default AdminPricesPage;
