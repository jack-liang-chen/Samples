import React, {useState} from 'react'
import axios from 'axios'

function Login({ setAuthTokens }) {
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const res = await axios.post('http://localhost:5027/api/auth/login2', { username, password })
      console.log(res.data)
      setAuthTokens(res.data)
    } catch (error) {
      console.log('Login failed', error)
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <input 
        type='text'
        placeholder='Username'
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        />
        <input 
        type='text'
        placeholder='Password'
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        />
        <button type='submit'>Login</button>
    </form>
  )
}

export default Login