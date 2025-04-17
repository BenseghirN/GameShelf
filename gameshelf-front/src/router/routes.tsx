import { Navigate, Route, Routes } from "react-router-dom";
import MainLayout from "@/components/layout/MainLayout";
import HomePage from "@/pages/HomePage";
import LoginPage from "@/pages/LoginPage";
import TestPage from "@/pages/TestPage";
import UnauthorizedPage from "@/pages/UnauthorizedPage";
import GamesPage from "@/pages/GamePage";

const AppRoutes = () => {
  return (
    <Routes>
      {/* Redirige "/" vers "/login" */}
      <Route path="/" element={<Navigate to="/login" replace />} />

      {/* Login sans layout */}
      <Route path="/login" element={<LoginPage />} />

      {/* Pages avec layout */}
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
        path="/test"
        element={
          <MainLayout>
            <TestPage />
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

      {/* <Route path="/" element={<LoginPage />} />
      <Route path="/home" element={<HomePage />} />
      <Route path="/test" element={<TestPage />} /> */}
    </Routes>
  );
};

export default AppRoutes;
