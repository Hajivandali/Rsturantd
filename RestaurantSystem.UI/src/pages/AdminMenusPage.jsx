import { useMemo, useState } from 'react';
import useSWR, { useSWRConfig } from 'swr';
import AdminHeader from '../components/AdminHeader.jsx';
import { buildApiUrl, fetchJson, normalizePayload, requestJson } from '../services/api.js';

function AdminMenusPage() {
  const { mutate } = useSWRConfig();
  const { data, error } = useSWR(buildApiUrl('/api/Menus'), fetchJson);
  const menus = useMemo(() => normalizePayload(data), [data]);
  const [title, setTitle] = useState('');
  const [editingId, setEditingId] = useState(null);
  const [editingTitle, setEditingTitle] = useState('');
  const [message, setMessage] = useState('');

  async function handleCreate(event) {
    event.preventDefault();
    setMessage('');
    const nextTitle = title.trim();
    if (!nextTitle) {
      setMessage('Menu title is required.');
      return;
    }
    await requestJson('/api/Menus', {
      method: 'POST',
      body: JSON.stringify({ title: nextTitle }),
    });
    setTitle('');
    mutate(buildApiUrl('/api/Menus'));
  }

  async function handleUpdate(menuId) {
    setMessage('');
    const nextTitle = editingTitle.trim();
    if (!nextTitle) {
      setMessage('Menu title is required.');
      return;
    }
    await requestJson(`/api/Menus/${menuId}`, {
      method: 'PUT',
      body: JSON.stringify({ title: nextTitle }),
    });
    setEditingId(null);
    setEditingTitle('');
    mutate(buildApiUrl('/api/Menus'));
  }

  async function handleDelete(menuId) {
    setMessage('');
    await requestJson(`/api/Menus/${menuId}`, { method: 'DELETE' });
    mutate(buildApiUrl('/api/Menus'));
  }

  return (
    <div className="relative min-h-screen bg-[var(--color-cream)] px-6 py-10">
      <div className="mx-auto flex w-full max-w-4xl flex-col gap-8">
        <AdminHeader />

        <form onSubmit={handleCreate} className="glass-panel flex flex-col gap-3 p-5">
          <label className="text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]">Create menu</label>
          <div className="flex flex-col gap-3 sm:flex-row">
            <input
              value={title}
              onChange={(event) => setTitle(event.target.value)}
              className="flex-1 rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
              placeholder="Menu title"
            />
            <button
              type="submit"
              className="rounded-full bg-[var(--color-ink)] px-5 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-cream)]"
            >
              Create
            </button>
          </div>
          {message ? <p className="text-xs text-[var(--color-terracotta)]">{message}</p> : null}
        </form>

        {error ? (
          <div className="glass-panel p-5 text-sm text-[var(--color-terracotta)]">Failed to load menus.</div>
        ) : (
          <div className="grid gap-4">
            {menus.map((menu) => {
              const id = menu.id ?? menu.Id;
              const menuTitle = menu.title ?? menu.Title ?? 'Menu';
              return (
                <div key={id} className="glass-panel flex flex-col gap-3 p-5">
                  <div className="flex items-center justify-between">
                    <div>
                      <h2 className="font-display text-2xl text-[var(--color-night)]">{menuTitle}</h2>
                      <p className="text-xs uppercase tracking-[0.25em] text-[var(--color-olive)]">
                        {(menu.menuItems ?? menu.MenuItems ?? []).length} items
                      </p>
                    </div>
                    <div className="flex gap-2">
                      <button
                        type="button"
                        onClick={() => {
                          setEditingId(id);
                          setEditingTitle(menuTitle);
                        }}
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

                  {editingId === id ? (
                    <div className="flex flex-col gap-2 sm:flex-row">
                      <input
                        value={editingTitle}
                        onChange={(event) => setEditingTitle(event.target.value)}
                        className="flex-1 rounded-full border border-[var(--color-mist)] bg-white px-4 py-2 text-sm"
                      />
                      <button
                        type="button"
                        onClick={() => handleUpdate(id)}
                        className="rounded-full bg-[var(--color-ink)] px-4 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-cream)]"
                      >
                        Save
                      </button>
                      <button
                        type="button"
                        onClick={() => {
                          setEditingId(null);
                          setEditingTitle('');
                        }}
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

export default AdminMenusPage;
