import { useMemo, useState } from 'react';
import useSWR, { useSWRConfig } from 'swr';
import AdminHeader from '../components/AdminHeader.jsx';
import { buildApiUrl, fetchJson, normalizePayload, requestJson } from '../services/api.js';

function AdminProductsPage() {
  const { mutate } = useSWRConfig();
  const { data, error } = useSWR(buildApiUrl('/api/Products'), fetchJson);
  const products = useMemo(() => normalizePayload(data), [data]);

  const [form, setForm] = useState({
    name: '',
    images: '',
    description: '',
    unit: '',
    inventoryItemId: '',
  });
  const [editing, setEditing] = useState(null);
  const [message, setMessage] = useState('');

  function updateForm(key, value) {
    setForm((prev) => ({ ...prev, [key]: value }));
  }

  async function handleCreate(event) {
    event.preventDefault();
    setMessage('');
    if (!form.name.trim()) {
      setMessage('Product name is required.');
      return;
    }
    await requestJson('/api/Products', {
      method: 'POST',
      body: JSON.stringify({
        name: form.name,
        images: form.images,
        description: form.description,
        unit: Number(form.unit || 0),
        inventoryItemID: Number(form.inventoryItemId || 0),
      }),
    });
    setForm({ name: '', images: '', description: '', unit: '', inventoryItemId: '' });
    mutate(buildApiUrl('/api/Products'));
  }

  async function handleUpdate() {
    if (!editing) return;
    await requestJson(`/api/Products/${editing.id}`, {
      method: 'PUT',
      body: JSON.stringify({
        name: editing.name,
        images: editing.images,
        description: editing.description,
        unit: Number(editing.unit || 0),
        inventoryItemID: Number(editing.inventoryItemId || 0),
      }),
    });
    setEditing(null);
    mutate(buildApiUrl('/api/Products'));
  }

  async function handleDelete(productId) {
    await requestJson(`/api/Products/${productId}`, { method: 'DELETE' });
    mutate(buildApiUrl('/api/Products'));
  }

  return (
    <div className="relative min-h-screen bg-[var(--color-cream)] px-6 py-10">
      <div className="mx-auto flex w-full max-w-5xl flex-col gap-8">
        <AdminHeader />

        <form onSubmit={handleCreate} className="glass-panel flex flex-col gap-4 p-5">
          <label className="text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]">Create product</label>
          <div className="grid gap-3 sm:grid-cols-2">
            <input
              value={form.name}
              onChange={(event) => updateForm('name', event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
              placeholder="Name"
            />
            <input
              value={form.images}
              onChange={(event) => updateForm('images', event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
              placeholder="Image URL"
            />
            <input
              value={form.description}
              onChange={(event) => updateForm('description', event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
              placeholder="Description"
            />
            <input
              value={form.unit}
              onChange={(event) => updateForm('unit', event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
              placeholder="Unit"
            />
            <input
              value={form.inventoryItemId}
              onChange={(event) => updateForm('inventoryItemId', event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
              placeholder="Inventory Item ID"
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
          <div className="glass-panel p-5 text-sm text-[var(--color-terracotta)]">Failed to load products.</div>
        ) : (
          <div className="grid gap-4">
            {products.map((product) => {
              const id = product.id ?? product.Id;
              const name = product.name ?? product.Name;
              return (
                <div key={id} className="glass-panel flex flex-col gap-3 p-5">
                  <div className="flex flex-wrap items-center justify-between gap-3">
                    <div>
                      <p className="text-sm font-semibold text-[var(--color-night)]">{name}</p>
                      <p className="text-xs text-[var(--color-olive)]">{product.description ?? product.Description}</p>
                    </div>
                    <div className="flex gap-2">
                      <button
                        type="button"
                        onClick={() =>
                          setEditing({
                            id,
                            name: name ?? '',
                            images: product.images ?? product.Images ?? '',
                            description: product.description ?? product.Description ?? '',
                            unit: product.unit ?? product.Unit ?? '',
                            inventoryItemId: product.inventoryItemID ?? product.InventoryItemID ?? '',
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
                    <div className="grid gap-3 sm:grid-cols-2">
                      <input
                        value={editing.name}
                        onChange={(event) => setEditing({ ...editing, name: event.target.value })}
                        className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      />
                      <input
                        value={editing.images}
                        onChange={(event) => setEditing({ ...editing, images: event.target.value })}
                        className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      />
                      <input
                        value={editing.description}
                        onChange={(event) => setEditing({ ...editing, description: event.target.value })}
                        className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      />
                      <input
                        value={editing.unit}
                        onChange={(event) => setEditing({ ...editing, unit: event.target.value })}
                        className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      />
                      <input
                        value={editing.inventoryItemId}
                        onChange={(event) => setEditing({ ...editing, inventoryItemId: event.target.value })}
                        className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
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

export default AdminProductsPage;
