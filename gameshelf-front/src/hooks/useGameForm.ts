import { useEffect, useState } from "react";
import { useToast } from "./useToas";
import { fetchData } from "@/utils/fetchData";

type EditableGameDto = {
  id?: string;
  titre: string;
  editeur: string;
  description: string;
  dateSortie: string | null;
  imgPath: string;
  tagIds: string[];
  platformIds: string[];
};

type TagDto = { id: string; nom: string };
type PlatformDto = { id: string; nom: string };

export function useGameForm(gameId: string) {
  const [game, setGame] = useState<EditableGameDto>({
    titre: "",
    editeur: "",
    description: "",
    dateSortie: null,
    imgPath: "",
    tagIds: [],
    platformIds: [],
  });

  const [tags, setTags] = useState<TagDto[]>([]);
  const [platforms, setPlatforms] = useState<PlatformDto[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const { showToast } = useToast();

  useEffect(() => {
    const load = async () => {
      try {
        setIsLoading(true);

        const [loadedTags, loadedPlatforms] = await Promise.all([
          fetchData<TagDto[]>("/api/admin/tags"),
          fetchData<PlatformDto[]>("/api/admin/platforms"),
        ]);

        setTags(loadedTags ?? []);
        setPlatforms(loadedPlatforms ?? []);

        if (gameId !== "new") {
          const gameRes = await fetchData<EditableGameDto>(
            `/api/admin/games/${gameId}`
          );
          if (gameRes) {
            setGame(gameRes);
          }
        }
      } catch (err) {
        console.error("Erreur useGameForm:", err);
        showToast("Impossible de charger les données du jeu.", "error");
      } finally {
        setIsLoading(false);
      }
    };
    load();
  }, [gameId, showToast]);

  const saveGame = async () => {
    try {
      const method = gameId === "new" ? "POST" : "PUT";
      const url =
        gameId === "new" ? "/api/admin/games" : `/api/admin/games/${gameId}`;

      await fetch(url, {
        method,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(game),
      });
      showToast("Jeu sauvegardé avec succès.", "success");
    } catch (err) {
      console.error("Erreur saveGame:", err);
      showToast("Erreur lors de la sauvegarde.", "error");
    }
  };

  return { game, setGame, tags, platforms, saveGame, isLoading };
}
