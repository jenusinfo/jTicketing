import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from '../api/axiosInstance';

export default function TicketDetail() {
  const { id } = useParams();
  const [ticket, setTicket] = useState<any>(null);
  const [comments, setComments] = useState([]);
  const [message, setMessage] = useState('');

  const loadTicket = async () => {
    const res = await axios.get(`/tickets/${id}`);
    setTicket(res.data);
  };

  const loadComments = async () => {
    const res = await axios.get(`/comments/${id}`);
    setComments(res.data);
  };

  const addComment = async () => {
    if (!message.trim()) return;
    await axios.post(`/comments/${id}`, JSON.stringify(message), {
      headers: { 'Content-Type': 'application/json' }
    });
    setMessage('');
    loadComments();
  };

  useEffect(() => {
    loadTicket();
    loadComments();
  }, [id]);

  if (!ticket) return <p className="p-4">Loading...</p>;

  return (
    <div className="p-6 max-w-2xl mx-auto">
      <h2 className="text-xl font-bold mb-2">Ticket: {ticket.title}</h2>
      <p className="mb-2">{ticket.description}</p>
      <p className="text-sm text-gray-500">Status: {ticket.status}, Priority: {ticket.priority}</p>

      <h3 className="mt-6 font-bold text-lg">Comments</h3>
      <ul className="mb-4">
        {comments.map((c: any) => (
          <li key={c.id} className="border-b py-2">
            <div className="text-sm text-gray-700">{c.message}</div>
            <div className="text-xs text-gray-400">by {c.author} @ {new Date(c.createdAt).toLocaleString()}</div>
          </li>
        ))}
      </ul>

      <textarea value={message} onChange={e => setMessage(e.target.value)} placeholder="Add a comment..." className="w-full border p-2 mb-2"></textarea>
      <button onClick={addComment} className="bg-blue-600 text-white px-4 py-2 rounded">Submit</button>
    </div>
  );
}
