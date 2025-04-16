export async function fetchData<T>(
  url: string,
  method: "GET" | "POST" | "PUT" | "DELETE" = "GET",
  body?: unknown
): Promise<T | null> {
  try {
    const options: RequestInit = {
      method,
      cache: "no-store",
      credentials: "include",
      headers: { "Content-Type": "application/json" },
    };

    if (body) {
      options.body = JSON.stringify(body);
    }

    const res = await fetch(url, options);

    if (!res.ok) {
      throw new Error(`Erreur ${res.status}: ${res.statusText}`);
    }

    const isJson = res.headers
      .get("content-type")
      ?.includes("application/json");

    return isJson ? await res.json() : null;
  } catch (error) {
    console.error("Erreur fetchData:", error);
    return null;
  }
}
