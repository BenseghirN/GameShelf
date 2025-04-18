import { useToast } from "@/hooks/useToas";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  addPlatform,
  loadAllPlatforms,
  selectPlatformById,
  updatePlatform,
} from "@/store/slices/admin/platformSlice";
import {
  Alert,
  Button,
  CircularProgress,
  Snackbar,
  TextField,
  Typography,
} from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

export default function AdminPlatformFormPage() {
  const { id } = useParams();
  const isCreate = id === "new";

  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { toast, showToast, closeToast } = useToast();

  const existingPlatform = useAppSelector(selectPlatformById(id!));
  const [nom, setNom] = useState("");
  const [imagePath, setImagePath] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [saving, setSaving] = useState(false);
  const [loading, setLoading] = useState(!isCreate);

  useEffect(() => {
    if (!isCreate) {
      dispatch(loadAllPlatforms());
    }
  }, [dispatch, isCreate]);

  useEffect(() => {
    if (!isCreate && existingPlatform) {
      setNom(existingPlatform.nom);
      setImagePath(existingPlatform.imagePath || "");
      setLoading(false);
    }
  }, [isCreate, existingPlatform]);

  const handleSave = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!nom.trim()) {
      setError("Le nom de la plateforme est requis.");
      return;
    }

    setError(null);
    setSaving(true);

    try {
      if (isCreate) {
        await dispatch(
          addPlatform({ nom: nom, imagePath: imagePath.trim() || null })
        ).unwrap();
        showToast("Plateforme créée avec succès", "success");
      } else {
        await dispatch(updatePlatform({ id: id!, nom, imagePath })).unwrap();
        showToast("Plateforme mise à jour avec succès", "success");
      }
      setTimeout(() => navigate("/admin/platforms"), 1000);
    } catch (err) {
      setError("Erreur lors de la sauvegarde.");
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
        maxWidth: "600px",
        mx: "auto",
      }}
    >
      <Typography variant="h5" fontWeight="bold" mb={3}>
        {isCreate ? "Créer une nouvelle plateforme" : "Modifier la plateforme"}
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
        <TextField
          fullWidth
          label="Nom de la plateforme"
          value={nom}
          onChange={(e) => setNom(e.target.value)}
          disabled={saving}
          sx={{ mb: 3 }}
          required
        />

        <TextField
          fullWidth
          label="Lien de l'image"
          value={imagePath}
          onChange={(e) => setImagePath(e.target.value)}
          disabled={saving}
          sx={{ mb: 3 }}
        />
        <Box display="flex" justifyContent="flex-end" gap={2}>
          <Button
            variant="outlined"
            onClick={() => navigate("/admin/platforms")}
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
