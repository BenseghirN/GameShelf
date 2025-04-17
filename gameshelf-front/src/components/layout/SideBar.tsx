import HomeIcon from "@mui/icons-material/Home";
import GamesIcon from "@mui/icons-material/SportsEsports";
import PeopleIcon from "@mui/icons-material/People";
import AdminIcon from "@mui/icons-material/AdminPanelSettings";
import ChecklistRtlIcon from "@mui/icons-material/ChecklistRtl";
import BugReportIcon from "@mui/icons-material/BugReport";
import { useLocation, useNavigate } from "react-router-dom";
import {
  Collapse,
  Divider,
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { UserViewModel } from "@/types/User";
import { useState } from "react";
import { ExpandLess, ExpandMore } from "@mui/icons-material";

const drawerWidth = 240;

const navItems = [
  { label: "Home", path: "/home", icon: <HomeIcon /> },
  { label: "Tous les jeux", path: "/games", icon: <GamesIcon /> },
  { label: "Ma collection", path: "/library", icon: <ChecklistRtlIcon /> },
  { label: "Test", path: "/test", icon: <BugReportIcon /> },
];

const adminNavItems = [
  { label: "Utilisateurs", path: "/admin/users", icon: <PeopleIcon /> },
  // Tu peux ajouter d’autres sous-liens ici
];

export default function SideBar({ user }: { user: UserViewModel | null }) {
  const location = useLocation();
  const navigate = useNavigate();
  const [adminOpen, setAdminOpen] = useState(false);

  const isActive = (path: string) => location.pathname === path;

  const handleNavigate = (path: string) => {
    navigate(path);
  };

  return (
    <Drawer
      variant="permanent"
      anchor="left"
      slotProps={{
        paper: {
          sx: {
            top: "auto",
            height: "calc(100vh - 70px)",
            width: drawerWidth,
          },
        },
      }}
    >
      <List>
        {/* Liens principaux */}
        {navItems.map((item) => (
          <ListItem key={item.label} disablePadding>
            <ListItemButton
              selected={isActive(item.path)}
              onClick={() => handleNavigate(item.path)}
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.label} />
            </ListItemButton>
          </ListItem>
        ))}

        {/* Liens admin en collapsible */}
        {user?.isAdmin && (
          <>
            <Divider sx={{ my: 1 }} />
            <ListItemButton onClick={() => setAdminOpen(!adminOpen)}>
              <ListItemIcon>
                <AdminIcon />
              </ListItemIcon>
              <ListItemText primary="Admin" />
              {adminOpen ? <ExpandLess /> : <ExpandMore />}
            </ListItemButton>

            <Collapse in={adminOpen} timeout="auto" unmountOnExit>
              <List component="div" disablePadding>
                {adminNavItems.map((item) => (
                  <ListItem key={item.label} disablePadding>
                    <ListItemButton
                      sx={{ pl: 4 }}
                      selected={isActive(item.path)}
                      onClick={() => handleNavigate(item.path)}
                    >
                      <ListItemIcon>{item.icon}</ListItemIcon>
                      <ListItemText primary={item.label} />
                    </ListItemButton>
                  </ListItem>
                ))}
              </List>
            </Collapse>
          </>
        )}
      </List>
    </Drawer>
  );
}
