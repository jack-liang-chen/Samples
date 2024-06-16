import './App.css'
import React, {useState} from 'react'
import Login from './Login'
import Protected from './Protected'

function App() {
  const [authTokens, setAuthTokens] = useState(null)

  const handleLogout = () => {
    setAuthTokens(null)
  }

  return (
    <div>
      {
        !authTokens ? (
          <Login setAuthTokens={setAuthTokens} />
        ) : (
          <>
            <Protected authTokens={authTokens} />
            <button onClick={handleLogout}>Logout</button>
          </>
        )
      }
    </div>
  );
}

export default App
