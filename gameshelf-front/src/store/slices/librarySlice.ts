import { UserGame } from "@/types/UserGame";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { RootState } from "../store";

interface UserGameState {
  userGames: UserGame[];
  loading: boolean;
  error: string | null;
}

const initialState: UserGameState = {
  userGames: [],
  loading: false,
  error: null,
};

export const loadUserLibrary = createAsyncThunk<
  UserGame[],
  void,
  { rejectValue: string }
>("userGames/loadUserLibrary", async (_, { rejectWithValue }) => {
  const response = await fetchData<UserGame[]>(
    `${import.meta.env.VITE_API_BASE_URL}/api/v1/Library`
  );
  if (!response)
    return rejectWithValue("Impossible de charger la bibliothèque.");
  return response;
});

export const deleteUserGame = createAsyncThunk<
  string, // L'ID du jeu supprimé
  string, // L'ID du jeu à supprimer
  { rejectValue: string }
>("userGames/deleteUserGame", async (userGameId, { rejectWithValue }) => {
  const url = `${
    import.meta.env.VITE_API_BASE_URL
  }/api/v1/Library/${userGameId}`;
  const result = await fetchData(url, "DELETE");

  if (result === null) {
    return userGameId; // On retourne l'ID pour la suppression locale
  }

  return rejectWithValue("Échec de la suppression.");
});

const userGameSlice = createSlice({
  name: "userGames",
  initialState,
  reducers: {
    resetUserLibrary: (state) => {
      state.userGames = [];
      state.loading = false;
      state.error = null;
    },
    removeUserGameLocally: (state, action) => {
      state.userGames = state.userGames.filter(
        (ug) => ug.gameId !== action.payload
      );
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loadUserLibrary.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadUserLibrary.fulfilled, (state, action) => {
        state.userGames = action.payload;
        state.loading = false;
      })
      .addCase(loadUserLibrary.rejected, (state, action) => {
        state.error = action.error.message || "Erreur inconnue.";
        state.loading = false;
      })
      .addCase(deleteUserGame.fulfilled, (state, action) => {
        state.userGames = state.userGames.filter(
          (ug) => ug.gameId !== action.payload
        );
      })
      .addCase(deleteUserGame.rejected, (state, action) => {
        state.error = action.payload || "Erreur lors de la suppression.";
      });
  },
});

// Selector pour récupérer tous les userGames
export const selectUserGames = (state: RootState) => state.library.userGames;

// Selector pour récupérer un UserGame par gameId
export const selectUserGameByGameId = (gameId: string) => (state: RootState) =>
  state.library.userGames.find((ug) => ug.gameId === gameId);

export const { resetUserLibrary, removeUserGameLocally } =
  userGameSlice.actions;
export default userGameSlice.reducer;
