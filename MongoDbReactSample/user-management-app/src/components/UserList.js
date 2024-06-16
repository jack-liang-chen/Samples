import React, { useState, useEffect } from 'react';
import UserService from '../api/UserService';
import UserForm from './UserForm';

const UserList = () => {
    const [users, setUsers] = useState([]);
    const [selectedUser, setSelectedUser] = useState(null);

    useEffect(() => {
        loadUsers();
    }, []);

    const loadUsers = () => {
        UserService.getUsers().then(response => {
            setUsers(response.data);
        }).catch(error => {
            console.error('There was an error retrieving the user list!', error);
        });
    };

    const handleEditUser = (user) => {
        setSelectedUser(user);
    };

    const handleDeleteUser = (id) => {
        UserService.deleteUser(id).then(() => {
            loadUsers();
        }).catch(error => {
            console.error('There was an error deleting the user!', error);
        });
    };

    const handleSaveUser = (user) => {
        if (user.id) {
            UserService.updateUser(user.id, user).then(() => {
                loadUsers();
                setSelectedUser(null);
            }).catch(error => {
                console.error('There was an error updating the user!', error);
            });
        } else {
            UserService.createUser(user).then(() => {
                loadUsers();
                setSelectedUser(null);
            }).catch(error => {
                console.error('There was an error creating the user!', error);
            });
        }
    };

    return (
        <div>
            <h1>User Management</h1>
            <UserForm user={selectedUser} onSave={handleSaveUser} />
            <table>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Age</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(user => (
                        <tr key={user.id}>
                            <td>{user.name}</td>
                            <td>{user.age}</td>
                            <td>
                                <button onClick={() => handleEditUser(user)}>Edit</button>
                                <button onClick={() => handleDeleteUser(user.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default UserList;
