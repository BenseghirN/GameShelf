import { createTheme } from "@mui/material/styles";

const theme = createTheme({
  palette: {
    mode: "light", // ou 'dark' si tu préfères
    primary: {
      main: "#6366F1", // Indigo (accent)
    },
    secondary: {
      main: "#4B5563", // Gris ardoise
    },
    background: {
      default: "#F3F4F6", // Fond clair
      paper: "#FFFFFF",
    },
    success: {
      main: "#10B981", // Vert (Terminé)
    },
    warning: {
      main: "#F59E0B", // Orange (En cours)
    },
    error: {
      main: "#EF4444", // Rouge (Backlog)
    },
    text: {
      primary: "#1F2937", // Gris très foncé
      secondary: "#4B5563",
    },
  },
});

export default theme;
