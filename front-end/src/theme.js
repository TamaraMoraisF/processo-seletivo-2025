// src/theme.js
import { createTheme } from "@mui/material/styles";
import { blueGrey, amber } from "@mui/material/colors";

const theme = createTheme({
  palette: {
    primary: {
      main: blueGrey[900],
      contrastText: "#fff",
    },
    secondary: {
      main: amber[500],
    },
    background: {
      default: "#f7f8fa",
    },
  },
  typography: {
    fontFamily: "Roboto, sans-serif",
    h1: { fontSize: "2rem", fontWeight: 700 },
    h2: { fontSize: "1.5rem", fontWeight: 600 },
  },
});

export default theme;
