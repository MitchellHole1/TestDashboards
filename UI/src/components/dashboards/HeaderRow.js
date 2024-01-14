import React, { Component, useState, useEffect } from 'react';
import { Table } from 'reactstrap';

const HeaderRow = (props) => {
  const [testRuns, setTestRuns] = useState(props.testRuns);
  const [type, setType] = useState("");

  useEffect(() => {
      if (props.testRuns[0]) {
        setTestRuns(props.testRuns);
      }
    }, [props]);


  return (
    <thead>
      <tr>
        <th>{type}</th>

        {testRuns.map(( listValue, index ) => {
          return (
            <th key={index}>
                <div><a href={"http://www." + listValue.link} target={"_blank"}>{listValue.build}</a></div>
                <div style={{fontSize: 10 + "px"}}>{listValue.duration}s</div>
                <div style={{fontSize: 10 + "px"}}>{listValue.created}</div>
            </th>
          );
        })}
      </tr>
    </thead>
  )
};

export default HeaderRow;
