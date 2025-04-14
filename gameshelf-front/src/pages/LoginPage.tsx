import { Button, Container, Typography, Box } from '@mui/material'

export default function LoginPage() {
  const handleLogin = () => {
    const returnUrl = encodeURIComponent('/home')
    const loginUrl = `${import.meta.env.VITE_API_BASE_URL}/Auth/connect?returnUrl=${returnUrl}`
    window.location.href = loginUrl
  }

  return (
    <Container maxWidth="sm">
      <Box mt={10} textAlign="center">
        <Typography variant="h4" gutterBottom>
          Bienvenue sur GameShelf 🎮
        </Typography>
        <Typography variant="body1" paragraph>
          Connectez-vous pour accéder à votre bibliothèque de jeux.
        </Typography>
        <Button variant="contained" color="primary" onClick={handleLogin}>
          Se connecter avec Azure
        </Button>
      </Box>
    </Container>
  )
}
