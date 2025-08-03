import React, { useState } from 'react'
import { useSearchParams, useNavigate } from 'react-router-dom'
import axios from 'axios'
import { TextField, Button, Snackbar, Alert, Typography, Paper } from '@mui/material'

export default function ResetConfirmPage() {
  const [params] = useSearchParams()
  const [password, setPassword] = useState('')
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'info' })
  const navigate = useNavigate()
  const token = params.get('token')

  const handleReset = async () => {
    try {
      await axios.post('/api/reset-confirm', { token, password })
      setSnackbar({ open: true, message: 'Password updated successfully.', severity: 'success' })
      setTimeout(() => navigate('/login'), 2000)
    } catch {
      setSnackbar({ open: true, message: 'Reset failed.', severity: 'error' })
    }
  }

  return (
    <div className="flex justify-center items-center h-screen bg-gray-50 dark:bg-gray-900">
      <Paper elevation={3} className="p-6 w-full max-w-sm space-y-4">
        <Typography variant="h6">Set New Password</Typography>
        <TextField
          label="New Password"
          type="password"
          fullWidth
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <Button variant="contained" fullWidth onClick={handleReset}>
          Reset Password
        </Button>
      </Paper>
      <Snackbar
        open={snackbar.open}
        autoHideDuration={4000}
        onClose={() => setSnackbar({ ...snackbar, open: false })}
      >
        <Alert severity={snackbar.severity} sx={{ width: '100%' }}>
          {snackbar.message}
        </Alert>
      </Snackbar>
    </div>
  )
}