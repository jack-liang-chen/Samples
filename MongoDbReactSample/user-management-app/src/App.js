import React from "react";
import { CssBaseline, Container } from "@mui/material";
import UserList from "./components/UserList";

function App() {
  return (
    <React.Fragment>
      <CssBaseline />
      <Container maxWidth="lg">
        <UserList />
      </Container>
    </React.Fragment>
  );
}

export default App;
