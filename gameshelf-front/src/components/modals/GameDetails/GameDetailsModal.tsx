import {
  Dialog,
  DialogTitle,
  IconButton,
  DialogContent,
  Typography,
  Chip,
  DialogActions,
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
  Alert,
  Snackbar,
} from "@mui/material";
import { Box, Stack } from "@mui/system";
import CloseIcon from "@mui/icons-material/Close";
import StarIcon from "@mui/icons-material/Star";
import {
  getGameFullImageUrl,
  getPlatformFullImageUrl,
} from "@/utils/imageUtils";
import { GameDetailsModalProps } from "./GameDetailsModalProps";
import { useState } from "react";
import { fetchData } from "@/utils/fetchData";
import { UpdateLibraryDto } from "@/types/UpdateLibraryDto";
import { UserGame } from "@/types/UserGame";
import EditIcon from "@mui/icons-material/Edit";
import { useToast } from "@/hooks/useToas";

const statutLabels: Record<string, string> = {
  Possede: "Possédé",
  EnCours: "En Cours",
  Termine: "Terminé",
};

const statutColors: Record<string, "default" | "warning" | "success"> = {
  Possede: "warning", // Jaune
  EnCours: "default", // Gris (ou modifiable avec sx)
  Termine: "success", // Vert
};

const GameDetailsModal: React.FC<GameDetailsModalProps> = ({
  open,
  onClose,
  onAddToLibrary,
  onRemoveFromLibrary,
  game,
  userGame,
  onSave,
}) => {
  const [isEditing, setIsEditing] = useState(false);
  const [statut, setStatut] = useState(userGame?.statut || "Possede");
  const [note, setNote] = useState(userGame?.note || 0);
  const [saving, setSaving] = useState(false);
  const { toast, showToast, closeToast } = useToast();

  const handleSave = async () => {
    if (!userGame) return;
    setSaving(true);
    try {
      const dto: UpdateLibraryDto = {
        statut,
        note,
      };
      const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Library/${
        game.id
      }`;
      const result: UserGame | null = await fetchData<UserGame>(
        url,
        "PUT",
        dto
      );
      if (result) {
        setStatut(result.statut as "Possede" | "EnCours" | "Termine");
        setNote(result.note ?? 0);
        showToast("Modifications enregistrées avec succès", "success");
        setIsEditing(false);
        if (onSave) onSave(result);
      } else {
        showToast("Erreur lors de la sauvegarde", "error");
      }
    } catch (error) {
      showToast("Erreur inattendue", "error");
      void error;
    } finally {
      setSaving(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="md" fullWidth>
      <DialogTitle>
        Détails du jeu
        <IconButton
          onClick={onClose}
          sx={{ position: "absolute", right: 8, top: 8 }}
        >
          <CloseIcon />
        </IconButton>
      </DialogTitle>
      <DialogContent>
        <Box display="flex" flexDirection={{ xs: "column", sm: "row" }} gap={3}>
          {/* Colonne image */}
          <Box flexShrink={0} width={{ xs: "100%", sm: "40%" }}>
            <Box
              component="img"
              src={getGameFullImageUrl(game.imagePath)}
              alt={game.titre}
              sx={{ width: "100%", borderRadius: 2 }}
            />
          </Box>

          {/* Colonne texte */}
          <Box flexGrow={1}>
            <Typography variant="h5" fontWeight={600} gutterBottom>
              {game.titre}
            </Typography>
            <Typography variant="body2" color="text.secondary" gutterBottom>
              {game.editeur} — {new Date(game.dateSortie).toLocaleDateString()}
            </Typography>
            <Typography variant="body1" paragraph>
              {game.description}
            </Typography>

            {/* Tags */}
            <Stack direction="row" spacing={1} mb={2} flexWrap="wrap">
              {game.tags.map((tag) => (
                <Chip key={tag.id} label={tag.nom} />
              ))}
            </Stack>

            {/* Plateformes */}
            <Stack direction="row" spacing={1} mb={2}>
              {game.platforms.map((p) => (
                <Box
                  key={p.id}
                  component="img"
                  src={getPlatformFullImageUrl(p.imagePath)}
                  alt={p.nom}
                  sx={{
                    height: 32,
                    width: 40,
                    objectFit: "contain",
                    borderRadius: 1,
                  }}
                />
              ))}
            </Stack>

            {userGame && (
              <Box mt={2}>
                {isEditing ? (
                  <>
                    <FormControl fullWidth margin="normal">
                      <InputLabel>Statut</InputLabel>
                      <Select
                        value={statut}
                        label="Statut"
                        onChange={(e) =>
                          setStatut(
                            e.target.value as "Possede" | "EnCours" | "Termine"
                          )
                        }
                      >
                        <MenuItem value="Possede">Possédé</MenuItem>
                        <MenuItem value="EnCours">En cours</MenuItem>
                        <MenuItem value="Termine">Terminé</MenuItem>
                      </Select>
                    </FormControl>
                    <TextField
                      fullWidth
                      type="number"
                      label="Note (sur 5)"
                      value={note}
                      onChange={(e) => setNote(Number(e.target.value))}
                      inputProps={{ min: 0, max: 5 }}
                    />
                  </>
                ) : (
                  <>
                    <Chip
                      label={statutLabels[userGame.statut] ?? userGame.statut}
                      color={statutColors[userGame.statut] ?? "default"}
                      sx={{ mb: 1 }}
                    />

                    <Typography>
                      <StarIcon
                        fontSize="small"
                        sx={{ verticalAlign: "middle", mr: 0.5 }}
                      />
                      Note : {userGame.note}/5
                    </Typography>
                    <Typography variant="caption" color="text.secondary">
                      Ajouté le :{" "}
                      {new Date(userGame.dateAjout).toLocaleDateString()}
                    </Typography>
                  </>
                )}
              </Box>
            )}
          </Box>
        </Box>
      </DialogContent>

      <DialogActions>
        {userGame && !isEditing && (
          <>
            <IconButton
              onClick={() => setIsEditing(true)}
              color="primary"
              aria-label="Modifier"
            >
              <EditIcon />
            </IconButton>
            <Button
              variant="outlined"
              color="error"
              onClick={onRemoveFromLibrary}
            >
              Supprimer de ma bibliothèque
            </Button>
          </>
        )}

        {!userGame && (
          <Button variant="contained" onClick={onAddToLibrary}>
            Ajouter à ma bibliothèque
          </Button>
        )}

        {isEditing && (
          <>
            <Button onClick={() => setIsEditing(false)} disabled={saving}>
              Annuler
            </Button>
            <Button variant="contained" onClick={handleSave} disabled={saving}>
              Sauvegarder
            </Button>
          </>
        )}
      </DialogActions>

      <Snackbar
        open={toast.open}
        autoHideDuration={3000}
        onClose={closeToast}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        <Alert
          severity={toast.severity}
          onClose={closeToast}
          sx={{ width: "100%" }}
        >
          {toast.message}
        </Alert>
      </Snackbar>
    </Dialog>
  );
};

export default GameDetailsModal;
