import { Navigate } from "react-router-dom";
import { useAppSelector } from "@/store/hooks";
import { JSX } from "react";
import { toUserViewModel } from "@/types/User";

export default function AdminRoute({ children }: { children: JSX.Element }) {
  const user =
    useAppSelector((state) => state.auth.user) ??
    toUserViewModel({
      id: "default-id",
      externalId: "default-external-id",
      email: "default@example.com",
      pseudo: "DevUser",
      givenName: "Dev",
      surname: "User",
      role: "Admin",
    });

  if (!user || !user.isAdmin) {
    return <Navigate to="/unauthorized" replace />;
  }

  return children;
}
