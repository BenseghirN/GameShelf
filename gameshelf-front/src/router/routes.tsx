import { Navigate, Route, Routes } from "react-router-dom";
import MainLayout from "@/components/layout/MainLayout";
import HomePage from "@/pages/HomePage";
import LoginPage from "@/pages/LoginPage";
import UnauthorizedPage from "@/pages/UnauthorizedPage";
import GamesPage from "@/pages/GamePage";
import LibraryPage from "@/pages/LibraryPage";
import NewProposalFormPage from "@/pages/NewProposalFormPage";
import NotFoundPage from "@/pages/NotFoundPage";
import MyProposalPage from "@/pages/ProposalPage";
import AdminRoute from "@/utils/AdminRoute";
import AdminTagsListPage from "@/pages/admin/tags/AdminTagListPage";
import AdminTagFormPage from "@/pages/admin/tags/AdminTagFormPage";

const AppRoutes = () => {
  return (
    <Routes>
      {/* Redirige "/" vers "/login" */}
      <Route path="/" element={<Navigate to="/login" replace />} />

      {/* Login sans layout */}
      <Route path="/login" element={<LoginPage />} />

      {/* Pages avec layout */}
      {/* Left Nav */}
      <Route
        path="/home"
        element={
          <MainLayout>
            <HomePage />
          </MainLayout>
        }
      />
      <Route
        path="/games"
        element={
          <MainLayout>
            <GamesPage />
          </MainLayout>
        }
      />
      <Route
        path="/library"
        element={
          <MainLayout>
            <LibraryPage />
          </MainLayout>
        }
      />
      <Route
        path="/my_proposals"
        element={
          <MainLayout>
            <MyProposalPage />
          </MainLayout>
        }
      />

      {/* Admin Section */}
      <Route
        path="/admin/tags"
        element={
          <MainLayout>
            <AdminRoute>
              <AdminTagsListPage />
            </AdminRoute>
          </MainLayout>
        }
      />
      <Route
        path="/admin/tags/:id"
        element={
          <MainLayout>
            <AdminRoute>
              <AdminTagFormPage />
            </AdminRoute>
          </MainLayout>
        }
      />

      {/* Proposition des utilisateurs */}
      <Route
        path="/proposal"
        element={
          <MainLayout>
            <NewProposalFormPage />
          </MainLayout>
        }
      />

      <Route
        path="/proposal/:id"
        element={
          <MainLayout>
            <NewProposalFormPage />
          </MainLayout>
        }
      />

      {/* Unauthorized */}
      <Route
        path="/unauthorized"
        element={
          <MainLayout>
            <UnauthorizedPage />
          </MainLayout>
        }
      />

      {/* Page 404 */}
      <Route
        path="*"
        element={
          <MainLayout>
            <NotFoundPage />
          </MainLayout>
        }
      />
    </Routes>
  );
};

export default AppRoutes;
