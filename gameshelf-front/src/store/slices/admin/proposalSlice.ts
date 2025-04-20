import { RootState } from "@/store/store";
import { Game } from "@/types/Game";
import { UserProposal } from "@/types/UserProposal";
import { fetchData } from "@/utils/fetchData";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

interface AdminProposalState {
  proposals: UserProposal[];
  loading: boolean;
  error: string | null;
}

const initialState: AdminProposalState = {
  proposals: [],
  loading: false,
  error: null,
};

// GET ALL
export const loadAllProposals = createAsyncThunk<UserProposal[]>(
  "adminProposals/loadAllProposals",
  async (_, { rejectWithValue }) => {
    const response = await fetchData<UserProposal[]>(
      `${import.meta.env.VITE_API_BASE_URL}/api/v1/UserProposals`,
      "GET"
    );
    if (!response)
      return rejectWithValue("Erreur lors du chargement des propositions.");
    return response;
  }
);

// ACCEPT
export const acceptProposal = createAsyncThunk<Game, string>(
  "adminProposals/acceptProposal",
  async (id, { rejectWithValue }) => {
    const url = `${
      import.meta.env.VITE_API_BASE_URL
    }/api/v1/UserProposals/${id}/accept`;
    const response = await fetchData<Game>(url, "PUT");
    if (!response)
      return rejectWithValue("Erreur lors de l'acceptation de la proposition.");
    return response;
  }
);

// REJECT
export const rejectProposal = createAsyncThunk<string, string>(
  "adminProposals/rejectProposal",
  async (id, { rejectWithValue }) => {
    const url = `${
      import.meta.env.VITE_API_BASE_URL
    }/api/v1/UserProposals/${id}/reject`;
    const response = await fetchData(url, "PUT");
    if (response === null) return id;
    return rejectWithValue("Erreur lors du refus de la proposition.");
  }
);

const adminProposalSlice = createSlice({
  name: "adminProposals",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(loadAllProposals.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(loadAllProposals.fulfilled, (state, action) => {
        state.loading = false;
        state.proposals = action.payload;
      })
      .addCase(loadAllProposals.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      .addCase(acceptProposal.fulfilled, (state, action) => {
        const acceptedProposalId = action.meta.arg;
        const proposal = state.proposals.find(
          (p) => p.id === acceptedProposalId
        );
        if (proposal) proposal.statut = "Validee";
      })

      .addCase(rejectProposal.fulfilled, (state, action) => {
        const index = state.proposals.findIndex((p) => p.id === action.payload);
        if (index !== -1) state.proposals[index].statut = "Refusee";
      })

      .addCase(acceptProposal.rejected, (state, action) => {
        state.error = action.payload as string;
      })
      .addCase(rejectProposal.rejected, (state, action) => {
        state.error = action.payload as string;
      });
  },
});

export const selectAdminProposals = (state: RootState) =>
  state.adminProposals.proposals;

export default adminProposalSlice.reducer;
