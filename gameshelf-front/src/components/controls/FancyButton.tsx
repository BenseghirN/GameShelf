import { Button } from "@mui/material";
import { SxProps } from "@mui/system";
import { ReactNode } from "react";

type Props = {
  children: ReactNode;
  color?: string;
  icon?: ReactNode;
  onClick?: () => void;
  sx?: SxProps;
};

export default function FancyButton({
  children,
  color = "#2196f3",
  icon,
  onClick,
  sx,
}: Props) {
  return (
    <Button
      onClick={onClick}
      startIcon={icon}
      sx={{
        borderRadius: "50px",
        paddingX: 3,
        paddingY: 1.2,
        fontWeight: "bold",
        backgroundColor: color,
        color: "white",
        boxShadow: "0 4px 10px rgba(0,0,0,0.2)",
        transition: "all 0.3s",
        "&:hover": {
          backgroundColor: color,
          transform: "scale(1.05)",
          boxShadow: "0 6px 14px rgba(0,0,0,0.3)",
        },
        ...sx,
      }}
    >
      {children}
    </Button>
  );
}
