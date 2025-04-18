import { RootState } from "@/store/store";
import { Tag } from "@/types/Tag";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

interface TagState {
  tags: Tag[];
  loading: boolean;
  error: string | null;
}

const initialState: TagState = {
  tags: [],
  loading: false,
  error: null,
};

// GET
export const loadAllTags = createAsyncThunk<Tag[]>(
  "tags/loadAllTags",
  async (_, { rejectWithValue }) => {
    const response = await fetchData<Tag[]>(
      `${import.meta.env.VITE_API_BASE_URL}/api/v1/Tags`
    );
    if (!response) return rejectWithValue("Impossible de charger les tags.");
    return response;
  }
);

// POST
export const addTag = createAsyncThunk<Tag, { nom: string }>(
  "tags/addTag",
  async ({ nom }, { rejectWithValue }) => {
    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Tags`;
    const response = await fetchData<Tag>(url, "POST", { nom });
    if (!response) return rejectWithValue("Erreur lors de l'ajout du tag.");
    return response;
  }
);

// PUT
export const updateTag = createAsyncThunk<Tag, { id: string; nom: string }>(
  "tags/updateTag",
  async ({ id, nom }, { rejectWithValue }) => {
    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Tags/${id}`;
    const response = await fetchData<Tag>(url, "PUT", { nom });
    if (!response) return rejectWithValue("Erreur lors de la modification.");
    return response;
  }
);

// DELETE
export const deleteTag = createAsyncThunk<string, string>(
  "tags/deleteTag",
  async (tagId, { rejectWithValue }) => {
    const url = `${import.meta.env.VITE_API_BASE_URL}/api/v1/Tags/${tagId}`;
    const response = await fetchData(url, "DELETE");
    if (response === null) return tagId;
    return rejectWithValue("Erreur lors de la suppression.");
  }
);

const tagSlice = createSlice({
  name: "tags",
  initialState,
  reducers: {
    resetTags: (state) => {
      state.tags = [];
      state.loading = false;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loadAllTags.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadAllTags.fulfilled, (state, action) => {
        state.tags = action.payload;
        state.loading = false;
      })
      .addCase(loadAllTags.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      .addCase(addTag.fulfilled, (state, action) => {
        state.tags.push(action.payload);
      })

      .addCase(updateTag.fulfilled, (state, action) => {
        const index = state.tags.findIndex((t) => t.id === action.payload.id);
        if (index !== -1) state.tags[index] = action.payload;
      })

      .addCase(deleteTag.fulfilled, (state, action) => {
        state.tags = state.tags.filter((t) => t.id !== action.payload);
      })

      .addCase(deleteTag.rejected, (state, action) => {
        state.error = action.payload as string;
      });
  },
});

export const selectTags = (state: RootState) => state.tags.tags;
export const selectTagById = (id: string) => (state: RootState) =>
  state.tags.tags.find((t) => t.id === id);

export const { resetTags } = tagSlice.actions;
export default tagSlice.reducer;
