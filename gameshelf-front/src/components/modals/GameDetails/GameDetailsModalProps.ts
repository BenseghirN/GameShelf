import { Game } from "../../../types/Game";
import { UserGame } from "../../../types/UserGame";

export type GameDetailsModalProps = {
  open: boolean;
  onClose: () => void;
  game: Game;
  userGame: UserGame | null;
  loading: boolean;
  error: string | null;
  onAddToLibrary: () => void;
  onRemoveFromLibrary: () => void;
};
