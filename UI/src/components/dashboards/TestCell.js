import React, { useState, useEffect } from 'react';
import TestBug from './TestBug'


const TestCell = (props) => {
    const [state, setState] = useState(props);
    const resultOptions = ["Fail", "Pass"];

    const displayTableCell = () => {
        if (state.testResult) {
            return <td className={state.testResult.passed ? resultOptions[1] : resultOptions[0]} onClick={() => {
                props.toggle();
                props.setTestResult(state.testResult.id);
            }}>
                {state.testResult.testResultBugs.map(d => (<TestBug key={d.id} testBug={d.testBug}></TestBug>))}
                {state.testResult.duration.toFixed(3)}s
            </td>
        } else {
            return <td></td>
        }
    }

    useEffect(() => {
        setState(props);
    }, [props]);

    return (
        displayTableCell()
    )
}

export default TestCell;