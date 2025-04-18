import { getGameFullImageUrl } from "@/utils/imageUtils";
import { TextField } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";

export default function GameImageField({
  value,
  onChange,
}: {
  value: string;
  onChange: (val: string) => void;
}) {
  const [previewUrl, setPreviewUrl] = useState<string>(
    getGameFullImageUrl(value)
  );

  useEffect(() => {
    setPreviewUrl(getGameFullImageUrl(value));
  }, [value]);

  return (
    <Box display="flex" flexDirection="column" alignItems="center" gap={2}>
      <img
        src={previewUrl}
        alt="Jaquette du jeu"
        style={{
          width: 200,
          height: "auto",
          borderRadius: 8,
          objectFit: "cover",
        }}
      />
      <TextField
        label="Chemin de l'image (imgPath)"
        fullWidth
        value={value}
        onChange={(e) => onChange(e.target.value)}
      />
    </Box>
  );
}
