import {
  Dialog,
  DialogTitle,
  IconButton,
  DialogContent,
  Typography,
  Chip,
  DialogActions,
  Button,
} from "@mui/material";
import { Box, Stack } from "@mui/system";
import CloseIcon from "@mui/icons-material/Close";
import StarIcon from "@mui/icons-material/Star";
import { getPlatformFullImageUrl } from "@/utils/imageUtils";
import { GameDetailsModalProps } from "./GameDetailsModalProps";

const GameDetailsModal: React.FC<GameDetailsModalProps> = ({
  open,
  onClose,
  onAddToLibrary,
  onRemoveFromLibrary,
  game,
  userGame,
}) => {
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
              src={game.imagePath}
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
              <Box>
                <Chip
                  label={`Statut : ${userGame.statut}`}
                  color="success"
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
              </Box>
            )}
          </Box>
        </Box>
      </DialogContent>

      <DialogActions>
        {userGame ? (
          <Button
            variant="outlined"
            color="error"
            onClick={onRemoveFromLibrary}
          >
            Supprimer de ma bibliothèque
          </Button>
        ) : (
          <Button variant="contained" onClick={onAddToLibrary}>
            Ajouter à ma bibliothèque
          </Button>
        )}
      </DialogActions>
    </Dialog>
  );
};

export default GameDetailsModal;
