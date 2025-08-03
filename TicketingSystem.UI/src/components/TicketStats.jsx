import React from 'react'
import { Card, CardContent, Typography, Chip } from '@mui/material'
import { motion } from 'framer-motion'

export default function TicketStats({ tickets }) {
  const total = tickets.length
  const open = tickets.filter(t => t.status.toLowerCase() === 'open').length
  const closed = tickets.filter(t => t.status.toLowerCase() === 'closed').length

  return (
    <motion.div
      initial={{ opacity: 0, y: -20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.4 }}
      className="grid grid-cols-1 sm:grid-cols-3 gap-4 mb-6"
    >
      <Card variant="outlined">
        <CardContent>
          <Typography variant="h6">Total Tickets</Typography>
          <Chip label={total} color="default" />
        </CardContent>
      </Card>
      <Card variant="outlined">
        <CardContent>
          <Typography variant="h6">Open</Typography>
          <Chip label={open} color="warning" />
        </CardContent>
      </Card>
      <Card variant="outlined">
        <CardContent>
          <Typography variant="h6">Closed</Typography>
          <Chip label={closed} color="success" />
        </CardContent>
      </Card>
    </motion.div>
  )
}