import GameCard from "@/components/GameCard";
import GameDetailsModal from "@/components/modals/GameDetails/GameDetailsModal";
import { useToast } from "@/hooks/useToas";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import {
  deleteUserGame,
  loadUserLibrary,
  selectUserGames,
  updateUserGameLocally,
} from "@/store/slices/librarySlice";
import { UserGame } from "@/types/UserGame";
import { Typography, CircularProgress, Alert, Snackbar } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";

export default function LibraryPage() {
  const dispatch = useAppDispatch();
  const userGames = useAppSelector(selectUserGames);
  const { loading, error } = useAppSelector((state) => state.library);

  const [selectedUserGame, setSelectedUserGame] = useState<UserGame | null>(
    null
  );
  const [isModalOpen, setModalOpen] = useState(false);
  const { toast, showToast, closeToast } = useToast();

  useEffect(() => {
    dispatch(loadUserLibrary());
  }, [dispatch]);

  const handleOpenModal = (userGame: UserGame) => {
    setSelectedUserGame(userGame);
    setModalOpen(true);
  };

  const handleCloseModal = () => {
    setModalOpen(false);
    setSelectedUserGame(null);
  };

  const handleRemoveFromLibrary = async () => {
    if (!selectedUserGame) return;

    try {
      await dispatch(deleteUserGame(selectedUserGame.gameId)).unwrap();
      showToast("Jeu supprimé de votre bibliothèque", "success");
      handleCloseModal();
    } catch (error) {
      showToast("Erreur lors de la suppression", "error");
      void error;
    }
  };

  return (
    <Box px={2} py={4}>
      <Typography variant="h4" gutterBottom>
        Ma Bibliothèque
      </Typography>

      {loading && <CircularProgress />}
      {error && <Alert severity="error">{error}</Alert>}

      {!loading && !error && userGames.length === 0 && (
        <Typography>Aucun jeu dans votre bibliothèque.</Typography>
      )}

      {!loading && !error && userGames.length > 0 && (
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
          {userGames.map((ug) =>
            ug.game ? (
              <Box key={ug.gameId}>
                <GameCard game={ug.game} onClick={() => handleOpenModal(ug)} />
              </Box>
            ) : null
          )}
        </Box>
      )}

      {selectedUserGame?.game && (
        <GameDetailsModal
          open={isModalOpen}
          onClose={handleCloseModal}
          game={selectedUserGame.game}
          userGame={selectedUserGame}
          onAddToLibrary={() => {}} // inutile ici, déjà ajouté
          onRemoveFromLibrary={handleRemoveFromLibrary}
          loading={false}
          error={null}
          onSave={(updatedUserGame) => {
            dispatch(updateUserGameLocally(updatedUserGame));
            setSelectedUserGame({
              ...updatedUserGame,
              game: selectedUserGame?.game ?? null,
            });
          }}
        />
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
