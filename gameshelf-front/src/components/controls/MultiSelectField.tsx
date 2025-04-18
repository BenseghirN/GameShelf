import {
  Checkbox,
  FormControl,
  InputLabel,
  ListItemText,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";

export default function MultiSelectField({
  label,
  options,
  value,
  onChange,
}: {
  label: string;
  options: { id: string; nom: string }[];
  value: string[];
  onChange: (val: string[]) => void;
}) {
  const handleChange = (event: SelectChangeEvent<string[]>) => {
    const {
      target: { value },
    } = event;
    onChange(typeof value === "string" ? value.split(",") : value);
  };

  return (
    <FormControl fullWidth>
      <InputLabel>{label}</InputLabel>
      <Select
        multiple
        value={value}
        onChange={handleChange}
        renderValue={(selected) =>
          options
            .filter((opt) => selected.includes(opt.id))
            .map((opt) => opt.nom)
            .join(", ")
        }
      >
        {options.map((opt) => (
          <MenuItem key={opt.id} value={opt.id}>
            <Checkbox checked={value.includes(opt.id)} />
            <ListItemText primary={opt.nom} />
          </MenuItem>
        ))}
      </Select>
    </FormControl>
  );
}
