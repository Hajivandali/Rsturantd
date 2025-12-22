const baseUrl = (import.meta.env.VITE_API_BASE_URL || '').replace(/\/$/, '');

export function buildApiUrl(path) {
  if (!baseUrl) return path;
  return `${baseUrl}${path}`;
}

export async function fetchJson(url) {
  const response = await fetch(url);
  if (!response.ok) {
    throw new Error(`Request failed: ${response.status}`);
  }
  return response.json();
}

export function normalizePayload(payload) {
  if (!payload) return [];
  const data = payload.data ?? payload.Data ?? payload;
  return Array.isArray(data) ? data : [];
}
