import React, { useEffect, useState } from 'react'
import { createTicket, getTickets } from '../../api/tickets'
import { useAuth } from '../../auth/useAuth'
import SidebarLayout from '../../layouts/SidebarLayout'
import {
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Typography,
  Card,
  CardContent,
  Chip,
  Snackbar,
  Alert
} from '@mui/material'
import TicketStats from '../../components/TicketStats'
import TicketFilters from '../../components/TicketFilters'

export default function CustomerDashboard() {
  const { username } = useAuth()
  const [tickets, setTickets] = useState([])
  const [filtered, setFiltered] = useState([])
  const [title, setTitle] = useState('')
  const [description, setDescription] = useState('')
  const [open, setOpen] = useState(false)
  const [statusFilter, setStatusFilter] = useState('')
  const [priorityFilter, setPriorityFilter] = useState('')
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' })

  const fetchTickets = () => {
    getTickets().then(res => {
      const userTickets = res.data.filter(t => t.createdBy === username)
      setTickets(userTickets)
    })
  }

  useEffect(() => {
    fetchTickets()
  }, [username])

  useEffect(() => {
    let result = [...tickets]
    if (statusFilter) result = result.filter(t => t.status === statusFilter)
    if (priorityFilter) result = result.filter(t => t.priority === priorityFilter)
    setFiltered(result)
  }, [tickets, statusFilter, priorityFilter])

  const handleSubmit = async () => {
    try {
      await createTicket({ title, description, createdBy: username })
      setTitle('')
      setDescription('')
      setOpen(false)
      setSnackbar({ open: true, message: 'Ticket submitted successfully!', severity: 'success' })
      fetchTickets()
    } catch {
      setSnackbar({ open: true, message: 'Failed to submit ticket.', severity: 'error' })
    }
  }

  return (
    <SidebarLayout>
      <div className="flex justify-between items-center mb-4">
        <Typography variant="h5" fontWeight="bold">Your Tickets</Typography>
        <Button variant="contained" color="primary" onClick={() => setOpen(true)}>
          Submit New Ticket
        </Button>
      </div>

      <TicketStats tickets={tickets} />
      <TicketFilters
        status={statusFilter}
        priority={priorityFilter}
        onStatusChange={setStatusFilter}
        onPriorityChange={setPriorityFilter}
      />

      <div className="grid gap-4">
        {filtered.map(ticket => (
          <Card key={ticket.id} variant="outlined">
            <CardContent>
              <Typography variant="h6">{ticket.title}</Typography>
              <Typography variant="body2" sx={{ my: 1 }}>
                Status: <Chip label={ticket.status} size="small" color={ticket.status === 'Closed' ? 'success' : 'warning'} />
              </Typography>
              <Typography variant="body2">
                Priority: {ticket.priority || 'None'}
              </Typography>
              <Typography variant="body2">
                Assigned to: {ticket.assignedTo || 'Unassigned'}
              </Typography>
            </CardContent>
          </Card>
        ))}
      </div>

      <Dialog open={open} onClose={() => setOpen(false)}>
        <DialogTitle>Submit a Ticket</DialogTitle>
        <DialogContent sx={{ pt: 2 }}>
          <TextField
            label="Title"
            fullWidth
            margin="normal"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
          <TextField
            label="Description"
            fullWidth
            multiline
            rows={4}
            margin="normal"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setOpen(false)}>Cancel</Button>
          <Button onClick={handleSubmit} variant="contained">Submit</Button>
        </DialogActions>
      </Dialog>

      <Snackbar
        open={snackbar.open}
        autoHideDuration={3000}
        onClose={() => setSnackbar({ ...snackbar, open: false })}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'right' }}
      >
        <Alert severity={snackbar.severity} sx={{ width: '100%' }}>
          {snackbar.message}
        </Alert>
      </Snackbar>
    </SidebarLayout>
  )
}