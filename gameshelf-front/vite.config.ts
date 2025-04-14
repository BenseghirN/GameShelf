import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src'), // permet d'utiliser '@/components/...' au lieu de chemins relatifs
    },
  },
  build: {
    outDir: '../gameshelf-back/GameShelf.API/wwwroot', // build directement dans ton dossier .NET
    emptyOutDir: true, // vide le dossier avant chaque build
    rollupOptions: {
      input: path.resolve(__dirname, 'index.html'),
    },
  },
  server: {
    port: 5173,
    open: true,
  },
})