import React from 'react'
import { useAuth } from '../../auth/useAuth'
import SidebarLayout from '../../layouts/SidebarLayout'
import { Typography, Paper, Chip } from '@mui/material'

export default function UserProfile() {
  const { username, role } = useAuth()
  const theme = localStorage.getItem('theme') || 'light'

  return (
    <SidebarLayout>
      <Typography variant="h5" className="mb-4 font-bold">User Profile</Typography>
      <Paper elevation={3} className="p-4 space-y-4 max-w-md">
        <div>
          <Typography variant="subtitle2">Username</Typography>
          <Typography variant="body1">{username}</Typography>
        </div>
        <div>
          <Typography variant="subtitle2">Role</Typography>
          <Chip label={role} color={role === 'agent' ? 'primary' : 'secondary'} />
        </div>
        <div>
          <Typography variant="subtitle2">Theme</Typography>
          <Chip label={theme.charAt(0).toUpperCase() + theme.slice(1)} />
        </div>
      </Paper>
    </SidebarLayout>
  )
}