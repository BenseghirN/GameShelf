import { UserGame } from "@/types/UserGame";
import { fetchData } from "@/utils/fetchData";
import { useEffect, useState } from "react";

export function useUserGame(open: boolean, gameId?: string) {
  const [userGame, setUserGame] = useState<UserGame | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (!gameId || !open) return;

    const load = async () => {
      setLoading(true);
      setError(null);

      const result = await fetchData<UserGame>(
        `${import.meta.env.VITE_API_BASE_URL}/api/v1/Library/game/${gameId}`
      );

      if (result) {
        setUserGame(result);
      } else {
        setUserGame(null);
        setError("Impossible de récupérer les données du jeu.");
      }

      setLoading(false);
    };

    load();
  }, [gameId, open]);

  return { userGame, loading, error };
}
