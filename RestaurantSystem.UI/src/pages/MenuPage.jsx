import { useMemo, useState } from 'react';
import { Link } from 'react-router-dom';
import { useMenusData } from '../hooks/useMenus.js';

function MenuPage() {
  const [openMenuId, setOpenMenuId] = useState(null);
  const [activeTag, setActiveTag] = useState('All');
  const { menus, prices, products, status } = useMenusData();

  const menuCards = useMemo(() => {
    return menus.map((menu) => {
      const id = menu.id ?? menu.Id;
      const title = menu.title ?? menu.Title ?? 'Menu';
      const items = menu.menuItems ?? menu.MenuItems ?? [];

      const menuItems = items.map((item) => {
        const productId = item.productId ?? item.ProductId;
        const directProduct = item.product ?? item.Product;
        const mappedProduct = productId != null ? products.get(Number(productId)) : null;
        const product = directProduct ?? mappedProduct ?? {};
        const price = productId != null ? prices.get(Number(productId)) : null;

        return {
          id: item.id ?? item.Id ?? productId ?? Math.random(),
          name: product.name ?? product.Name ?? (productId != null ? `Item #${productId}` : 'Menu item'),
          description: product.description ?? product.Description ?? 'Freshly prepared.',
          image: product.images ?? product.Images ?? '',
          unit: product.unit ?? product.Unit ?? null,
          inventoryItemId: product.inventoryItemID ?? product.InventoryItemID ?? null,
          price,
          isActive: item.isActive ?? item.IsActive ?? false,
        };
      });

      return {
        id,
        title,
        count: items.length,
        items: menuItems,
      };
    });
  }, [menus, prices, products]);

  const tags = useMemo(() => {
    const titles = menuCards.map((menu) => menu.title).filter(Boolean);
    return ['All', ...new Set(titles)];
  }, [menuCards]);

  const filteredMenus = useMemo(() => {
    if (activeTag === 'All') return menuCards;
    return menuCards.filter((menu) => menu.title === activeTag);
  }, [menuCards, activeTag]);

  function toggleMenu(id) {
    setOpenMenuId((current) => (current === id ? null : id));
  }

  return (
    <div className="relative min-h-screen bg-[var(--color-cream)] px-6 py-10">
      <div className="mx-auto flex w-full max-w-5xl flex-col gap-10">
        <header className="flex flex-wrap items-center justify-between gap-4">
          <div>
            <p className="kicker">Menus</p>
            <h1 className="font-display text-4xl">Choose a menu</h1>
            <p className="mt-2 text-sm text-[var(--color-olive)]">
              {status === 'ready'
                ? 'Tap a menu title to reveal its items.'
                : 'We are loading the menus.'}
            </p>
          </div>
          <Link
            to="/"
            className="rounded-full border border-[var(--color-ink)] px-5 py-2 text-xs uppercase tracking-[0.2em]"
          >
            Back home
          </Link>
        </header>

        <div className="flex flex-wrap gap-3">
          {tags.map((tag) => (
            <button
              key={tag}
              type="button"
              onClick={() => {
                setActiveTag(tag);
                setOpenMenuId(null);
              }}
              className={`rounded-full px-4 py-2 text-xs uppercase tracking-[0.2em] ${
                activeTag === tag
                  ? 'bg-[var(--color-ink)] text-[var(--color-cream)]'
                  : 'border border-[var(--color-olive)] text-[var(--color-olive)]'
              }`}
            >
              {tag}
            </button>
          ))}
        </div>

        {status === 'error' ? (
          <div className="glass-panel p-6">
            <p className="text-sm text-[var(--color-olive)]">
              Could not load menus. Check the API base URL or try again.
            </p>
          </div>
        ) : (
          <div className="grid gap-5">
            {filteredMenus.map((menu) => (
              <div key={menu.id ?? menu.title} className="glass-panel p-5">
                <button
                  type="button"
                  onClick={() => toggleMenu(menu.id)}
                  className="flex w-full items-center justify-between gap-4 text-left"
                >
                  <div>
                    <h2 className="font-display text-2xl text-[var(--color-night)]">{menu.title}</h2>
                    <p className="text-xs uppercase tracking-[0.25em] text-[var(--color-olive)]">
                      {menu.count} items
                    </p>
                  </div>
                  <span className="rounded-full border border-[var(--color-olive)] px-3 py-1 text-xs text-[var(--color-olive)]">
                    {openMenuId === menu.id ? 'Hide' : 'View'}
                  </span>
                </button>

                {openMenuId === menu.id ? (
                  <div className="mt-5 space-y-4">
                    {menu.items.length ? (
                      menu.items.map((item) => (
                        <div
                          key={item.id}
                          className="flex flex-col gap-3 rounded-2xl border border-white/70 bg-white/60 p-4 sm:flex-row sm:items-center sm:justify-between"
                        >
                          <div className="flex items-center gap-4">
                            <div className="h-16 w-16 overflow-hidden rounded-xl bg-[var(--color-mist)]">
                              {item.image ? (
                                <img
                                  src={item.image}
                                  alt={item.name}
                                  className="h-full w-full object-cover"
                                  loading="lazy"
                                />
                              ) : null}
                            </div>
                            <div>
                              <p className="text-sm font-semibold text-[var(--color-night)]">{item.name}</p>
                              <p className="text-xs text-[var(--color-olive)]">{item.description}</p>
                              <div className="mt-1 text-[10px] uppercase tracking-[0.25em] text-[var(--color-olive)]">
                                {item.unit != null ? `Unit ${item.unit}` : 'Unit n/a'}
                                {item.inventoryItemId != null ? ` · Inv ${item.inventoryItemId}` : ''}
                              </div>
                            </div>
                          </div>
                          <div className="flex items-center gap-4 text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]">
                            <span>{item.isActive ? 'Active' : 'Inactive'}</span>
                            <span className="text-sm font-semibold text-[var(--color-terracotta)]">
                              {item.price != null ? `$${item.price}` : 'Market'}
                            </span>
                          </div>
                        </div>
                      ))
                    ) : (
                      <p className="text-sm text-[var(--color-olive)]">No items available.</p>
                    )}
                  </div>
                ) : null}
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default MenuPage;
