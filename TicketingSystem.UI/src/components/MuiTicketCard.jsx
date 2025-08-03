import React from 'react'
import { Card, CardContent, Typography, Chip } from '@mui/material'

export default function MuiTicketCard({ title, status, assignedTo }) {
  return (
    <Card variant="outlined" sx={{ bgcolor: 'background.paper' }}>
      <CardContent>
        <Typography variant="h6" fontWeight="bold">{title}</Typography>
        <Typography variant="body2" sx={{ my: 1 }}>
          Status: <Chip label={status} size="small" color={status === 'Closed' ? 'success' : 'warning'} />
        </Typography>
        <Typography variant="body2">
          Assigned to: {assignedTo || 'Unassigned'}
        </Typography>
      </CardContent>
    </Card>
  )
}