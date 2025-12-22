import { useMemo } from 'react';
import { Link, Route, Routes } from 'react-router-dom';
import MenuPage from './pages/MenuPage.jsx';
import { useMenusData } from './hooks/useMenus.js';

const fallbackMenus = [
  {
    title: 'Harvest Brunch',
    note: 'Citrus ricotta, wild greens, toasted einkorn.',
    emphasis: 'Bright, light, seasonal.',
  },
  {
    title: 'Stonefire Supper',
    note: 'Smoked tomato broth, charred roots, herb oil.',
    emphasis: 'Slow heat, deep flavor.',
  },
  {
    title: 'Coastal Night',
    note: 'Saffron mussels, fennel, grilled lemon.',
    emphasis: 'Ocean-forward and clean.',
  },
];

const chefPicks = [
  {
    name: 'Rosemary Flatbread',
    desc: 'Olive oil, warm olives, citrus salt.',
    price: '$9',
  },
  {
    name: 'Seared Halloumi',
    desc: 'Blood orange, pistachio dust, wild honey.',
    price: '$14',
  },
  {
    name: 'Cedar Trout',
    desc: 'Charred leek, smoked butter, grains.',
    price: '$22',
  },
];

const moments = [
  {
    title: 'Open Flame Kitchen',
    detail: 'Watch the live fire from every table.',
  },
  {
    title: 'Market-Driven Menu',
    detail: 'We rewrite the menu every morning.',
  },
  {
    title: 'Private Table',
    detail: 'Eight seats, curated course journey.',
  },
];

