import React, {useEffect, useState} from 'react'
import axios from 'axios'

function Protected({ authTokens }) {
  const [message, setMessage] = useState('')

  useEffect(() => {
    const fetchData =async () => {
      try {
        const res = await axios.get('http://localhost:5027/api/protected', { 
          headers: {
            Authorization: `Bearer ${authTokens.accessToken}`
          }
        })
        console.log(res.data)
        setMessage(res.data.message)
      } catch (error) {
        console.error('Failed to fetch protected data', error)
        setMessage('Unauthorized')
      }
    }
    fetchData()
  }, [authTokens])
  
  return (

    <div>Protected</div>
  )
}

export default Protected