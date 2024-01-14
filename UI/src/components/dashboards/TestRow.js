import React, { Component, useState, useEffect } from 'react';
import TestCell from './TestCell'

const TestRow = (props) => {
  const [testResultState, setTestResultState] = useState(props.testResults);
  const [testCaseState, setTestCaseState] = useState(props.testCases);

    useEffect(function setState() {
      let i = 0;
      while (i < props.testRuns.length) {
          if (props.testResults[i] && (props.testRuns[i].id != props.testResults[i].testRun.id)) {
              props.testResults.splice(i,0, undefined)
          }
          i++;
      }
      setTestResultState(props.testResults);
      setTestCaseState(props.testCases);
  }, [props]);

  return (
    <tr>
      <td>{testCaseState.testName}</td>
      {testResultState.map(( listValue, index ) => {
        return (
            <TestCell toggle={props.toggle} setTestResult={props.setTestResult} key={index} testResult={listValue}></TestCell>
        );
      })}
    </tr>
  );
};

export default TestRow;
