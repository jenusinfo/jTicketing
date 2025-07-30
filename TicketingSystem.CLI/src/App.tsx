import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './pages/CustomerPortal/Login';
import Register from './pages/CustomerPortal/Register';
import Dashboard from './pages/CustomerPortal/Dashboard';

export default function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/dashboard" element={<Dashboard />} />
      </Routes>
    </Router>
  );
}
