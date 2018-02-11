import * as React from "react";
import { connect } from "react-redux";
import { Card, CardTitle, CardText, CardActions } from "react-toolbox/lib/card";
import { Button } from "react-toolbox/lib/button";
import { Input } from "react-toolbox/lib/input";
import Person from "../../store/Person";
import { ApplicationState } from "../../store/ApplicationState";
import { Dispatch } from "redux";
import { createPerson } from "../../store/Person";
import { FormEvent } from "react";

interface AddPersonProps {
  addPerson: (firstName: string, lastName: string) => any;
}

interface AddPersonState {
  firstName: string;
  lastName: string;
}

class AddPerson extends React.Component<AddPersonProps, AddPersonState> {
  constructor(props: AddPersonProps, context?: any) {
    super(props, context);
    this.state = {
      firstName: "",
      lastName: ""
    };
  }

  firstNameChanged(firstName: string) {
    this.setState({
      firstName: firstName
    });
  }

  lastNameChanged(lastName: string) {
    this.setState({
      lastName: lastName
    });
  }

  addPerson(e: FormEvent<any>) {
    this.props.addPerson(this.state.firstName, this.state.lastName);
    e.preventDefault();
  }

  public render(): JSX.Element {
    return (
      <form onSubmit={this.addPerson.bind(this)}>
        <Card>
          <CardTitle>Add Person</CardTitle>
          <CardText>
            <Input
              label="First Name"
              floating
              value={this.state.firstName}
              onChange={this.firstNameChanged.bind(this)}
            />
            <Input
              label="Last Name"
              floating
              value={this.state.lastName}
              onChange={this.lastNameChanged.bind(this)}
            />
          </CardText>
          <CardActions>
            <Button
              label="Add"
              type="submit"
              style={{ width: "100%" }}
              primary
              raised
              ripple
            />
          </CardActions>
        </Card>
      </form>
    );
  }
}

export default connect(
  (state: ApplicationState) => ({
    // Map state to props
  }),
  (dispatch: Dispatch<ApplicationState>) => ({
    addPerson: (firstName: string, lastName: string) =>
      dispatch(createPerson({ firstName: firstName, lastName: lastName }))
    // Map dispatch to props
  })
)(AddPerson);
