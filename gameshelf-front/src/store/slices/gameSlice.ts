import { Game } from "@/types/Game";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";

interface GameState {
  games: Game[] | [];
  loading: boolean;
  error: string | null;
}

const initialState: GameState = {
  games: [],
  loading: false,
  error: null,
};

// export const loadAllGames = createAsyncThunk<Game[]>(
//   "games/loadAllGames",
//   async (_, { rejectWithValue }) => {
//     const response = await fetchData<Game[]>(
//       `${import.meta.env.VITE_API_BASE_URL}/api/v1/Games`
//     );
//     if (!response) return rejectWithValue("Erreur lors du chargement des jeux");
//     return response;
//   }
// );
export const loadAllGames = createAsyncThunk<
  Game[],
  { genres?: string[]; platforms?: string[] } | undefined
>("games/loadAllGames", async (filters, { rejectWithValue }) => {
  const params = new URLSearchParams();

  filters?.genres?.forEach((g) => params.append("genres", g));
  filters?.platforms?.forEach((p) => params.append("platforms", p));

  const response = await fetchData<Game[]>(
    `${import.meta.env.VITE_API_BASE_URL}/api/v1/Games?${params.toString()}`
  );

  if (!response) return rejectWithValue("Erreur lors du chargement des jeux");
  return response;
});

const gameSlice = createSlice({
  name: "games",
  initialState,
  reducers: {
    setGames: (state, action: PayloadAction<Game[]>) => {
      state.games = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loadAllGames.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadAllGames.fulfilled, (state, action) => {
        state.loading = false;
        state.games = action.payload;
      })
      .addCase(loadAllGames.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export const { setGames } = gameSlice.actions;
export default gameSlice.reducer;
