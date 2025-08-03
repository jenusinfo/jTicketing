import React, { useEffect, useState } from 'react'
import {
  Card,
  CardContent,
  Typography,
  Chip,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Avatar,
  Snackbar,
  Alert
} from '@mui/material'
import { AssignmentInd, Update } from '@mui/icons-material'
import SidebarLayout from '../../layouts/SidebarLayout'
import { getTickets, updateTicket } from '../../api/tickets'
import TicketStats from '../../components/TicketStats'
import TicketFilters from '../../components/TicketFilters'

export default function AgentDashboard() {
  const [tickets, setTickets] = useState([])
  const [filtered, setFiltered] = useState([])
  const [selectedTicket, setSelectedTicket] = useState(null)
  const [dialogType, setDialogType] = useState(null)
  const [inputValue, setInputValue] = useState('')
  const [statusFilter, setStatusFilter] = useState('')
  const [priorityFilter, setPriorityFilter] = useState('')
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' })

  const fetchTickets = async () => {
    const res = await getTickets()
    setTickets(res.data)
  }

  useEffect(() => {
    fetchTickets()
  }, [])

  useEffect(() => {
    let result = [...tickets]
    if (statusFilter) result = result.filter(t => t.status === statusFilter)
    if (priorityFilter) result = result.filter(t => t.priority === priorityFilter)
    setFiltered(result)
  }, [tickets, statusFilter, priorityFilter])

  const openDialog = (ticket, type) => {
    setSelectedTicket(ticket)
    setDialogType(type)
    setInputValue('')
  }

  const closeDialog = () => {
    setSelectedTicket(null)
    setDialogType(null)
    setInputValue('')
  }

  const handleConfirm = async () => {
    try {
      if (dialogType === 'assign') {
        await updateTicket(selectedTicket.id, { assignedTo: inputValue })
        setSnackbar({ open: true, message: 'Ticket assigned.', severity: 'success' })
      } else if (dialogType === 'status') {
        await updateTicket(selectedTicket.id, { status: inputValue })
        setSnackbar({ open: true, message: 'Status updated.', severity: 'success' })
      }
      closeDialog()
      fetchTickets()
    } catch (err) {
      setSnackbar({ open: true, message: 'Failed to update ticket.', severity: 'error' })
    }
  }

  return (
    <SidebarLayout>
      <Typography variant="h5" fontWeight="bold" className="mb-4">All Tickets</Typography>
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
              <div className="flex justify-between items-center mb-2">
                <Typography variant="h6">{ticket.title}</Typography>
                <Avatar>{(ticket.assignedTo || 'U')[0]}</Avatar>
              </div>
              <Typography variant="body2" sx={{ mb: 1 }}>
                Status: <Chip label={ticket.status} size="small" color={ticket.status === 'Closed' ? 'success' : 'warning'} />
              </Typography>
              <Typography variant="body2">
                Priority: {ticket.priority || 'None'}
              </Typography>
              <Typography variant="body2">
                Assigned to: {ticket.assignedTo || 'Unassigned'}
              </Typography>
              <div className="flex gap-2 mt-3">
                <Button
                  startIcon={<AssignmentInd />}
                  variant="outlined"
                  size="small"
                  onClick={() => openDialog(ticket, 'assign')}
                >
                  Assign
                </Button>
                <Button
                  startIcon={<Update />}
                  variant="outlined"
                  size="small"
                  onClick={() => openDialog(ticket, 'status')}
                >
                  Change Status
                </Button>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>

      <Dialog open={!!selectedTicket} onClose={closeDialog}>
        <DialogTitle>{dialogType === 'assign' ? 'Assign Ticket' : 'Change Status'}</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            fullWidth
            label={dialogType === 'assign' ? 'Agent Name' : 'New Status'}
            value={inputValue}
            onChange={(e) => setInputValue(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={closeDialog}>Cancel</Button>
          <Button onClick={handleConfirm} variant="contained">Save</Button>
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