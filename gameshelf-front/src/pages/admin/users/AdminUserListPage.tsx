import { useToast } from "@/hooks/useToas";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  Alert,
  Box,
  CircularProgress,
  FormControlLabel,
  Snackbar,
  Switch,
  TextField,
  Typography,
} from "@mui/material";
import { DataGrid, GridColDef } from "@mui/x-data-grid";
import { useEffect, useState } from "react";
import {
  demoteUser,
  loadAllUsers,
  promoteUser,
  selectUsers,
} from "@/store/slices/admin/userSlice";

export default function AdminUserListPage() {
  const dispatch = useAppDispatch();
  const users = useAppSelector(selectUsers);
  const { loading, error } = useAppSelector((state) => state.adminUsers);
  const { toast, showToast, closeToast } = useToast();

  useEffect(() => {
    dispatch(loadAllUsers());
  }, [dispatch]);

  const handleToggleRole = async (userId: string, isAdmin: boolean) => {
    try {
      if (isAdmin) {
        await dispatch(demoteUser(userId)).unwrap();
        showToast("Utilisateur rétrogradé avec succès", "success");
      } else {
        await dispatch(promoteUser(userId)).unwrap();
        showToast("Utilisateur promu avec succès", "success");
      }
    } catch {
      showToast("Erreur lors de la mise à jour du rôle", "error");
    }
  };

  // Mise en place de la disposition des colonnes du Grid
  const columns: GridColDef[] = [
    { field: "pseudo", headerName: "Pseudo", flex: 1 },
    { field: "email", headerName: "Email", flex: 1 },
    { field: "givenName", headerName: "Prénom", flex: 1 },
    { field: "surname", headerName: "Nom", flex: 1 },
    {
      field: "isAdmin",
      headerName: "Admin",
      width: 120,
      sortable: false,
      renderCell: (params) => (
        <FormControlLabel
          control={
            <Switch
              checked={params.row.isAdmin}
              onChange={() =>
                handleToggleRole(params.row.id, params.row.isAdmin)
              }
              color="primary"
            />
          }
          label=""
        />
      ),
    },
  ];

  const [paginationModel, setPaginationModel] = useState({
    page: 0,
    pageSize: 10,
  });

  const [search, setSearch] = useState("");

  const filteredUsers = users.filter((user) =>
    user.pseudo.toLowerCase().includes(search.toLowerCase())
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
      <Typography variant="h5" fontWeight="bold" mb={3}>
        Gestion des utilisateurs
      </Typography>

      {loading && (
        <Box display="flex" justifyContent="center" mt={4}>
          <CircularProgress />
        </Box>
      )}

      {error && (
        <Box display="flex" justifyContent="center" mt={3}>
          <Alert severity="error">{error}</Alert>
        </Box>
      )}

      {!loading && users.length > 0 && (
        <Box sx={{ height: 650, width: "100%" }}>
          <TextField
            label="Rechercher un user"
            variant="outlined"
            size="small"
            fullWidth
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            sx={{ mb: 2 }}
          />
          <DataGrid
            rows={filteredUsers}
            columns={columns}
            getRowId={(row) => row.id}
            paginationModel={paginationModel}
            onPaginationModelChange={setPaginationModel}
            pageSizeOptions={[10]}
            disableRowSelectionOnClick
            localeText={{
              noRowsLabel: "Aucun user à afficher",
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
