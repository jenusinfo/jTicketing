import { useState, useEffect } from 'react'
import axios from 'axios'
import jwt_decode from 'jwt-decode'

export function useAuth() {
  const [user, setUser] = useState(() => {
    const token = localStorage.getItem('token')
    const refreshToken = localStorage.getItem('refreshToken')
    const role = localStorage.getItem('role')
    const username = localStorage.getItem('username')
    return token ? { token, refreshToken, role, username } : null
  })

  const isTokenExpired = (token) => {
    try {
      const decoded = jwt_decode(token)
      return decoded.exp * 1000 < Date.now()
    } catch {
      return true
    }
  }

  const refresh = async () => {
    try {
      const res = await axios.post('/api/refresh', {
        refreshToken: localStorage.getItem('refreshToken'),
      })
      localStorage.setItem('token', res.data.token)
      setUser((u) => ({ ...u, token: res.data.token }))
      return res.data.token
    } catch {
      logout()
      return null
    }
  }

  const login = async (username, password) => {
    const res = await axios.post('/api/login', { username, password })
    const { token, refreshToken, role } = res.data
    localStorage.setItem('token', token)
    localStorage.setItem('refreshToken', refreshToken)
    localStorage.setItem('role', role)
    localStorage.setItem('username', username)
    setUser({ token, refreshToken, role, username })
  }

  const logout = () => {
    localStorage.clear()
    setUser(null)
  }

  const getValidToken = async () => {
    if (!user?.token) return null
    if (isTokenExpired(user.token)) {
      return await refresh()
    }
    return user.token
  }

  const role = user?.role || null
  const token = user?.token || null
  const username = user?.username || null

  return { user, role, token, username, login, logout, getValidToken }
}