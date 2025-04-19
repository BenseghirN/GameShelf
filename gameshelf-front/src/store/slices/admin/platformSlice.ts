import { RootState } from "@/store/store";
import { Platform } from "@/types/Platform";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

interface PlatformState {
  platforms: Platform[];
  loading: boolean;
  error: string | null;
}

const initialState: PlatformState = {
  platforms: [],
  loading: false,
  error: null,
};

// GET
export const loadAllPlatforms = createAsyncThunk<Platform[]>(
  "platforms/loadAllPlatforms",
  async (_, { rejectWithValue }) => {
    const response = await fetchData<Platform[]>(
      `${import.meta.env.VITE_API_BASE_URL}/api/v1/Platforms`
    );
    if (!response)
      return rejectWithValue("Impossible de charger les plateformes.");
    return response;
  }
);

// POST
export const addPlatform = createAsyncThunk<
  Platform,
  { nom: string; imagePath: string | null }
>("platforms/addPlatform", async ({ nom, imagePath }, { rejectWithValue }) => {
  const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Platforms`;
  const response = await fetchData<Platform>(url, "POST", {
    nom,
    imagePath: imagePath?.trim() || "",
  });
  if (!response)
    return rejectWithValue("Erreur lors de l'ajout de la plateforme.");
  return response;
});

// PUT
export const updatePlatform = createAsyncThunk<Platform, Platform>(
  "platforms/updatePlatform",
  async (platform, { rejectWithValue }) => {
    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Platforms/${
      platform.id
    }`;
    const response = await fetchData<Platform>(url, "PUT", {
      nom: platform.nom,
      imagePath: platform.imagePath?.trim() || "",
    });
    if (!response) return rejectWithValue("Erreur lors de la modification.");
    return response;
  }
);

// DELETE
export const deletePlatform = createAsyncThunk<string, string>(
  "platforms/deletePlatform",
  async (id, { rejectWithValue }) => {
    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Platforms/${id}`;
    const result = await fetchData(url, "DELETE");
    if (result === null) return id;
    return rejectWithValue("Erreur lors de la suppression.");
  }
);

const platformSlice = createSlice({
  name: "platforms",
  initialState,
  reducers: {
    resetPlatforms: (state) => {
      state.platforms = [];
      state.loading = false;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loadAllPlatforms.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadAllPlatforms.fulfilled, (state, action) => {
        state.platforms = action.payload;
        state.loading = false;
      })
      .addCase(loadAllPlatforms.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      .addCase(addPlatform.fulfilled, (state, action) => {
        state.platforms.push(action.payload);
      })

      .addCase(updatePlatform.fulfilled, (state, action) => {
        const index = state.platforms.findIndex(
          (p) => p.id === action.payload.id
        );
        if (index !== -1) state.platforms[index] = action.payload;
      })

      .addCase(deletePlatform.fulfilled, (state, action) => {
        state.platforms = state.platforms.filter(
          (p) => p.id !== action.payload
        );
      })

      .addCase(deletePlatform.rejected, (state, action) => {
        state.error = action.payload as string;
      });
  },
});

export const selectPlatforms = (state: RootState) =>
  state.adminPlatforms.platforms;
export const selectPlatformById = (id: string) => (state: RootState) =>
  state.adminPlatforms.platforms.find((p) => p.id === id);

export const { resetPlatforms } = platformSlice.actions;
export default platformSlice.reducer;
