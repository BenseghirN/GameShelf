import { Card, CardMedia, CardContent, Typography } from "@mui/material";
import { Game } from "@/types/Game";
import { getFullImageUrl } from "@/utils/imageUtils";

export default function GameCard({ game }: { game: Game }) {
  return (
    <Card
      sx={{ width: 220, height: 350, display: "flex", flexDirection: "column" }}
    >
      <CardMedia
        component="img"
        image={getFullImageUrl(game.imagePath)}
        alt={game.titre}
        sx={{
          height: 280,
          objectFit: "cover",
        }}
      />
      <CardContent sx={{ flexGrow: 1 }}>
        <Typography variant="subtitle1" align="center" noWrap>
          {game.titre}
        </Typography>
      </CardContent>
    </Card>
  );
}
