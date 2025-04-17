import { Game } from "./Game";

export interface UserGame {
  userId: string;
  gameId: string;
  game: Game | null;
  gameName: string;
  statut: string;
  note: number;
  imagePersoPath: string;
  dateAjout: string;
}
