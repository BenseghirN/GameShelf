import { configureStore } from "@reduxjs/toolkit";
import authReducer from "./slices/authSlice";
import gameReducer from "./slices/gameSlice";
import libraryReducer from "./slices/librarySlice";
import tagReducer from "./slices/admin/tagSlice";
import platformReducer from "./slices/admin/platformSlice";
import adminGameReducer from "./slices/admin/gameSlice";
import adminUserReducer from "./slices/admin/userSlice";
import adminUserProposalReducer from "./slices/admin/proposalSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    games: gameReducer,
    library: libraryReducer,
    adminTags: tagReducer,
    adminPlatforms: platformReducer,
    adminGames: adminGameReducer,
    adminUsers: adminUserReducer,
    adminProposals: adminUserProposalReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
