import { useEffect, useState } from 'react';
import { getUserInfo } from '../../api/user';
const user = getUserInfo();

import axios from '../../api/axiosInstance';

export default function Dashboard() {
  const [tickets, setTickets] = useState([]);

  useEffect(() => {
    axios.get('/tickets').then(res => setTickets(res.data));
  }, []);

  return (
    <div className="p-6">
      {user && (
  <div className="mb-4 text-sm text-right text-gray-600">
    Logged in as <b>{user.email}</b> (<span className="bg-gray-200 px-2 py-1 rounded">{user.role}</span>)
  </div>
)}

  <h1 className="text-xl font-bold mb-4">My Tickets</h1>
      <ul className="space-y-2">
        {tickets.map((t: any) => (
          <li key={t.id} className="border p-2 rounded shadow-sm">
            <b>{t.title}</b> — {t.status} ({t.priority})
          </li>
        ))}
      </ul>
    </div>
  );
}
