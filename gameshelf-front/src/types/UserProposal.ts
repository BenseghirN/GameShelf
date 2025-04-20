import { Platform } from "./Platform";

export interface UserProposal {
  id: string;
  userId: string;
  titre: string;
  platformId: string;
  platform: Platform;
  imagePath: string;
  dateSoumission: string;
  statut: "EnAttente" | "Validee" | "Refusee";
}
