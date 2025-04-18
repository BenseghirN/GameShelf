import { RootState } from "@/store/store";
import { Game } from "@/types/Game";
import { NewGameDto } from "@/types/NewGameDto";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

interface AdminGameState {
  games: Game[];
  loading: boolean;
  error: string | null;
}

const initialState: AdminGameState = {
  games: [],
  loading: false,
  error: null,
};

// GET
export const loadAllAdminGames = createAsyncThunk<Game[]>(
  "adminGames/loadAll",
  async (_, { rejectWithValue }) => {
    const response = await fetchData<Game[]>(
      `${import.meta.env.VITE_API_BASE_URL}/api/v1/Games`
    );
    if (!response) return rejectWithValue("Erreur lors du chargement des jeux");
    return response;
  }
);

// POST
export const addGame = createAsyncThunk<Game, NewGameDto>(
  "adminGames/addGame",
  async (dto, { rejectWithValue }) => {
    const response = await fetchData<Game>(
      `${import.meta.env.VITE_API_BASE_URL}/api/v1/Games`,
      "POST",
      dto
    );
    if (!response) return rejectWithValue("Erreur lors de la création du jeu");
    return response;
  }
);

// PUT
export const updateGame = createAsyncThunk<
  Game,
  { id: string; dto: NewGameDto }
>("adminGames/updateGame", async ({ id, dto }, { rejectWithValue }) => {
  const response = await fetchData<Game>(
    `${import.meta.env.VITE_API_BASE_URL}/api/v1/Games/${id}`,
    "PUT",
    dto
  );
  if (!response) return rejectWithValue("Erreur lors de la mise à jour");
  return response;
});

// DELETE
export const deleteGame = createAsyncThunk<string, string>(
  "adminGames/deleteGame",
  async (id, { rejectWithValue }) => {
    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Games/${id}`;
    const response = await fetchData(url, "DELETE");
    if (response === null) return id;
    return rejectWithValue("Erreur lors de la suppression.");
  }
);

const adminGameSlice = createSlice({
  name: "adminGames",
  initialState,
  reducers: {
    resetAdminGames: (state) => {
      state.games = [];
      state.loading = false;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loadAllAdminGames.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadAllAdminGames.fulfilled, (state, action) => {
        state.loading = false;
        state.games = action.payload;
      })
      .addCase(loadAllAdminGames.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      .addCase(addGame.fulfilled, (state, action) => {
        state.games.push(action.payload);
      })

      .addCase(updateGame.fulfilled, (state, action) => {
        const index = state.games.findIndex((g) => g.id === action.payload.id);
        if (index !== -1) state.games[index] = action.payload;
      })

      .addCase(deleteGame.fulfilled, (state, action) => {
        state.games = state.games.filter((g) => g.id !== action.payload);
      })

      .addCase(deleteGame.rejected, (state, action) => {
        state.error = action.payload as string;
      });
  },
});
export const selectGames = (state: RootState) => state.adminGames.games;
export const selectGameById = (id: string) => (state: RootState) =>
  state.adminGames.games.find((g) => g.id === id);

export const { resetAdminGames } = adminGameSlice.actions;
export default adminGameSlice.reducer;
