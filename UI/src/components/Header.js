import React, { Component } from 'react';
import { Navbar, Container } from "react-bootstrap";

const Header = () => {
  return (
    <Navbar bg="dark" data-bs-theme="dark">
      <Container>
        <Navbar.Brand href="/">Orca Test Dashboards</Navbar.Brand>
      </Container>
    </Navbar>
  )
};

export default Header;
