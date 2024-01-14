import React, { useState, useEffect } from 'react';

const TestBug = (props) => {
    const [state, setState] = useState(props);

    useEffect(() => {
        setState(props);
    }, [props]);

    return (
        <a href={state.testBug.link}>
            <img className="icon" src={process.env.PUBLIC_URL + '/bug.png'}/>
        </a>
    )
}

export default TestBug;