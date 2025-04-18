import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  Typography,
  Button,
  CircularProgress,
  Alert,
  IconButton,
  TextField,
  Snackbar,
} from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { useToast } from "@/hooks/useToas";
import {
  deleteGame,
  loadAllAdminGames,
  selectGames,
} from "@/store/slices/admin/gameSlice";
import { Tag } from "@/types/Tag";
import { Platform } from "@/types/Platform";

export default function AdminGameListPage() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const games = useAppSelector(selectGames);
  const { loading, error } = useAppSelector((state) => state.adminGames);
  const { toast, showToast, closeToast } = useToast();

  useEffect(() => {
    dispatch(loadAllAdminGames());
  }, [dispatch]);

  const handleEdit = (id: string) => {
    navigate(`/admin/games/${id}`);
  };

  const handleDelete = async (id: string) => {
    try {
      if (confirm("Voulez-vous vraiment supprimer ce jeu ?")) {
        await dispatch(deleteGame(id)).unwrap();
        showToast("Jeu supprimé avec succès", "success");
      }
    } catch (err) {
      showToast("Erreur lors de la suppression", "error");
      void err;
    }
  };

  // Mise en place de la disposition des colonnes du Grid
  const columns: GridColDef[] = [
    { field: "titre", headerName: "Titre", flex: 1 },
    { field: "editeur", headerName: "Éditeur", flex: 1 },
    {
      field: "dateSortie",
      headerName: "Date de sortie",
      flex: 1,
      renderCell: ({ row }) =>
        row.dateSortie
          ? new Date(row.dateSortie).toLocaleDateString("fr-FR")
          : "-",
    },
    {
      field: "tags",
      headerName: "Tags",
      flex: 1,
      renderCell: ({ row }) => row.tags.map((t: Tag) => t.nom).join(", "),
    },
    {
      field: "platforms",
      headerName: "Plateformes",
      flex: 1,
      renderCell: ({ row }) =>
        row.platforms.map((t: Platform) => t.nom).join(", "),
    },
    {
      field: "actions",
      headerName: "Actions",
      sortable: false,
      width: 120,
      renderCell: (params) => (
        <>
          <IconButton onClick={() => handleEdit(params.row.id)}>
            <EditIcon color="primary" />
          </IconButton>
          <IconButton onClick={() => handleDelete(params.row.id)}>
            <DeleteIcon color="error" />
          </IconButton>
        </>
      ),
    },
  ];

  const [paginationModel, setPaginationModel] = useState({
    page: 0,
    pageSize: 10,
  });

  const [search, setSearch] = useState("");

  const filteredGames = games.filter((game) =>
    game.titre.toLowerCase().includes(search.toLowerCase())
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
        width="100%"
        display="flex"
        justifyContent="space-between"
        alignItems="center"
        mb={3}
      >
        <Typography variant="h5" fontWeight="bold">
          Gestion des jeux
        </Typography>

        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={() => navigate("/admin/games/new")}
        >
          Nouveau jeu
        </Button>
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

      {!loading && !error && games.length === 0 && (
        <Box display="flex" justifyContent="center" mt={3}>
          <Alert
            severity="warning"
            sx={{
              maxWidth: 400,
              width: "100%",
              textAlign: "center",
            }}
          >
            Aucun Jeu disponible.
          </Alert>
        </Box>
      )}

      {!loading && games.length > 0 && (
        <Box sx={{ height: 650, width: "100%" }}>
          <TextField
            label="Rechercher un jeu"
            variant="outlined"
            size="small"
            fullWidth
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            sx={{ mb: 2 }}
          />
          <DataGrid
            rows={filteredGames}
            columns={columns}
            getRowId={(row) => row.id}
            paginationModel={paginationModel}
            onPaginationModelChange={setPaginationModel}
            pageSizeOptions={[10]}
            disableRowSelectionOnClick
            localeText={{
              noRowsLabel: "Aucun jeu à afficher",
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
