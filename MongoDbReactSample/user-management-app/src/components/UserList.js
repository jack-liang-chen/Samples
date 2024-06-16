import React, { useState, useEffect } from "react";
import UserService from "../api/UserService";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  Button,
  Container,
  Typography,
} from "@mui/material";
import UserForm from "./UserForm";

const UserList = () => {
  const [users, setUsers] = useState([]);
  const [selectedUser, setSelectedUser] = useState(null);

  useEffect(() => {
    loadUsers();
  }, []);

  const loadUsers = () => {
    UserService.getUsers()
      .then((response) => {
        setUsers(response.data);
      })
      .catch((error) => {
        console.error("There was an error retrieving the user list!", error);
      });
  };

  const handleEditUser = (user) => {
    setSelectedUser(user);
  };

  const handleDeleteUser = (id) => {
    UserService.deleteUser(id)
      .then(() => {
        loadUsers();
      })
      .catch((error) => {
        console.error("There was an error deleting the user!", error);
      });
  };

  const handleSaveUser = (user) => {
    if (user.id) {
      UserService.updateUser(user.id, user)
        .then(() => {
          loadUsers();
          setSelectedUser(null);
        })
        .catch((error) => {
          console.error("There was an error updating the user!", error);
        });
    } else {
      UserService.createUser(user)
        .then(() => {
          loadUsers();
          setSelectedUser(null);
        })
        .catch((error) => {
          console.error("There was an error creating the user!", error);
        });
    }
  };

  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        User Management
      </Typography>
      <UserForm user={selectedUser} onSave={handleSaveUser} />
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Name</TableCell>
              <TableCell>Age</TableCell>
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {users.map((user) => (
              <TableRow key={user.id}>
                <TableCell>{user.name}</TableCell>
                <TableCell>{user.age}</TableCell>
                <TableCell>
                  <Button
                    variant="contained"
                    color="primary"
                    onClick={() => handleEditUser(user)}
                  >
                    Edit
                  </Button>
                  <Button
                    variant="contained"
                    color="secondary"
                    onClick={() => handleDeleteUser(user.id)}
                  >
                    Delete
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </Container>
  );
};

export default UserList;
