import { useState, useEffect } from "react";
import {
  Container,
  Typography,
  TextField,
  MenuItem,
  Button,
  Snackbar,
  Alert,
  Box,
  Chip,
  Stack,
} from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import { Platform } from "@/types/Platform";
import { NewProposalDto } from "@/types/NewProposalDto";
import { fetchData } from "@/utils/fetchData";
import { UserProposal } from "@/types/UserProposal";
import { useToast } from "@/hooks/useToas";

const proposalStatusLabels: Record<string, string> = {
  EnAttente: "En attente",
  Validee: "Validée",
  Refusee: "Refusée",
};

const proposalStatusColors: Record<
  string,
  "default" | "info" | "success" | "error"
> = {
  EnAttente: "info",
  Validee: "success",
  Refusee: "error",
};

export default function NewProposalFormPage() {
  const { id } = useParams();
  const isEdit = Boolean(id);
  const navigate = useNavigate();

  const [proposal, setProposal] = useState<UserProposal | null>(null);
  const [titre, setTitre] = useState("");
  const [platformId, setPlatformId] = useState("");
  const [platforms, setPlatforms] = useState<Platform[]>([]);
  const { toast, showToast, closeToast } = useToast();

  useEffect(() => {
    const fetchPlatforms = async () => {
      const result = await fetchData<Platform[]>(
        `${import.meta.env.VITE_API_BASE_URL}/api/v1/Platforms`
      );
      if (result) setPlatforms(result);
    };

    // Si en mode édition, pré-remplit les champs
    const fetchProposal = async () => {
      if (!id) return;
      const proposal = await fetchData<UserProposal>(
        `${import.meta.env.VITE_API_BASE_URL}/api/v1/UserProposals/${id}`
      );
      if (proposal) {
        setTitre(proposal.titre);
        setPlatformId(proposal.platformId);
        setProposal(proposal);
      }
    };

    fetchPlatforms();
    if (isEdit) fetchProposal();
  }, [id, isEdit]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const dto: NewProposalDto = {
      titre,
      platformId,
      dateSoumission: new Date().toISOString(),
    };

    const url = isEdit
      ? `${import.meta.env.VITE_API_BASE_URL}/api/v1/UserProposals/${id}`
      : `${import.meta.env.VITE_API_BASE_URL}/api/v1/UserProposals`;

    const method = isEdit ? "PUT" : "POST";
    const result = await fetchData(url, method, dto);

    if (result) {
      const message = isEdit
        ? "Proposition mise à jour"
        : "Proposition envoyée";
      showToast(message, "success");
      setTimeout(() => navigate("/Home"), 1000);
    } else {
      showToast("Erreur lors de l'envoi", "error");
    }
  };

  return (
    <Container maxWidth="sm">
      <Typography variant="h4" gutterBottom>
        {isEdit ? "Modifier une proposition" : "Proposer un nouveau jeu"}
      </Typography>

      <form onSubmit={handleSubmit}>
        <TextField
          label="Titre du jeu"
          fullWidth
          value={titre}
          onChange={(e) => setTitre(e.target.value)}
          margin="normal"
          required
          disabled={
            proposal?.statut === "Validee" || proposal?.statut === "Refusee"
          }
        />

        <TextField
          label="Plateforme"
          select
          fullWidth
          value={platformId}
          onChange={(e) => setPlatformId(e.target.value)}
          margin="normal"
          required
          disabled={
            proposal?.statut === "Validee" || proposal?.statut === "Refusee"
          }
        >
          {platforms.map((platform) => (
            <MenuItem key={platform.id} value={platform.id}>
              {platform.nom}
            </MenuItem>
          ))}
        </TextField>

        {isEdit && (
          <Box mt={4}>
            <Typography variant="subtitle2" gutterBottom>
              Informations de la proposition :
            </Typography>

            <Stack direction="row" spacing={2} alignItems="center">
              <Chip
                label={proposalStatusLabels[proposal?.statut || "EnAttente"]}
                color={proposalStatusColors[proposal?.statut || "EnAttente"]}
                sx={{ fontWeight: "bold" }}
              />
              <Typography variant="body2" color="text.secondary">
                Soumis le :{" "}
                {proposal?.dateSoumission
                  ? new Date(proposal.dateSoumission).toLocaleDateString()
                  : "—"}
              </Typography>
            </Stack>
          </Box>
        )}

        <Button
          type="submit"
          variant="contained"
          color="primary"
          sx={{ mt: 2 }}
          disabled={
            proposal?.statut === "Validee" || proposal?.statut === "Refusee"
          }
        >
          {isEdit ? "Mettre à jour" : "Envoyer la proposition"}
        </Button>
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
    </Container>
  );
}
