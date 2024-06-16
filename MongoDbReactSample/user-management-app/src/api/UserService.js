import axios from 'axios'

const API_URL = 'http://localhost:5022/api/users'

class UserService {
  getUsers() {
    return axios.get(API_URL)
  }

  getUser(id) {
    return axios.get(`${API_URL}/${id}`)
  }

  createUser(user) {
    return axios.post(API_URL, user)
  }

  updateUser(id, user) {
    return axios.put(`${API_URL}/${id}`, user)
  }

  deleteUser(id) {
    return axios.delete(`${API_URL}/${id}`)
  }
}

export default new UserService()