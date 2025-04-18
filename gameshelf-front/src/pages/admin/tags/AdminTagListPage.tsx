import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  deleteTag,
  loadAllTags,
  selectTags,
} from "@/store/slices/admin/tagSlice";
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
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { useToast } from "@/hooks/useToas";

export default function AdminTagsListPage() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const tags = useAppSelector(selectTags);
  const { loading, error } = useAppSelector((state) => state.tags);
  const { toast, showToast, closeToast } = useToast();

  useEffect(() => {
    dispatch(loadAllTags());
  }, [dispatch]);

  const handleEdit = (id: string) => {
    navigate(`/admin/tags/${id}`);
  };

  const handleDelete = async (id: string) => {
    try {
      if (confirm("Voulez-vous vraiment supprimer ce tag ?")) {
        await dispatch(deleteTag(id)).unwrap();
        showToast("Tag supprimé avec succès", "success");
      }
    } catch (err) {
      showToast("Erreur lors de la suppression", "error");
      void err;
    }
  };

  // Mise en place de la disposition des colonnes du Grid
  const columns: GridColDef[] = [
    {
      field: "nom",
      headerName: "Nom du tag",
      flex: 1,
    },
    {
      field: "actions",
      headerName: "Actions",
      sortable: false,
      align: "right",
      headerAlign: "right",
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

  const filteredTags = tags.filter((tag) =>
    tag.nom.toLowerCase().includes(search.toLowerCase())
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
          Gestion des tags
        </Typography>

        <Button
          variant="contained"
          color="primary"
          onClick={() => navigate("/admin/tags/new")}
        >
          Créer un tag
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

      {!loading && !error && tags.length === 0 && (
        <Box display="flex" justifyContent="center" mt={3}>
          <Alert
            severity="warning"
            sx={{
              maxWidth: 400,
              width: "100%",
              textAlign: "center",
            }}
          >
            Aucun Tag disponible.
          </Alert>
        </Box>
      )}

      {!loading && tags.length > 0 && (
        <Box sx={{ height: 650, width: "100%" }}>
          <TextField
            label="Rechercher un tag"
            variant="outlined"
            size="small"
            fullWidth
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            sx={{ mb: 2 }}
          />
          <DataGrid
            rows={filteredTags}
            columns={columns}
            getRowId={(row) => row.id}
            paginationModel={paginationModel}
            onPaginationModelChange={setPaginationModel}
            pageSizeOptions={[10]}
            disableRowSelectionOnClick
            localeText={{
              noRowsLabel: "Aucun tag à afficher",
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
