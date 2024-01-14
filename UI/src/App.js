import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import classnames from 'classnames';


import {
  Container,
  Nav,
  NavLink,
  NavItem,
  TabContent,
  TabPane
} from 'reactstrap';

import Header from './components/Header'
import TestTable from './components/dashboards/TestTable'


function App() {

  const [value, setValue] = useState('');
  const [activeTab, setActiveTab] = useState('1');
  const [testTypeState, setTestTypeState] = useState([]);

  const toggle = tab => {
    if(activeTab !== tab) setActiveTab(tab);
  }

  useEffect(() => {
    document.title = "Test Dashboards";
  });

  useEffect(function getTestTypes() {
    const testTypes = fetch('/api/testtype');
    Promise.all([ testTypes ]).then((responses) => {
      var testTypes = responses[0].json();
      Promise.all([ testTypes ]).then((data) => {
        setTestTypeState(data[0]);
      });
    })
  }, [value]);

  return (
    <div className="App">
      <Container fluid className="centered">
        <Header />
        <Nav className="testType" tabs>
          {testTypeState.map(( listValue, index ) => {
            return(
            <NavItem key={index}>
              <NavLink href="#" className={classnames({active: activeTab === listValue.id.toString()})}
                       onClick={() => {
                         toggle(listValue.id.toString());
                       }}>{listValue.name}</NavLink>
            </NavItem>
            )
          })}
        </Nav>
        <TabContent activeTab={activeTab}>
          {testTypeState.map(( listValue, index ) => {
            return (
                <TabPane tabId={listValue.id.toString()}>
                  <TestTable testType={listValue.name}/>
                </TabPane>
            )
          })}
        </TabContent>
      </Container>
    </div>
  );
}

export default App;
