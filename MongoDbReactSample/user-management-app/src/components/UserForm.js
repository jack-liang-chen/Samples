import React, { useState, useEffect } from "react";
import {
  TextField,
  Button,
  Container,
  Typography,
  Grid,
  Paper,
} from "@mui/material";

const UserForm = ({ user, onSave }) => {
  const [formData, setFormData] = useState({
    id: "",
    name: "",
    age: "",
    addresses: [
      {
        state: "",
        country: "",
      },
    ],
  });

  useEffect(() => {
    if (user) {
      setFormData(user);
    } else {
      setFormData({
        id: "",
        name: "",
        age: "",
        addresses: [
          {
            state: "",
            country: "",
          },
        ],
      });
    }
  }, [user]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevState) => ({
      ...prevState,
      [name]: value,
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
    setFormData((prevState) => ({
      ...prevState,
      addresses: newAddresses,
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSave(formData);
  };

  const handleAddAddress = () => {
    setFormData((prevState) => ({
      ...prevState,
      addresses: [
        ...prevState.addresses,
        {
          state: "",
          country: "",
        },
      ],
    }));
  };

  return (
    <Container>
      <Paper style={{ padding: 16, marginBottom: 16 }}>
        <Typography variant="h6">
          {formData.id ? "Edit User" : "Create User"}
        </Typography>
        <form onSubmit={handleSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <TextField
                name="name"
                label="Name"
                fullWidth
                value={formData.name}
                onChange={handleChange}
              />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                name="age"
                label="Age"
                type="number"
                fullWidth
                value={formData.age}
                onChange={handleChange}
              />
            </Grid>
            <Grid item xs={12}>
              <Typography variant="subtitle1">Addresses</Typography>
              {formData.addresses.map((address, index) => (
                <Grid container spacing={2} key={index}>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      name="state"
                      label="State"
                      fullWidth
                      value={address.state}
                      onChange={(e) => handleAddressChange(e, index)}
                    />
                  </Grid>
                  <Grid item xs={12} sm={6}>
                    <TextField
                      name="country"
                      label="Country"
                      fullWidth
                      value={address.country}
                      onChange={(e) => handleAddressChange(e, index)}
                    />
                  </Grid>
                </Grid>
              ))}
              <Button
                type="button"
                variant="outlined"
                color="primary"
                onClick={handleAddAddress}
                style={{ marginTop: 16 }}
              >
                Address
              </Button>
            </Grid>
            <Grid item xs={12}>
              <Button type="submit" variant="contained" color="primary">
                Save
              </Button>
            </Grid>
          </Grid>
        </form>
      </Paper>
    </Container>
  );
};

export default UserForm;
