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

export async function requestJson(path, options = {}) {
  const response = await fetch(buildApiUrl(path), {
    headers: { 'Content-Type': 'application/json', ...(options.headers || {}) },
    ...options,
  });
  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || `Request failed: ${response.status}`);
  }
  if (response.status === 204) return null;
  return response.json();
}

export function normalizePayload(payload) {
  if (!payload) return [];
  const data = payload.data ?? payload.Data ?? payload;
  return Array.isArray(data) ? data : [];
}
