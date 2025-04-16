import { UserDto } from "@/types/UserDto";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";

interface AuthState {
  user: UserDto | null;
  loading: boolean;
}

const initialState: AuthState = {
  user: null,
  loading: false,
};

export const loadCurrentUser = createAsyncThunk(
  "auth/loadCurrentUser",
  async (): Promise<UserDto | null> => {
    return await fetchData<UserDto>(
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
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(loadCurrentUser.pending, (state) => {
        state.loading = true;
      })
      .addCase(
        loadCurrentUser.fulfilled,
        (state, action: PayloadAction<UserDto | null>) => {
          state.user = action.payload;
          state.loading = false;
        }
      )
      .addCase(loadCurrentUser.rejected, (state) => {
        state.user = null;
        state.loading = false;
      });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;
