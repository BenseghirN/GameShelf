import { Navigate } from "react-router-dom";
import { useAppSelector } from "@/store/hooks";
import { JSX } from "react";

export default function AdminRoute({ children }: { children: JSX.Element }) {
  const user = useAppSelector((state) => state.auth.user);

  if (!user || !user.isAdmin) {
    return <Navigate to="/unauthorized" replace />;
  }

  return children;
}
