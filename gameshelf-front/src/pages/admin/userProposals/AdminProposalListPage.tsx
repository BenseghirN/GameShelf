import { useToast } from "@/hooks/useToas";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  Alert,
  Box,
  Chip,
  CircularProgress,
  IconButton,
  Snackbar,
  TextField,
  Typography,
} from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import CheckIcon from "@mui/icons-material/Check";
import ClearIcon from "@mui/icons-material/Clear";
import {
  acceptProposal,
  loadAllProposals,
  rejectProposal,
  selectAdminProposals,
} from "@/store/slices/admin/proposalSlice";
import { UserProposal } from "@/types/UserProposal";

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

export default function AdminProposalListPage() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const proposals = useAppSelector(selectAdminProposals);
  const { loading, error } = useAppSelector((state) => state.adminProposals);
  const { toast, showToast, closeToast } = useToast();

  useEffect(() => {
    dispatch(loadAllProposals());
  }, [dispatch]);

  const handleAccept = async (id: string) => {
    try {
      const game = await dispatch(acceptProposal(id)).unwrap();
      showToast("Proposition acceptée, jeu créé avec succès", "success");
      navigate(`/admin/games/${game.id}`);
    } catch (err) {
      showToast("Erreur lors de l'acceptation", "error");
      void err;
    }
  };

  const handleReject = async (id: string) => {
    try {
      await dispatch(rejectProposal(id)).unwrap();
      showToast("Proposition refusée", "success");
    } catch (err) {
      showToast("Erreur lors du refus", "error");
      void err;
    }
  };

  // Mise en place de la disposition des colonnes du Grid
  const columns: GridColDef[] = [
    { field: "titre", headerName: "Titre", flex: 1 },
    {
      field: "platform",
      headerName: "Plateforme",
      flex: 1,
      renderCell: ({ row }) => row.platform?.nom ?? "N/A",
    },
    {
      field: "dateSoumission",
      headerName: "Soumis le",
      flex: 1,
      renderCell: ({ row }) =>
        row.dateSoumission
          ? new Date(row.dateSoumission).toLocaleDateString("fr-FR")
          : "-",
    },
    {
      field: "statut",
      headerName: "Statut",
      flex: 1,
      renderCell: (params) => (
        <Chip
          label={proposalStatusLabels[params.value]}
          color={proposalStatusColors[params.value]}
          size="small"
          variant="outlined"
        />
      ),
    },
    {
      field: "actions",
      headerName: "Actions",
      sortable: false,
      width: 120,
      renderCell: (params) => {
        const row: UserProposal = params.row;
        return row.statut === "EnAttente" ? (
          <>
            <IconButton onClick={() => handleAccept(row.id)}>
              <CheckIcon color="success" />
            </IconButton>
            <IconButton onClick={() => handleReject(row.id)}>
              <ClearIcon color="error" />
            </IconButton>
          </>
        ) : null;
      },
    },
  ];

  const [paginationModel, setPaginationModel] = useState({
    page: 0,
    pageSize: 10,
  });

  const [search, setSearch] = useState("");

  const filteredProposals = proposals.filter((proposal) =>
    proposal.titre.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <Box
      px={2}
      py={4}
      sx={{
        minHeight: "calc(100vh - 64px)",
        width: "100%",
        maxWidth: "1200px",
        mx: "auto",
      }}
    >
      <Box
        display="flex"
        justifyContent="space-between"
        alignItems="center"
        mb={3}
      >
        <Typography variant="h5" fontWeight="bold">
          Gestion des propositions des utilisateurs
        </Typography>
      </Box>

      {loading && (
        <Box display="flex" justifyContent="center" mt={4}>
          <CircularProgress />
        </Box>
      )}

      {error && (
        <Box display="flex" justifyContent="center" mt={3}>
          <Alert
            severity="error"
            sx={{
              maxWidth: 400,
              width: "100%",
              textAlign: "center",
            }}
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
            Aucune proposition à afficher.
          </Alert>
        </Box>
      )}

      {!loading && !error && proposals.length > 0 && (
        <Box sx={{ height: 650, width: "100%" }}>
          <TextField
            label="Rechercher une proposition (titre du jeu)"
            variant="outlined"
            size="small"
            fullWidth
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            sx={{ mb: 2 }}
          />
          <DataGrid
            rows={filteredProposals}
            columns={columns}
            getRowId={(row) => row.id}
            paginationModel={paginationModel}
            onPaginationModelChange={setPaginationModel}
            pageSizeOptions={[10]}
            disableRowSelectionOnClick
            localeText={{
              noRowsLabel: "Aucune proposition  à afficher",
            }}
          />
        </Box>
      )}
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
