import { NavLink } from 'react-router-dom';

const links = [
  { to: '/admin/menus', label: 'Menus' },
  { to: '/admin/menu-items', label: 'Menu Items' },
  { to: '/admin/products', label: 'Products' },
  { to: '/admin/prices', label: 'Prices' },
];

function AdminHeader() {
  return (
    <header className="flex flex-wrap items-center justify-between gap-4">
      <div>
        <p className="kicker">Admin</p>
        <h1 className="font-display text-3xl">Menu Manager</h1>
      </div>
      <nav className="flex flex-wrap gap-2">
        {links.map((link) => (
          <NavLink
            key={link.to}
            to={link.to}
            className={({ isActive }) =>
              `rounded-full px-4 py-2 text-xs uppercase tracking-[0.2em] ${
                isActive
                  ? 'bg-[var(--color-ink)] text-[var(--color-cream)]'
                  : 'border border-[var(--color-olive)] text-[var(--color-olive)]'
              }`
            }
          >
            {link.label}
          </NavLink>
        ))}
      </nav>
    </header>
  );
}

export default AdminHeader;
