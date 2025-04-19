import { useToast } from "@/hooks/useToas";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  deletePlatform,
  loadAllPlatforms,
  selectPlatforms,
} from "@/store/slices/admin/platformSlice";
import {
  Alert,
  Box,
  Button,
  CircularProgress,
  IconButton,
  Snackbar,
  TextField,
  Typography,
} from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";
import AddIcon from "@mui/icons-material/Add";

export default function AdminPlatformListPage() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const platforms = useAppSelector(selectPlatforms);
  const { loading, error } = useAppSelector((state) => state.adminPlatforms);
  const { toast, showToast, closeToast } = useToast();

  useEffect(() => {
    dispatch(loadAllPlatforms());
  }, [dispatch]);

  const handleEdit = (id: string) => {
    navigate(`/admin/platforms/${id}`);
  };

  const handleDelete = async (id: string) => {
    try {
      if (confirm("Voulez-vous vraiment supprimer cette plateforme ?")) {
        await dispatch(deletePlatform(id)).unwrap();
        showToast("Plateforme supprimé avec succès", "success");
      }
    } catch (err) {
      showToast("Erreur lors de la suppression", "error");
      void err;
    }
  };

  // Mise en place de la disposition des colonnes du Grid
  const columns: GridColDef[] = [
    { field: "nom", headerName: "Nom", flex: 1 },
    { field: "imagePath", headerName: "Image Path", flex: 2 },
    {
      field: "actions",
      headerName: "Actions",
      sortable: false,
      filterable: false,
      align: "center",
      headerAlign: "center",
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

  const filteredPlatforms = platforms.filter((platform) =>
    platform.nom.toLowerCase().includes(search.toLowerCase())
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
          Gestion des plateformes
        </Typography>
        <Button
          variant="contained"
          startIcon={<AddIcon />}
          onClick={() => navigate("/admin/platforms/new")}
        >
          Nouvelle plateforme
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

      {!loading && !error && platforms.length === 0 && (
        <Box display="flex" justifyContent="center" mt={3}>
          <Alert
            severity="warning"
            sx={{
              maxWidth: 400,
              width: "100%",
              textAlign: "center",
            }}
          >
            Aucune Plateforme disponible.
          </Alert>
        </Box>
      )}

      {!loading && platforms.length > 0 && (
        <Box sx={{ height: 650, width: "100%" }}>
          <TextField
            label="Rechercher une plateforme"
            variant="outlined"
            size="small"
            fullWidth
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            sx={{ mb: 2 }}
          />
          <DataGrid
            rows={filteredPlatforms}
            columns={columns}
            getRowId={(row) => row.id}
            paginationModel={paginationModel}
            onPaginationModelChange={setPaginationModel}
            pageSizeOptions={[10]}
            disableRowSelectionOnClick
            localeText={{
              noRowsLabel: "Aucune plateforme à afficher",
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
