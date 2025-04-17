import { Card, CardMedia, CardContent, Typography } from "@mui/material";
import { Game } from "@/types/Game";
import { getGameFullImageUrl } from "@/utils/imageUtils";
type Props = {
  game: Game;
  onClick?: () => void;
};

export default function GameCard(props: Props) {
  return (
    <Card
      sx={{
        width: 220,
        height: 350,
        display: "flex",
        flexDirection: "column",
        cursor: props.onClick ? "pointer" : "default",
      }}
      onClick={props.onClick}
    >
      <CardMedia
        component="img"
        image={getGameFullImageUrl(props.game.imagePath)}
        alt={props.game.titre}
        sx={{
          height: 280,
          objectFit: "cover",
        }}
      />
      <CardContent sx={{ flexGrow: 1 }}>
        <Typography variant="subtitle1" align="center" noWrap>
          {props.game.titre}
        </Typography>
      </CardContent>
    </Card>
  );
}
