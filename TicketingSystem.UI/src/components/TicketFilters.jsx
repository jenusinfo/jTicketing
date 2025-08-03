import React from 'react'
import { MenuItem, TextField } from '@mui/material'

export default function TicketFilters({ status, priority, onStatusChange, onPriorityChange }) {
  return (
    <div className="flex gap-4 mb-6">
      <TextField
        select
        label="Status"
        value={status}
        onChange={(e) => onStatusChange(e.target.value)}
        size="small"
      >
        <MenuItem value="">All</MenuItem>
        <MenuItem value="Open">Open</MenuItem>
        <MenuItem value="Closed">Closed</MenuItem>
      </TextField>

      <TextField
        select
        label="Priority"
        value={priority}
        onChange={(e) => onPriorityChange(e.target.value)}
        size="small"
      >
        <MenuItem value="">All</MenuItem>
        <MenuItem value="Low">Low</MenuItem>
        <MenuItem value="Medium">Medium</MenuItem>
        <MenuItem value="High">High</MenuItem>
      </TextField>
    </div>
  )
}