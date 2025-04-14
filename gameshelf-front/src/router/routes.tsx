import { Route, Routes } from 'react-router-dom'
import HomePage from '@/pages/HomePage'
import LoginPage from '@/pages/LoginPage'

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<LoginPage  />} />
      <Route path="/home" element={<HomePage />} />
    </Routes>
  )
}

export default AppRoutes
