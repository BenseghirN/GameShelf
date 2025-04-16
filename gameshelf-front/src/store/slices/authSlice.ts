import { toUserViewModel, User, UserViewModel } from "@/types/User";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";

interface AuthState {
  user: UserViewModel | null;
  loading: boolean;
  error: string | null;
}

const initialState: AuthState = {
  user: null,
  loading: false,
  error: null,
};

export const loadCurrentUser = createAsyncThunk(
  "auth/loadCurrentUser",
  async (): Promise<User | null> => {
    return await fetchData<User>(
      `${import.meta.env.VITE_API_BASE_URL}/Auth/user-info`
    );
  }
);

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logout(state) {
      state.user = null;
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loadCurrentUser.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(
        loadCurrentUser.fulfilled,
        (state, action: PayloadAction<User | null>) => {
          state.user = action.payload ? toUserViewModel(action.payload) : null;
          state.loading = false;
          state.error = null;
        }
      )
      .addCase(loadCurrentUser.rejected, (state, action) => {
        state.user = null;
        state.loading = false;
        state.error = action.error.message || "Une erreur est survenue.";
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;
