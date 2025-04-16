import { Route, Routes } from "react-router-dom";
import HomePage from "@/pages/HomePage";
import LoginPage from "@/pages/LoginPage";
import TestPage from "@/pages/TestPage";

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<LoginPage />} />
      <Route path="/home" element={<HomePage />} />
      <Route path="/test" element={<TestPage />} />
    </Routes>
  );
};

export default AppRoutes;
