import { useAppDispatch } from "@/store/hooks";
import { logout } from "@/store/slices/authSlice";
import { UserViewModel } from "@/types/User";
import { Button, AppBar, Toolbar, Typography } from "@mui/material";
import { motion } from "framer-motion";
import Logo from "@/assets/GameShelf.svg";
import { useNavigate } from "react-router-dom";

export default function TopBar({ user }: { user: UserViewModel }) {
  const MotionButton = motion(Button);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleLogout = () => {
    window.location.href = `${
      import.meta.env.VITE_API_BASE_URL
    }/Auth/logout?returnUrl=/`;
    dispatch(logout());
  };

  return (
    <AppBar position="static" color="primary" enableColorOnDark>
      <Toolbar>
        <img
          src={Logo}
          alt="Logo GameShelf"
          style={{ height: 70, marginRight: 8, cursor: "pointer" }}
          onClick={() => navigate("/home")}
        />
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          GameShelf
        </Typography>

        {user && (
          <>
            <Typography variant="body1" sx={{ mr: 2 }}>
              Bonjour, {user.pseudo}
            </Typography>
            <MotionButton
              variant="contained"
              onClick={handleLogout}
              whileHover={{ scale: 1.1 }}
              whileTap={{ scale: 0.95 }}
              transition={{ type: "spring", stiffness: 300 }}
              sx={{
                ml: 2,
                backgroundColor: "#7c3aed",
                color: "white",
                fontWeight: "bold",
                "&:hover": {
                  backgroundColor: "#6b21a8",
                  boxShadow: 4,
                },
              }}
            >
              Déconnexion
            </MotionButton>
          </>
        )}
      </Toolbar>
    </AppBar>
  );
}
