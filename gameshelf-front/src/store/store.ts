import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import gameReducer from "./slices/gameSlice";
import libraryReducer from "./slices/librarySlice";
import tagReducer from "./slices/admin/tagSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    games: gameReducer,
    library: libraryReducer,
    tags: tagReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
