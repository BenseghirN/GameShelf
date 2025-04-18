import { useToast } from "@/hooks/useToas";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  addTag,
  loadAllTags,
  selectTagById,
  updateTag,
} from "@/store/slices/admin/tagSlice";
import {
  Typography,
  Alert,
  TextField,
  Button,
  CircularProgress,
  Snackbar,
} from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

export default function AdminTagFormPage() {
  const { id } = useParams();
  const isCreate = id === "new";

  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { toast, showToast, closeToast } = useToast();

  const existingTag = useAppSelector(selectTagById(id!));
  const [nom, setNom] = useState("");
  const [error, setError] = useState<string | null>(null);
  const [saving, setSaving] = useState(false);
  const [loading, setLoading] = useState(!isCreate);

  useEffect(() => {
    if (!isCreate) {
      dispatch(loadAllTags());
    }
  }, [dispatch, isCreate]);

  useEffect(() => {
    if (!isCreate && existingTag) {
      setNom(existingTag.nom);
      setLoading(false);
    }
  }, [isCreate, existingTag]);

  const handleSave = async () => {
    if (!nom.trim()) {
      setError("Le nom du tag est requis.");
      return;
    }

    setError(null);
    setSaving(true);

    try {
      if (isCreate) {
        await dispatch(addTag({ nom })).unwrap();
        showToast("Tag créé avec succès", "success");
      } else {
        await dispatch(updateTag({ id: id!, nom })).unwrap();
        showToast("Tag mis à jour avec succès", "success");
      }
      setTimeout(() => navigate("/admin/tags"), 1000);
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
        {isCreate ? "Créer un nouveau tag" : "Modifier le tag"}
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

      <TextField
        fullWidth
        label="Nom du tag"
        value={nom}
        onChange={(e) => setNom(e.target.value)}
        disabled={saving}
        sx={{ mb: 3 }}
      />

      <Box display="flex" justifyContent="flex-end" gap={2}>
        <Button
          variant="outlined"
          onClick={() => navigate("/admin/tags")}
          disabled={saving}
        >
          Annuler
        </Button>
        <Button variant="contained" onClick={handleSave} disabled={saving}>
          Sauvegarder
        </Button>
      </Box>
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
