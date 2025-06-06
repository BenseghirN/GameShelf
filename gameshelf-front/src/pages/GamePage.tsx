import { useEffect, useState } from "react";
import {
  Box,
  TextField,
  CircularProgress,
  Typography,
  Alert,
  Snackbar,
} from "@mui/material";
import { useAppDispatch, useAppSelector } from "@/store/hooks";
import { loadAllGames } from "@/store/slices/gameSlice";
import GameCard from "@/components/GameCard";
import { Game } from "@/types/Game";
import { useUserGame } from "@/hooks/useUserGame";
import { fetchData } from "@/utils/fetchData";
import GameDetailsModal from "@/components/modals/GameDetails/GameDetailsModal";
import { AddToLibraryDto } from "@/types/AddToLibraryDto";
import FancyButton from "@/components/controls/FancyButton";
import SendIcon from "@mui/icons-material/Send";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useToast } from "@/hooks/useToas";
import GameFilters from "@/components/GameFilters";

export default function GamesPage() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const { games, loading, error } = useAppSelector((state) => state.games);
  const [search, setSearch] = useState("");
  const [selectedGame, setSelectedGame] = useState<Game | null>(null);
  const [isModalOpen, setModalOpen] = useState(false);
  const { toast, showToast, closeToast } = useToast();
  const [searchParams] = useSearchParams();

  // useEffect(() => {
  //   dispatch(loadAllGames());
  // }, [dispatch]);

  const filteredGames =
    games.filter((game) =>
      game.titre.toLowerCase().includes(search.toLowerCase())
    ) || [];

  useEffect(() => {
    const genres = searchParams.getAll("genres");
    const platforms = searchParams.getAll("platforms");

    dispatch(loadAllGames({ genres, platforms }));
  }, [dispatch, searchParams]);

  const {
    userGame,
    loading: userGameLoading,
    error: userGameError,
  } = useUserGame(isModalOpen, selectedGame?.id);

  const handleOpenModal = (game: Game) => {
    setSelectedGame(game);
    setModalOpen(true);
  };

  const handleCloseModal = () => {
    setModalOpen(false);
    setSelectedGame(null);
  };

  const handleAddToLibrary = async () => {
    if (!selectedGame) return;

    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Library/${
      selectedGame.id
    }`;

    const dto: AddToLibraryDto = {
      statut: "Possede",
      note: null,
      imagePersoPath: null,
    };
    const result = await fetchData(url, "POST", dto);

    if (result) {
      showToast("Jeu ajouté à votre bibliothèque !", "success");
      refreshModal();
    } else {
      showToast("Échec de l'ajout", "error");
    }
  };

  const handleRemoveFromLibrary = async () => {
    if (!selectedGame) return;

    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Library/${
      selectedGame.id
    }`;
    const result = await fetchData(url, "DELETE");

    if (result !== null || result === null) {
      showToast("Jeu supprimé de votre bibliothèque !", "success");
      refreshModal();
    } else {
      showToast("Échec de la suppression", "error");
    }
  };

  const refreshModal = () => {
    setModalOpen(false);
    setTimeout(() => setModalOpen(true), 10);
  };

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
        px={4}
        mb={3}
      >
        <Typography variant="h5" fontWeight="bold">
          Tous les jeux
        </Typography>

        <FancyButton
          color="#7c3aed"
          icon={<SendIcon />}
          onClick={() => navigate("/proposal")}
        >
          Proposer un jeu
        </FancyButton>
      </Box>

      {/* Champ de recherche pour filtrer les jeux */}
      <Box maxWidth="900px" mx="auto" mb={4}>
        <TextField
          fullWidth
          label="Rechercher un jeu"
          variant="outlined"
          size="small"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          sx={{ mb: 3 }}
        />
        <GameFilters />
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
              alignItems: "center",
              whiteSpace: "nowrap",
            }}
          >
            {error}
          </Alert>
        </Box>
      )}

      {!loading && !error && (
        <>
          {filteredGames.length === 0 ? (
            <Box display="flex" justifyContent="center" mt={3}>
              <Alert
                severity="warning"
                sx={{
                  maxWidth: 400,
                  width: "100%",
                  alignItems: "center",
                  whiteSpace: "nowrap",
                }}
              >
                Aucun jeu trouvé.
              </Alert>
            </Box>
          ) : (
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
              {filteredGames.map((game) => (
                <Box key={game.id}>
                  <GameCard game={game} onClick={() => handleOpenModal(game)} />
                </Box>
              ))}
            </Box>
          )}
        </>
      )}

      {selectedGame && (
        <GameDetailsModal
          open={isModalOpen}
          onClose={handleCloseModal}
          game={selectedGame}
          userGame={userGame}
          loading={userGameLoading}
          error={userGameError}
          onAddToLibrary={handleAddToLibrary}
          onRemoveFromLibrary={handleRemoveFromLibrary}
          onSave={refreshModal}
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