function HomePage() {
  const { menus, status } = useMenusData();

  const menuCards = useMemo(() => {
    if (!menus.length) return fallbackMenus;

    return menus.slice(0, 3).map((menu) => {
      const title = menu.title ?? menu.Title ?? 'Seasonal Menu';
      const items = menu.menuItems ?? menu.MenuItems ?? [];
      const count = items.length;
      const itemNames = items
        .map((item) => item.product?.name ?? item.Product?.Name)
        .filter(Boolean)
        .slice(0, 2);

      return {
        title,
        note: count
          ? `${count} curated selections inside.`
          : 'A curated selection is being prepared.',
        emphasis: 'Updated daily from the market.',
        items: itemNames,
      };
    });
  }, [menus]);

  return (
    <div className="relative min-h-screen overflow-hidden bg-[var(--color-cream)]">
      <div className="pointer-events-none absolute -top-32 left-10 h-80 w-80 rounded-full bg-[radial-gradient(circle,rgba(199,109,58,0.35),transparent_65%)] blur-2xl animate-float-slow" />
      <div className="pointer-events-none absolute -bottom-20 right-0 h-96 w-96 rounded-full bg-[radial-gradient(circle,rgba(184,138,79,0.35),transparent_60%)] blur-3xl" />
      <div className="pointer-events-none absolute inset-0 bg-grain" />

      <div className="relative z-10 mx-auto flex w-full max-w-6xl flex-col gap-16 px-6 py-10">
        <header className="flex items-center justify-between">
          <div className="flex items-center gap-3">
            <div className="flex h-10 w-10 items-center justify-center rounded-full border border-[var(--color-ink)] text-lg font-semibold">
              S
            </div>
            <div>
              <p className="font-display text-lg tracking-wide">Saffron &amp; Stone</p>
              <p className="text-xs text-[var(--color-olive)]">Market kitchen · since 2012</p>
            </div>
          </div>
          <nav className="hidden items-center gap-8 text-sm text-[var(--color-olive)] md:flex">
            <a className="hover:text-[var(--color-ink)]" href="#menu">
              Menu
            </a>
            <a className="hover:text-[var(--color-ink)]" href="#experience">
              Experience
            </a>
            <a className="hover:text-[var(--color-ink)]" href="#reserve">
              Reserve
            </a>
          </nav>
          <button className="rounded-full bg-[var(--color-ink)] px-5 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-cream)]">
            Book Table
          </button>
        </header>

        <section className="grid items-center gap-10 lg:grid-cols-[1.1fr_0.9fr]">
          <div className="flex flex-col gap-6">
            <p className="kicker">Seasonal menu</p>
            <h1 className="font-display text-4xl leading-tight md:text-5xl">
              A modern menu shaped by fire, citrus, and the morning market.
            </h1>
            <p className="max-w-xl text-base text-[var(--color-olive)]">
              We keep the room calm and the flavors bold. Each course is crafted to pair with our
              small-batch wines and an open kitchen glow.
            </p>
            <div className="flex flex-wrap gap-4">
              <Link
                to="/menu"
                className="rounded-full bg-[var(--color-terracotta)] px-6 py-3 text-sm font-medium text-white"
              >
                Explore Menu
              </Link>
              <button className="rounded-full border border-[var(--color-ink)] px-6 py-3 text-sm font-medium">
                Order Pickup
              </button>
            </div>
            <div className="flex flex-wrap gap-6 text-xs uppercase tracking-[0.25em] text-[var(--color-olive)]">
              <span>Open 5 PM - 11 PM</span>
              <span>Old Town District</span>
            </div>
          </div>

          <div className="glass-panel grid gap-6 p-6 md:p-8">
            <div className="flex items-center justify-between">
              <div>
                <p className="kicker">Tonight</p>
                <h2 className="font-display text-2xl">Chef's Counter</h2>
              </div>
              <span className="rounded-full border border-[var(--color-olive)] px-3 py-1 text-xs text-[var(--color-olive)]">
                Limited seats
              </span>
            </div>
            <div className="soft-divider" />
            <div className="space-y-4">
              {chefPicks.map((item) => (
                <div key={item.name} className="flex items-start justify-between gap-4">
                  <div>
                    <p className="text-sm font-semibold text-[var(--color-ink)]">{item.name}</p>
                    <p className="text-sm text-[var(--color-olive)]">{item.desc}</p>
                  </div>
                  <span className="text-sm font-semibold text-[var(--color-terracotta)]">{item.price}</span>
                </div>
              ))}
            </div>
          </div>
        </section>

        <section id="menu" className="grid gap-8">
          <div className="flex flex-wrap items-center justify-between gap-4">
            <div>
              <p className="kicker">Menu preview</p>
              <h2 className="font-display text-3xl">Menus that change with the market.</h2>
            </div>
            <span className="rounded-full bg-[var(--color-sand)] px-4 py-2 text-xs uppercase tracking-[0.2em] text-[var(--color-olive)]">
              {status === 'ready' ? 'Synced with API' : 'Studio menu'}
            </span>
          </div>

          <div className="grid gap-6 md:grid-cols-3">
            {menuCards.map((menu, index) => (
              <article
                key={menu.title}
                className="glass-panel flex h-full flex-col gap-4 p-6 animate-fade-up"
                style={{ animationDelay: `${index * 120}ms` }}
              >
                <h3 className="font-display text-2xl text-[var(--color-night)]">{menu.title}</h3>
                <p className="text-sm text-[var(--color-olive)]">{menu.note}</p>
                {menu.items?.length ? (
                  <div className="flex flex-col gap-2 text-xs text-[var(--color-olive)]">
                    {menu.items.map((item) => (
                      <span key={item}>• {item}</span>
                    ))}
                  </div>
                ) : null}
                <p className="text-xs uppercase tracking-[0.3em] text-[var(--color-terracotta)]">
                  {menu.emphasis}
                </p>
              </article>
            ))}
          </div>
        </section>

        <section id="experience" className="grid gap-8 lg:grid-cols-[0.9fr_1.1fr]">
          <div className="glass-panel flex flex-col gap-5 p-6 md:p-8">
            <p className="kicker">Experience</p>
            <h2 className="font-display text-3xl">A warm room built for slow evenings.</h2>
            <p className="text-sm text-[var(--color-olive)]">
              Soft brass lighting, open fire, and a menu that follows the seasons. Let us build a
              tasting for your table or keep it casual with shared plates.
            </p>
            <div className="space-y-4">
              {moments.map((moment) => (
                <div key={moment.title} className="rounded-2xl border border-white/70 bg-white/50 p-4">
                  <p className="text-sm font-semibold text-[var(--color-night)]">{moment.title}</p>
                  <p className="text-xs text-[var(--color-olive)]">{moment.detail}</p>
                </div>
              ))}
            </div>
          </div>

          <div className="relative overflow-hidden rounded-[32px] bg-[var(--color-night)] p-8 text-[var(--color-cream)]">
            <div className="absolute -top-10 right-0 h-40 w-40 rounded-full bg-[radial-gradient(circle,rgba(247,241,232,0.2),transparent_70%)]" />
            <p className="kicker text-[var(--color-brass)]">Signature pairing</p>
            <h3 className="font-display text-3xl">Saffron pairing flight</h3>
            <p className="mt-4 text-sm text-[var(--color-mist)]">
              Three pours from Mediterranean vineyards with grilled citrus and cumin-laced
              appetizers.
            </p>
            <div className="mt-6 flex items-center gap-4">
              <span className="rounded-full border border-[var(--color-brass)] px-4 py-2 text-xs uppercase tracking-[0.2em]">
                Reserve flight
              </span>
              <span className="text-sm text-[var(--color-mist)]">$28 per guest</span>
            </div>
          </div>
        </section>

        <section
          id="reserve"
          className="relative overflow-hidden rounded-[36px] border border-white/70 bg-[var(--color-sand)] p-8 md:p-10"
        >
          <div className="absolute -left-10 top-6 h-28 w-28 rounded-full bg-[radial-gradient(circle,rgba(199,109,58,0.3),transparent_65%)]" />
          <div className="grid gap-6 lg:grid-cols-[1.2fr_0.8fr]">
            <div>
              <p className="kicker">Reserve</p>
              <h2 className="font-display text-3xl">Save a seat for golden hour.</h2>
              <p className="mt-3 text-sm text-[var(--color-olive)]">
                Bookings open two weeks ahead. Tell us if you are celebrating and we will curate a
                course just for you.
              </p>
            </div>
            <div className="flex flex-col gap-3">
              <button className="rounded-full bg-[var(--color-ink)] px-6 py-3 text-sm font-medium text-[var(--color-cream)]">
                Reserve Online
              </button>
              <button className="rounded-full border border-[var(--color-ink)] px-6 py-3 text-sm font-medium">
                Call 555-0144
              </button>
              <p className="text-xs text-[var(--color-olive)]">
                API endpoint: {import.meta.env.VITE_API_BASE_URL || 'same origin'}
              </p>
            </div>
          </div>
        </section>

        <footer className="flex flex-wrap items-center justify-between gap-4 border-t border-[var(--color-mist)] pt-6 text-xs text-[var(--color-olive)]">
          <span>Saffron &amp; Stone · 124 Ember Lane · Open daily</span>
          <span>Instagram · @saffronstone</span>
        </footer>
      </div>
    </div>
  );
}

function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/menu" element={<MenuPage />} />
    </Routes>
  );
}

export default App;
