import { Game } from "./Game";

export interface UserGame {
  userId: string;
  gameId: string;
  game: Game | null;
  gameName: string;
  statut: "Possede" | "EnCours" | "Termine";
  note: number;
  imagePersoPath: string;
  dateAjout: string;
}
