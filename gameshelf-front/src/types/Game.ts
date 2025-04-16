import { Platform } from "./Platform";
import { Tag } from "./Tag";

export interface Game {
  id: string;
  titre: string;
  description: string;
  dateSortie: string;
  editeur: string;
  imagePath: string;
  tags: Tag[];
  platforms: Platform[];
}
