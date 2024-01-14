import React, {useState} from "react";
import { Modal, Button, Accordion, Form, Table } from "react-bootstrap";

const TestModal = (props) => {

    const [inputs, setInputs] = useState({isPassed: false});
    const [testResultBugs, setTestResultBugs] = useState([]);
    const [testResultError, setTestResultError] = useState([]);
    const [testBugs, setTestBugs] = useState([]);

    const getTestBug = () => {
        fetch("/api/testbug/", {
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            method: "GET",
        }).then(function (response) {
            return response.json();
        }).then(function (data) {
            console.log(data);
            setTestBugs(data)
        });
    }

    const getTestResult = () => {
        fetch("/api/testresult/" + props.testResultId, {
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            method: "GET",
        }).then(function (response) {
            return response.json();
        }).then(function (data) {
            console.log(data);
            setTestResultBugs(data.testResultBugs)
            setTestResultError(data.errorMessage)
        });
    };

    const updateTestResult = () => {
        fetch("/api/testresult/" + props.testResultId, {
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            method: "PUT",

            // Fields that to be updated are passed
            body: JSON.stringify({
                duration: inputs.duration,
                passed: inputs.isPassed
            })
        }).then(function (response) {
            props.toggle()
            return response.json();
            }).then(function (data) {
                console.log(data);
            });
    };

    const addTestBug = () => {
        fetch("/api/testresult/" + props.testResultId + "/testbug", {
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            method: "POST",

            // Fields that to be updated are passed
            body: JSON.stringify({
                testBugId: inputs.bugId
            })
        }).then(function (response) {
            props.toggle()
            return response.json();
        }).then(function (data) {
            console.log(data);
        });
    };

    const removeTestBug = (id) => {
        fetch("/api/testresult/" + props.testResultId + "/testbug/" + id, {
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            method: "DELETE",
        }).then(function (response) {
            props.toggle()
        });
    };

    const deleteTestResult = () => {
        fetch("/api/testresult/" + props.testResultId + "/", {
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json"
            },
            method: "DELETE"
        }).then(function (response) {
            console.log(response);
            props.toggle()
        });
    };

    const handleCheckChange = (event) => {
        const name = event.target.name;
        const value = event.target.checked;
        setInputs(values => ({...values, [name]: value}))
        console.log(value)
    }

    const handleValueChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;
        setInputs(values => ({...values, [name]: value}))
    }

    return(
        <Modal show={props.modal} onHide={props.toggle}>
            <Modal.Header closeButton>
            <Modal.Title>Test Result</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Accordion>
                    <Accordion.Item eventKey="0">
                        <Accordion.Header>Edit</Accordion.Header>
                        <Accordion.Body>
                            <Form>
                                <Form.Check
                                    onChange={handleCheckChange} // prettier-ignore
                                    name="isPassed"
                                    type="switch"
                                    id="custom-switch"
                                    label="Passed"
                                />
                                <br/>
                                <Form.Group className="mb-3" controlId="formBasicDuration">
                                    <Form.Label>Duration</Form.Label>
                                    <Form.Control name="duration" type="number" placeholder="Enter duration" onChange={handleValueChange} />
                                    <Form.Text className="text-muted">
                                        Time in seconds...
                                    </Form.Text>
                                </Form.Group>
                            </Form>
                            <Button variant="primary" onClick={updateTestResult} disabled={!inputs.duration}>
                                Edit
                            </Button><br/>
                        </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="1" onClick={getTestBug}>
                        <Accordion.Header>Link a Bug</Accordion.Header>
                        <Accordion.Body>
                            <Form.Group className="mb-3" controlId="formBasicBug">
                                <Form.Label>Bug Id</Form.Label>
                                <Form.Select aria-label="Default select example" onChange={handleValueChange} name="bugId">
                                    <option>Select Bug</option>
                                    {testBugs.map(( listValue, index ) => {
                                        return (
                                            <option key={index} value={listValue.id}>{listValue.identifier}</option>
                                        )})}
                                </Form.Select>
                            </Form.Group>
                            <Button variant="primary" onClick={addTestBug} disabled={!inputs.bugId}>
                                Add Bug
                            </Button><br/>
                        </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="2" onClick={getTestResult} >
                        <Accordion.Header>Remove Bug Link</Accordion.Header>
                        <Accordion.Body>
                            <Table striped bordered hover>
                                <thead>
                                <tr>
                                    <th>Name</th>
                                    <th></th>
                                </tr>
                                </thead>
                                <tbody>
                                    {testResultBugs.map(( listValue, index ) => {
                                        return (
                                            <tr key={index}>
                                                <td>
                                                    <a href={listValue.testBug.link}>
                                                        {listValue.testBug.identifier}
                                                    </a>
                                                </td>
                                                <td>
                                                    <Button variant="danger" onClick={() => {
                                                        removeTestBug(listValue.id)
                                                    }}>
                                                        Remove Bug
                                                    </Button>
                                                </td>
                                            </tr>
                                        );
                                    })}
                                </tbody>
                            </Table>
                        </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="3">
                        <Accordion.Header>Delete</Accordion.Header>
                            <Accordion.Body>
                                <Form.Group className="mb-3" controlId="formBasicReason">
                                    <Form.Label>Reason</Form.Label>
                                    <Form.Control name="reason" type="text" placeholder="Enter reason" onChange={handleValueChange} />
                                    <Form.Text className="text-muted">
                                        Reason for deletion...
                                    </Form.Text>
                                </Form.Group>
                                <Button variant="danger" onClick={deleteTestResult} disabled={!inputs.reason}>
                                    Delete
                                </Button>
                            </Accordion.Body>
                    </Accordion.Item>
                    <Accordion.Item eventKey="4" onClick={getTestResult}>
                        <Accordion.Header>Error Message</Accordion.Header>
                        <Accordion.Body>
                            {testResultError}
                        </Accordion.Body>
                    </Accordion.Item>
                </Accordion>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={props.toggle}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    )
}

export default TestModal;