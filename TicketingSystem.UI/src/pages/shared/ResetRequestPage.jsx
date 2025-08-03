import React, { useState } from 'react'
import axios from 'axios'
import { TextField, Button, Snackbar, Alert, Typography, Paper } from '@mui/material'

export default function ResetRequestPage() {
  const [email, setEmail] = useState('')
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'info' })

  const handleRequest = async () => {
    try {
      await axios.post('/api/reset-request', { email })
      setSnackbar({ open: true, message: 'Reset link sent to your email.', severity: 'success' })
    } catch {
      setSnackbar({ open: true, message: 'Failed to send reset link.', severity: 'error' })
    }
  }

  return (
    <div className="flex justify-center items-center h-screen bg-gray-50 dark:bg-gray-900">
      <Paper elevation={3} className="p-6 w-full max-w-sm space-y-4">
        <Typography variant="h6">Reset Your Password</Typography>
        <TextField
          label="Email"
          type="email"
          fullWidth
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <Button variant="contained" fullWidth onClick={handleRequest}>
          Send Reset Link
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