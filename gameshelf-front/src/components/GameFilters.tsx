import { Autocomplete, TextField, Box } from "@mui/material";
import { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { fetchData } from "@/utils/fetchData";
import { Platform } from "@/types/Platform";
import { Tag } from "@/types/Tag";

export default function GameFilters() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const [tags, setTags] = useState<string[]>(searchParams.getAll("genres"));
  const [platforms, setPlatforms] = useState<string[]>(
    searchParams.getAll("platforms")
  );

  const [allPlatforms, setAllPlatforms] = useState<Platform[]>([]);
  const [allTags, setAllTags] = useState<Tag[]>([]);

  useEffect(() => {
    const fetchFilters = async () => {
      const [platformRes, tagRes] = await Promise.all([
        fetchData<Platform[]>(
          `${import.meta.env.VITE_API_BASE_URL}/api/v1/Platforms`
        ),
        fetchData<Tag[]>(`${import.meta.env.VITE_API_BASE_URL}/api/v1/Tags`),
      ]);
      if (platformRes) setAllPlatforms(platformRes);
      if (tagRes) setAllTags(tagRes);
    };
    fetchFilters();
  }, []);

  useEffect(() => {
    const params = new URLSearchParams();
    tags.forEach((g) => params.append("genres", g));
    platforms.forEach((p) => params.append("platforms", p));
    navigate({ pathname: "/games", search: params.toString() });
  }, [tags, platforms]);

  return (
    <Box display="flex" gap={2} flexWrap="wrap" mb={2}>
      <Autocomplete
        multiple
        options={allTags.map((tag) => tag.nom)}
        value={tags}
        onChange={(_, value) => setTags(value)}
        renderInput={(params) => <TextField {...params} label="Genres" />}
        sx={{ minWidth: 200 }}
      />

      <Autocomplete
        multiple
        options={allPlatforms.map((p) => p.nom)}
        value={platforms}
        onChange={(_, value) => setPlatforms(value)}
        renderInput={(params) => <TextField {...params} label="Plateformes" />}
        sx={{ minWidth: 200 }}
      />
    </Box>
  );
}
