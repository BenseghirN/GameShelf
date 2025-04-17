export interface AddToLibraryDto {
  statut: "Possede" | "EnCours" | "Termine" | null;
  note: number | null;
  imagePersoPath: string | null;
}
