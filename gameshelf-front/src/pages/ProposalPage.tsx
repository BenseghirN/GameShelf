import { UserProposal } from "@/types/UserProposal";
import { fetchData } from "@/utils/fetchData";
import { Typography, CircularProgress, Alert, Chip } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

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

export default function MyProposalPage() {
  const [proposals, setProposals] = useState<UserProposal[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const loadProposals = async () => {
      try {
        const result = await fetchData<UserProposal[]>(
          `${import.meta.env.VITE_API_BASE_URL}/api/v1/UserProposals/mine`
        );
        if (result) setProposals(result);
      } catch (error) {
        setError("Erreur lors du chargement des propositions.");
        void error;
      } finally {
        setLoading(false);
      }
    };

    loadProposals();
  }, []);

  return (
    <Box
      px={2}
      py={4}
      sx={{
        minHeight: "calc(100vh - 64px)",
        maxWidth: "1200px",
        mx: "auto",
      }}
    >
      <Typography
        variant="h5"
        fontWeight="bold"
        mb={3}
        sx={{ textAlign: "center" }}
      >
        Mes propositions
      </Typography>

      {loading && (
        <Box display="flex" justifyContent="center" mt={4}>
          <CircularProgress />
        </Box>
      )}

      {error && (
        <Box display="flex" justifyContent="center" mt={3}>
          <Alert
            severity="error"
            sx={{ maxWidth: 400, mx: "auto", display: "block" }}
          >
            {error}
          </Alert>
        </Box>
      )}

      {!loading && !error && proposals.length === 0 && (
        <Box display="flex" justifyContent="center" mt={3}>
          <Alert
            severity="warning"
            sx={{
              maxWidth: 400,
              width: "100%",
              textAlign: "center",
            }}
          >
            Aucune proposition trouvée.
          </Alert>
        </Box>
      )}

      {!loading && !error && proposals.length > 0 && (
        <Box
          display="grid"
          gridTemplateColumns={{
            xs: "repeat(1, 1fr)",
            sm: "repeat(2, 1fr)",
            md: "repeat(3, 1fr)",
            lg: "repeat(4, 1fr)",
          }}
          gap={2}
        >
          {proposals.map((proposal) => (
            <Box
              key={proposal.id}
              p={2}
              borderRadius={2}
              boxShadow={2}
              bgcolor="#fff"
              display="flex"
              justifyContent="space-between"
              alignItems="center"
              sx={{
                cursor: "pointer",
                "&:hover": { boxShadow: 4, backgroundColor: "#f9f9f9" },
              }}
              onClick={() => navigate(`/proposal/${proposal.id}`)}
            >
              <Box>
                <Typography fontWeight={600}>{proposal.titre}</Typography>
                <Typography variant="body2" color="text.secondary">
                  Soumise le :{" "}
                  {new Date(proposal.dateSoumission).toLocaleDateString()}
                </Typography>
              </Box>

              <Chip
                label={proposalStatusLabels[proposal.statut]}
                color={proposalStatusColors[proposal.statut]}
                variant="outlined"
              />
            </Box>
          ))}
        </Box>
      )}
    </Box>
  );
}
