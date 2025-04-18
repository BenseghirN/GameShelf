import { useToast } from "@/hooks/useToas";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  addGame,
  loadAllAdminGames,
  selectGameById,
  updateGame,
} from "@/store/slices/admin/gameSlice";
import {
  loadAllPlatforms,
  selectPlatforms,
} from "@/store/slices/admin/platformSlice";
import { loadAllTags, selectTags } from "@/store/slices/admin/tagSlice";
import { NewGameDto } from "@/types/NewGameDto";
import { getGameFullImageUrl } from "@/utils/imageUtils";
import {
  Typography,
  Alert,
  TextField,
  Button,
  CircularProgress,
  Snackbar,
  Checkbox,
  FormControl,
  InputLabel,
  ListItemText,
  MenuItem,
  Select,
} from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

export default function AdminGameFormPage() {
  const { id } = useParams();
  const isCreate = id === "new";

  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { toast, showToast, closeToast } = useToast();

  const existingGame = useAppSelector(selectGameById(id!));
  const allTags = useAppSelector(selectTags);
  const allPlatforms = useAppSelector(selectPlatforms);
  const [formData, setFormData] = useState<NewGameDto>({
    titre: "",
    description: "",
    editeur: "",
    dateSortie: "",
    imagePath: "",
    tagIds: [],
    platformIds: [],
  });
  const [error, setError] = useState<string | null>(null);
  const [saving, setSaving] = useState(false);
  const [loading, setLoading] = useState(!isCreate);

  useEffect(() => {
    dispatch(loadAllTags());
    dispatch(loadAllPlatforms());
    if (!isCreate) {
      dispatch(loadAllAdminGames());
    }
  }, [dispatch, isCreate]);

  useEffect(() => {
    if (!isCreate && existingGame) {
      setFormData({
        titre: existingGame.titre,
        description: existingGame.description,
        editeur: existingGame.editeur,
        dateSortie: existingGame.dateSortie?.split("T")[0] || "",
        imagePath: existingGame.imagePath,
        tagIds: existingGame.tags.map((tag) => tag.id),
        platformIds: existingGame.platforms.map((platform) => platform.id),
      });
      setLoading(false);
    }
  }, [isCreate, existingGame]);

  const handleSave = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!formData.titre.trim()) {
      setError("Le titre du jeu est requis.");
      return;
    }

    setError(null);
    setSaving(true);

    try {
      if (isCreate) {
        await dispatch(addGame(formData)).unwrap();
        showToast("Jeu créé avec succès", "success");
      } else {
        await dispatch(updateGame({ id: id!, dto: formData })).unwrap();
        showToast("Jeu mis à jour avec succès", "success");
      }
      setTimeout(() => navigate("/admin/games"), 1000);
    } catch (err) {
      setError("Échec de la sauvegarde.");
      if (error) {
        showToast(error, "error");
      }
      void err;
    } finally {
      setSaving(false);
    }
  };

  if (!isCreate && loading) {
    return (
      <Box display="flex" justifyContent="center" mt={6}>
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box
      px={2}
      py={4}
      sx={{
        minHeight: "calc(100vh - 64px)",
        width: "100%",
        maxWidth: "900px",
        mx: "auto",
      }}
    >
      <Typography variant="h5" fontWeight="bold" mb={3}>
        {isCreate ? "Créer un nouveau jeu" : "Modifier le jeu"}
      </Typography>

      {error && (
        <Box display="flex" justifyContent="center" mt={3}>
          <Alert
            severity="error"
            sx={{
              maxWidth: 400,
              width: "100%",
              alignItems: "center",
              whiteSpace: "nowrap",
            }}
          >
            {error}
          </Alert>
        </Box>
      )}

      <form onSubmit={handleSave}>
        <Box display="flex" gap={4}>
          {/* Left column with image */}
          <Box flex={1}>
            <img
              src={getGameFullImageUrl(formData.imagePath)}
              alt="Jaquette du jeu"
              style={{
                width: "100%",
                height: "auto",
                borderRadius: "8px",
                objectFit: "cover",
                maxHeight: 300,
              }}
            />

            <TextField
              label="Image (lien relatif)"
              fullWidth
              value={formData.imagePath}
              onChange={(e) =>
                setFormData((prev) => ({ ...prev, imagePath: e.target.value }))
              }
            />
          </Box>

          {/* Right column with fields */}
          <Box flex={2} display="flex" flexDirection="column" gap={2}>
            <TextField
              label="Titre"
              fullWidth
              value={formData.titre}
              onChange={(e) =>
                setFormData((prev) => ({ ...prev, titre: e.target.value }))
              }
              required
            />

            <TextField
              label="Éditeur"
              fullWidth
              value={formData.editeur}
              onChange={(e) =>
                setFormData((prev) => ({ ...prev, editeur: e.target.value }))
              }
              required
            />

            <TextField
              label="Date de sortie"
              type="date"
              fullWidth
              value={formData.dateSortie}
              onChange={(e) =>
                setFormData((prev) => ({ ...prev, dateSortie: e.target.value }))
              }
            />
          </Box>
        </Box>

        <Box mt={3}>
          <TextField
            label="Description"
            fullWidth
            multiline
            minRows={4}
            value={formData.description}
            onChange={(e) =>
              setFormData((prev) => ({ ...prev, description: e.target.value }))
            }
          />
        </Box>

        <FormControl fullWidth sx={{ mt: 3, mb: 2 }}>
          <InputLabel id="select-tags">Tags</InputLabel>
          <Select
            labelId="select-tags"
            multiple
            value={formData.tagIds}
            onChange={(event) =>
              setFormData((prev) => ({
                ...prev,
                tagIds: event.target.value as string[],
              }))
            }
            renderValue={(selected) =>
              allTags
                .filter((tag) => selected.includes(tag.id))
                .map((t) => t.nom)
                .join(", ")
            }
          >
            {allTags.map((tag) => (
              <MenuItem key={tag.id} value={tag.id}>
                <Checkbox checked={formData.tagIds.includes(tag.id)} />
                <ListItemText primary={tag.nom} />
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <FormControl fullWidth sx={{ mt: 3, mb: 2 }}>
          <InputLabel id="select-platforms">Plateformes</InputLabel>
          <Select
            labelId="select-platforms"
            multiple
            value={formData.platformIds}
            onChange={(event) =>
              setFormData((prev) => ({
                ...prev,
                platformIds: event.target.value as string[],
              }))
            }
            renderValue={(selected) =>
              allPlatforms
                .filter((p) => selected.includes(p.id))
                .map((p) => p.nom)
                .join(", ")
            }
          >
            {allPlatforms.map((platform) => (
              <MenuItem key={platform.id} value={platform.id}>
                <Checkbox
                  checked={formData.platformIds.includes(platform.id)}
                />
                <ListItemText primary={platform.nom} />
              </MenuItem>
            ))}
          </Select>
        </FormControl>

        <Box mt={3} display="flex" gap={2}>
          <Button
            variant="outlined"
            onClick={() => navigate("/admin/games")}
            disabled={saving}
          >
            Annuler
          </Button>
          <Button type="submit" variant="contained" disabled={saving}>
            Sauvegarder
          </Button>
        </Box>
      </form>
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
    </Box>
  );
}
