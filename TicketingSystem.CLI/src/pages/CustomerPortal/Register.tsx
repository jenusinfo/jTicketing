import { useState } from 'react';
import axios from '../../api/axiosInstance';

export default function Register() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [orgName, setOrgName] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await axios.post('/auth/register', { email, password, orgName });
  };

  return (
    <form onSubmit={handleSubmit} className="max-w-md mx-auto mt-12">
      <h1 className="text-2xl font-bold mb-4">Register</h1>
      <input type="text" value={orgName} onChange={e => setOrgName(e.target.value)} placeholder="Organization Name" className="border p-2 mb-2 w-full" />
      <input type="email" value={email} onChange={e => setEmail(e.target.value)} placeholder="Email" className="border p-2 mb-2 w-full" />
      <input type="password" value={password} onChange={e => setPassword(e.target.value)} placeholder="Password" className="border p-2 mb-4 w-full" />
      <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">Register</button>
    </form>
  );
}
