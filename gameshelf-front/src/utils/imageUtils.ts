export function getGameFullImageUrl(relativePath: string): string {
  if (!relativePath) return "/images/games/default-cover.webp";
  const baseUrl = import.meta.env.VITE_API_BASE_URL;
  return `${baseUrl}${relativePath}`;
}

export function getPlatformFullImageUrl(relativePath: string | null): string {
  if (!relativePath) return "/images/platforms/default-platform.png";
  const baseUrl = import.meta.env.VITE_API_BASE_URL;
  return `${baseUrl}${relativePath}`;
}
