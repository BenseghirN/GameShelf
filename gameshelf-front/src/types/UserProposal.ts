export interface UserProposal {
  id: string;
  userId: string;
  titre: string;
  platformId: string;
  imagePath: string;
  dateSoumission: string;
  statut: "EnAttente" | "Validee" | "Refusee";
}
