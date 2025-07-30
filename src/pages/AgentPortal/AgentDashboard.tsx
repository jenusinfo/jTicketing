import { useEffect, useState } from 'react';
import axios from '../../api/axiosInstance';
import { useNavigate } from 'react-router-dom';

export default function AgentDashboard() {
  const [tickets, setTickets] = useState([]);
  const navigate = useNavigate();

  const fetchTickets = async () => {
    const res = await axios.get('/tickets');
    setTickets(res.data);
  };

  useEffect(() => {
    fetchTickets();
  }, []);

  const assignTicket = async (id: number) => {
    await axios.post(`/tickets/${id}/assign`);
    fetchTickets();
  };

  const changeStatus = async (id: number, status: string) => {
    await axios.post(`/tickets/${id}/status`, null, { params: { status } });
    fetchTickets();
  };

  return (
    <div className="p-6">
      <h1 className="text-xl font-bold mb-4">Agent Dashboard</h1>
      <ul className="space-y-4">
        {tickets.map((t: any) => (
          <li key={t.id} className="border p-4 rounded shadow-sm">
            <b>{t.title}</b> — {t.status} ({t.priority})
            <div className="mt-2 space-x-2">
              {!t.assignedToId && (
                <button className="bg-green-600 text-white px-2 py-1 rounded" onClick={() => assignTicket(t.id)}>
                  Assign to Me
                </button>
              )}
              {t.assignedToId && (
                <>
                  <button className="bg-blue-500 text-white px-2 py-1 rounded" onClick={() => changeStatus(t.id, 'In Progress')}>
                    Mark In Progress
                  </button>
                  <button className="bg-gray-700 text-white px-2 py-1 rounded" onClick={() => changeStatus(t.id, 'Resolved')}>
                    Mark Resolved
                  </button>
                </>
              )}
              <button className="bg-yellow-600 text-white px-2 py-1 rounded" onClick={() => navigate(`/ticket/${t.id}`)}>
                View Details
              </button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}
