import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import path from "path";
import copy from "rollup-plugin-copy";

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    react(),
    copy({
      targets: [
        {
          src: "../images/*", // Le dossier à copier
          dest: "../gameshelf-back/GameShelf.API/wwwroot/images/", // Destination dans wwwroot
        },
      ],
      hook: "writeBundle", // Copie après la génération du build
    }),
  ],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"), // Alias pour simplifier les imports
    },
  },
  build: {
    outDir: "../gameshelf-back/GameShelf.API/wwwroot", // Génère le build dans wwwroot
    emptyOutDir: true, // Vide le dossier avant chaque build
    rollupOptions: {
      input: path.resolve(__dirname, "index.html"), // Fichier d'entrée principal
    },
  },
  server: {
    port: 5173,
    open: true, // Ouvre automatiquement le navigateur
  },
});
