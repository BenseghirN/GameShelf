import { Box } from "@mui/material";
import TopBar from "./TopBar";
import { useAppSelector } from "@/store/hooks";
import SideBar from "./SideBar";
import { UserViewModel } from "@/types/User";

export default function MainLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const user =
    useAppSelector((state) => state.auth.user) ??
    ({
      id: "default-id",
      externalId: "default-external-id",
      email: "default@example.com",
      pseudo: "DevUser",
      givenName: "Dev",
      surname: "User",
      role: "User",
    } as UserViewModel);

  return (
    <Box sx={{ display: "flex", flexDirection: "column", height: "100vh" }}>
      {/* TOPBAR FIXE EN HAUT */}
      <TopBar user={user} />

      {/* CONTENU PRINCIPAL EN DESSOUS AVEC SIDEBAR + PAGE */}
      <Box sx={{ display: "flex", flexGrow: 1, overflow: "hidden" }}>
        {/* SIDEBAR À GAUCHE */}
        <SideBar user={user} />

        {/* CONTENU PRINCIPAL */}
        <Box
          component="main"
          sx={{
            flexGrow: 1,
            p: 3,
            overflowY: "auto",
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          {children}
        </Box>
      </Box>
    </Box>
  );
}
