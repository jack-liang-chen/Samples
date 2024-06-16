import React, { useState, useEffect } from 'react';

const UserForm = ({ user, onSave }) => {
    const [formData, setFormData] = useState({
        id: '',
        name: '',
        age: '',
        addresses: [{
            state: '',
            country: ''
        }]
    });

    useEffect(() => {
        if (user) {
            setFormData(user);
        } else {
            setFormData({
                id: '',
                name: '',
                age: '',
                addresses: [{
                    state: '',
                    country: ''
                }]
            });
        }
    }, [user]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleAddressChange = (e, index) => {
        const { name, value } = e.target;
        const newAddresses = formData.addresses.map((address, i) => {
            if (i === index) {
                return { ...address, [name]: value };
            }
            return address;
        });
        setFormData(prevState => ({
            ...prevState,
            addresses: newAddresses
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onSave(formData);
    };

    const handleAddAddress = () => {
        setFormData(prevState => ({
            ...prevState,
            addresses: [...prevState.addresses, {
                state: '',
                country: ''
            }]
        }));
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>{formData.id ? 'Edit User' : 'Create User'}</h2>
            <div>
                <label>Name:</label>
                <input type="text" name="name" value={formData.name} onChange={handleChange} />
            </div>
            <div>
                <label>Age:</label>
                <input type="number" name="age" value={formData.age} onChange={handleChange} />
            </div>
            <div>
                <h3>Addresses</h3>
                {formData.addresses.map((address, index) => (
                    <div key={index}>
                        <div>
                            <label>State:</label>
                            <input type="text" name="state" value={address.state} onChange={(e) => handleAddressChange(e, index)} />
                        </div>
                        <div>
                            <label>Country:</label>
                            <input type="text" name="country" value={address.country} onChange={(e) => handleAddressChange(e, index)} />
                        </div>
                    </div>
                ))}
                <button type="button" onClick={handleAddAddress}>Add Address</button>
            </div>
            <button type="submit">Save</button>
        </form>
    );
};

export default UserForm;
