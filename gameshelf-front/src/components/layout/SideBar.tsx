import HomeIcon from "@mui/icons-material/Home";
import GamesIcon from "@mui/icons-material/SportsEsports";
import PeopleIcon from "@mui/icons-material/People";
import { useLocation, useNavigate } from "react-router-dom";
import {
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { UserViewModel } from "@/types/User";

const drawerWidth = 240;

const navItems = [
  { label: "Home", path: "/home", icon: <HomeIcon /> },
  { label: "Ma collection", path: "/library", icon: <GamesIcon /> },
  { label: "Test", path: "/test", icon: <GamesIcon /> },
  {
    label: "Utilisateurs",
    path: "/admin/users",
    icon: <PeopleIcon />,
    adminOnly: true,
  },
];

export default function SideBar({ user }: { user: UserViewModel }) {
  const location = useLocation();
  const navigate = useNavigate();

  const visibleItems = navItems.filter(
    (item) => !item.adminOnly || user.isAdmin
  );

  return (
    <Drawer
      variant="permanent"
      anchor="left"
      slotProps={{
        paper: {
          sx: {
            top: "auto",
            height: "calc(100vh - 64px)",
            width: drawerWidth,
          },
        },
      }}
    >
      <List>
        {visibleItems.map((item) => (
          <ListItem key={item.label} disablePadding>
            <ListItemButton
              selected={location.pathname === item.path}
              onClick={() => item.path && navigate(item.path)}
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.label} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>
    </Drawer>
  );
}
