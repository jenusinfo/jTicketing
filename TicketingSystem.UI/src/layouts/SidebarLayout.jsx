import React from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { useAuth } from '../auth/useAuth'
import ThemeToggle from '../components/ThemeToggle'

export default function SidebarLayout({ children }) {
  const { role, logout } = useAuth()
  const navigate = useNavigate()

  const handleLogout = () => {
    logout()
    navigate('/login')
  }

  return (
    <div className="flex h-screen bg-gray-50 dark:bg-gray-900 text-black dark:text-white">
      <aside className="w-64 bg-gray-800 dark:bg-gray-950 text-white p-4 flex flex-col">
        <h2 className="text-lg font-bold mb-6">jTicketing</h2>
        {role === 'agent' && (
          <>
            <Link to="/agent" className="mb-2 hover:underline">Agent Dashboard</Link>
          </>
        )}
        {role === 'customer' && (
          <>
            <Link to="/customer" className="mb-2 hover:underline">My Tickets</Link>
          </>
        )}
        <ThemeToggle />
        <div className="mt-auto">
          <button onClick={handleLogout} className="bg-red-500 px-3 py-1 rounded mt-4 w-full">
            Logout
          </button>
        </div>
      </aside>
      <main className="flex-1 p-6 overflow-auto">{children}</main>
    </div>
  )
}