import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { useAuth } from '../../auth/useAuth'
import { TextField, Button, Typography, Paper, Snackbar, Alert } from '@mui/material'

export default function LoginPage() {
  const navigate = useNavigate()
  const { login } = useAuth()
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'error' })

  const handleLogin = async () => {
    try {
      await login(username, password)
      navigate('/' + (localStorage.getItem('role') === 'agent' ? 'agent' : 'customer'))
    } catch (err) {
      setSnackbar({ open: true, message: 'Login failed. Check credentials.', severity: 'error' })
    }
  }

  return (
    <div className="flex justify-center items-center h-screen bg-gray-50 dark:bg-gray-900">
      <Paper elevation={4} className="p-6 w-full max-w-sm space-y-4">
        <Typography variant="h5" fontWeight="bold">Login</Typography>
        <TextField
          label="Username"
          fullWidth
          value={username}
          onChange={e => setUsername(e.target.value)}
        />
        <TextField
          label="Password"
          type="password"
          fullWidth
          value={password}
          onChange={e => setPassword(e.target.value)}
        />
        <Button variant="contained" fullWidth onClick={handleLogin}>Login</Button>
      </Paper>
      <Snackbar
        open={snackbar.open}
        autoHideDuration={3000}
        onClose={() => setSnackbar({ ...snackbar, open: false })}
      >
        <Alert severity={snackbar.severity} sx={{ width: '100%' }}>{snackbar.message}</Alert>
      </Snackbar>
    </div>
  )
}