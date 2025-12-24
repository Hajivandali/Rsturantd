import { useMemo, useState } from 'react';
import useSWR, { useSWRConfig } from 'swr';
import AdminHeader from '../components/AdminHeader.jsx';
import { buildApiUrl, fetchJson, normalizePayload, requestJson } from '../services/api.js';

function AdminMenuItemsPage() {
  const { mutate } = useSWRConfig();
  const { data: menuItemsPayload, error: menuItemsError } = useSWR(buildApiUrl('/api/MenuItems'), fetchJson);
  const { data: menusPayload } = useSWR(buildApiUrl('/api/Menus'), fetchJson);
  const { data: productsPayload } = useSWR(buildApiUrl('/api/Products'), fetchJson);
  const { data: pricesPayload } = useSWR(buildApiUrl('/api/Prices'), fetchJson);

  const menuItems = useMemo(() => normalizePayload(menuItemsPayload), [menuItemsPayload]);
  const menus = useMemo(() => normalizePayload(menusPayload), [menusPayload]);
  const products = useMemo(() => normalizePayload(productsPayload), [productsPayload]);
  const prices = useMemo(() => normalizePayload(pricesPayload), [pricesPayload]);

  const [menuId, setMenuId] = useState('');
  const [productId, setProductId] = useState('');
  const [priceId, setPriceId] = useState('');
  const [isActive, setIsActive] = useState(true);
  const [editingItem, setEditingItem] = useState(null);
  const [message, setMessage] = useState('');

  const priceOptions = useMemo(() => {
    const id = Number(productId || editingItem?.productId);
    if (!id) return [];
    return prices.filter((price) =>
      (price.productId ?? price.ProductId ?? price.productReference ?? price.ProductReference) === id
    );
  }, [prices, productId, editingItem]);

  async function handleCreate(event) {
    event.preventDefault();
    setMessage('');
    if (!menuId || !productId) {
      setMessage('Menu and Product are required.');
      return;
    }
    await requestJson('/api/MenuItems', {
      method: 'POST',
      body: JSON.stringify({
        menuId: Number(menuId),
        productId: Number(productId),
        isActive,
      }),
    });
    setMenuId('');
    setProductId('');
    setPriceId('');
    setIsActive(true);
    mutate(buildApiUrl('/api/MenuItems'));
    mutate(buildApiUrl('/api/Menus'));
  }

  async function handleUpdate() {
    if (!editingItem) return;
    setMessage('');
    await requestJson(`/api/MenuItems/${editingItem.id}`, {
      method: 'PUT',
      body: JSON.stringify({
        menuId: Number(editingItem.menuId),
        productId: Number(editingItem.productId),
        isActive: Boolean(editingItem.isActive),
      }),
    });
    setEditingItem(null);
    mutate(buildApiUrl('/api/MenuItems'));
    mutate(buildApiUrl('/api/Menus'));
  }

  async function handleDelete(itemId) {
    setMessage('');
    await requestJson(`/api/MenuItems/${itemId}`, { method: 'DELETE' });
    mutate(buildApiUrl('/api/MenuItems'));
    mutate(buildApiUrl('/api/Menus'));
  }

  return (
    <div className="relative min-h-screen bg-[var(--color-cream)] px-6 py-10">
      <div className="mx-auto flex w-full max-w-5xl flex-col gap-8">
        <AdminHeader />

        <form onSubmit={handleCreate} className="glass-panel flex flex-col gap-3 p-5">
          <label className="text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]">Create menu item</label>
          <div className="grid gap-3 sm:grid-cols-3">
            <select
              value={menuId}
              onChange={(event) => setMenuId(event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
            >
              <option value="">Select menu</option>
              {menus.map((menu) => (
                <option key={menu.id ?? menu.Id} value={menu.id ?? menu.Id}>
                  {menu.title ?? menu.Title}
                </option>
              ))}
            </select>
            <select
              value={productId}
              onChange={(event) => {
                setProductId(event.target.value);
                setPriceId('');
              }}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
            >
              <option value="">Select product</option>
              {products.map((product) => (
                <option key={product.id ?? product.Id} value={product.id ?? product.Id}>
                  {product.name ?? product.Name}
                </option>
              ))}
            </select>
            <select
              value={priceId}
              onChange={(event) => setPriceId(event.target.value)}
              className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
            >
              <option value="">Select price</option>
              {priceOptions.map((price) => (
                <option key={price.id ?? price.Id} value={price.id ?? price.Id}>
                  {price.amount ?? price.Amount}
                </option>
              ))}
            </select>
            <label className="flex items-center gap-2 text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]">
              <input
                type="checkbox"
                checked={isActive}
                onChange={(event) => setIsActive(event.target.checked)}
              />
              Active
            </label>
          </div>
          <button
            type="submit"
            className="w-fit rounded-full bg-[var(--color-ink)] px-5 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-cream)]"
          >
            Create
          </button>
          {message ? <p className="text-xs text-[var(--color-terracotta)]">{message}</p> : null}
        </form>

        {menuItemsError ? (
          <div className="glass-panel p-5 text-sm text-[var(--color-terracotta)]">Failed to load menu items.</div>
        ) : (
          <div className="grid gap-4">
            {menuItems.map((item) => {
              const id = item.id ?? item.Id;
              const menu = menus.find((m) => (m.id ?? m.Id) === (item.menuId ?? item.MenuId));
              const product = products.find((p) => (p.id ?? p.Id) === (item.productId ?? item.ProductId));
              return (
                <div key={id} className="glass-panel flex flex-col gap-3 p-5">
                  <div className="flex flex-wrap items-center justify-between gap-3">
                    <div>
                      <p className="text-sm font-semibold text-[var(--color-night)]">{product?.name ?? product?.Name ?? 'Product'}</p>
                      <p className="text-xs text-[var(--color-olive)]">{menu?.title ?? menu?.Title ?? 'Menu'}</p>
                    </div>
                    <div className="flex gap-2">
                      <button
                        type="button"
                        onClick={() =>
                          setEditingItem({
                            id,
                            menuId: item.menuId ?? item.MenuId,
                            productId: item.productId ?? item.ProductId,
                            priceId: '',
                            isActive: item.isActive ?? item.IsActive ?? false,
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

                  {editingItem?.id === id ? (
                    <div className="grid gap-3 sm:grid-cols-3">
                      <select
                        value={editingItem.menuId}
                        onChange={(event) => setEditingItem({ ...editingItem, menuId: event.target.value })}
                        className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      >
                        {menus.map((menu) => (
                          <option key={menu.id ?? menu.Id} value={menu.id ?? menu.Id}>
                            {menu.title ?? menu.Title}
                          </option>
                        ))}
                      </select>
                      <select
                        value={editingItem.productId}
                        onChange={(event) =>
                          setEditingItem({ ...editingItem, productId: event.target.value, priceId: '' })
                        }
                        className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      >
                        {products.map((product) => (
                          <option key={product.id ?? product.Id} value={product.id ?? product.Id}>
                            {product.name ?? product.Name}
                          </option>
                        ))}
                      </select>
                      <select
                        value={editingItem.priceId}
                        onChange={(event) => setEditingItem({ ...editingItem, priceId: event.target.value })}
                        className="rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      >
                        <option value="">Select price</option>
                        {priceOptions.map((price) => (
                          <option key={price.id ?? price.Id} value={price.id ?? price.Id}>
                            {price.amount ?? price.Amount}
                          </option>
                        ))}
                      </select>
                      <label className="flex items-center gap-2 text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]">
                        <input
                          type="checkbox"
                          checked={Boolean(editingItem.isActive)}
                          onChange={(event) => setEditingItem({ ...editingItem, isActive: event.target.checked })}
                        />
                        Active
                      </label>
                      <button
                        type="button"
                        onClick={handleUpdate}
                        className="rounded-full bg-[var(--color-ink)] px-4 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-cream)]"
                      >
                        Save
                      </button>
                      <button
                        type="button"
                        onClick={() => setEditingItem(null)}
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

export default AdminMenuItemsPage;
