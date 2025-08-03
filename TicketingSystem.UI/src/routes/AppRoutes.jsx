import React from 'react'
import { Routes, Route, Navigate } from 'react-router-dom'
import LoginPage from '../pages/shared/LoginPage'
import ResetRequestPage from '../pages/shared/ResetRequestPage'
import ResetConfirmPage from '../pages/shared/ResetConfirmPage'
import AgentDashboard from '../pages/agent/AgentDashboard'
import CustomerDashboard from '../pages/customer/CustomerDashboard'
import UserProfile from '../pages/shared/UserProfile'
import { useAuth } from '../auth/useAuth'

export default function AppRoutes() {
  const { role, token } = useAuth()

  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/reset" element={<ResetRequestPage />} />
      <Route path="/reset-confirm" element={<ResetConfirmPage />} />
      <Route path="/login" element={<LoginPage />} />
      <Route path="/profile" element={token ? <UserProfile /> : <Navigate to="/login" />} />
      {token && role === 'agent' && (
        <Route path="/agent" element={<AgentDashboard />} />
      )}
      {token && role === 'customer' && (
        <Route path="/customer" element={<CustomerDashboard />} />
      )}
      <Route path="*" element={<Navigate to={token ? `/${role}` : "/login"} />} />
    </Routes>
  )
}