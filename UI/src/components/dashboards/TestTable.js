import React, { useState, useEffect } from 'react';
import { Table } from 'react-bootstrap';
import HeaderRow from './HeaderRow'
import TestRow from './TestRow'
import TestModal from '../TestModal'

const TestTable = (props) => {
  const [testCaseState, setTestCaseState] = useState([]);
  const [testRunState, setTestRunState] = useState([]);
  const [testResultState, setTestResultState] = useState([]);
  const [testModalState, setTestModalState] = useState(false);
  const [testModalIdState, setTestModalIdState] = useState(0);
  const [value, setValue] = useState(0); // integer state

  const toggleTestModal = () => {
    setTestModalState(prev => !prev)
    setValue(value => value + 1);
  };

  useEffect(function getTestCases() {
    const testCases = fetch('/api/testcase?testType=');
    const testRuns = fetch('/api/testrun?testType=' + props.testType);
    const testResults = fetch('/api/testresult');
    Promise.all([ testCases, testRuns, testResults ]).then((responses) => {
      var testCases = responses[0].json();
      var testRuns = responses[1].json();
      var testResults = responses[2].json();
      Promise.all([ testCases, testRuns, testResults ]).then((data) => {
        setTestCaseState(data[0]);
        setTestRunState(data[1]);
        setTestResultState(data[2]);
      });
    });
  },[value]);

  return (
    <Table>
      <HeaderRow testRuns={testRunState}/>
      <tbody>
          {testCaseState.map(( listValue, index ) => {
            return (
              <TestRow toggle={toggleTestModal} setTestResult={setTestModalIdState} key={index} testCases={listValue} testRuns={testRunState} testResults={testResultState.filter(obj => {return obj.testCase.id === listValue.id})} />
            );
          })}
      </tbody>
      <TestModal toggle={toggleTestModal} modal={testModalState} testResultId={testModalIdState}></TestModal>
    </Table>
  )
};

export default TestTable;
