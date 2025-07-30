import { Navigate } from 'react-router-dom';

function isAuthenticated(): boolean {
  const token = localStorage.getItem('token');
  if (!token) return false;
  try {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return !!payload?.email;
  } catch {
    return false;
  }
}

export default function ProtectedRoute({ children }: { children: JSX.Element }) {
  return isAuthenticated() ? children : <Navigate to="/" />;
}
