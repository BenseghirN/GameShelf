import { RootState } from "@/store/store";
import { toUserViewModel, User } from "@/types/User";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";

interface UserState {
  users: User[];
  loading: boolean;
  error: string | null;
}

const initialState: UserState = {
  users: [],
  loading: false,
  error: null,
};

// GET
export const loadAllUsers = createAsyncThunk<User[]>(
  "adminUsers/loadAll",
  async (_, { rejectWithValue }) => {
    const response = await fetchData<User[]>(
      `${import.meta.env.VITE_API_BASE_URL}/api/v1/Users`
    );
    if (!response)
      return rejectWithValue("Erreur lors du chargement des utilisateurs.");
    return response;
  }
);

// PROMOTE
export const promoteUser = createAsyncThunk<User, string>(
  "adminUsers/promote",
  async (id, { rejectWithValue }) => {
    const response = await fetchData<User>(
      `${import.meta.env.VITE_API_BASE_URL}/api/v1/Users/${id}/promote`,
      "PUT"
    );
    if (!response) return rejectWithValue("Erreur lors de la promotion.");
    return response;
  }
);

// DEMOTE
export const demoteUser = createAsyncThunk<User, string>(
  "adminUsers/demote",
  async (id, { rejectWithValue }) => {
    const response = await fetchData<User>(
      `${import.meta.env.VITE_API_BASE_URL}/api/v1/Users/${id}/demote`,
      "PUT"
    );
    if (!response) return rejectWithValue("Erreur lors de la rétrogradation.");
    return response;
  }
);

const userSlice = createSlice({
  name: "adminUsers",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(loadAllUsers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        loadAllUsers.fulfilled,
        (state, action: PayloadAction<User[]>) => {
          state.loading = false;
          state.users = action.payload.map(toUserViewModel);
        }
      )
      .addCase(loadAllUsers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      .addCase(promoteUser.fulfilled, (state, action) => {
        const updatedUser = toUserViewModel(action.payload);
        const index = state.users.findIndex((u) => u.id === updatedUser.id);
        if (index !== -1) {
          state.users[index] = { ...updatedUser };
        }
      })
      .addCase(demoteUser.fulfilled, (state, action) => {
        const updatedUser = toUserViewModel(action.payload);
        const index = state.users.findIndex((u) => u.id === updatedUser.id);
        if (index !== -1) {
          state.users[index] = { ...updatedUser };
        }
      });
  },
});

export const selectUsers = (state: RootState) => state.adminUsers.users;
export default userSlice.reducer;
