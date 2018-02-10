import * as React from "react";
import { connect } from "react-redux";
import { Card, CardTitle, CardText, CardActions } from "react-toolbox/lib/card";
import { Button } from "react-toolbox/lib/button";
import { Input } from "react-toolbox/lib/input";

interface AddPersonProps {}

interface AddPersonState {}

class AddPerson extends React.Component<AddPersonProps, AddPersonState> {
  public render(): JSX.Element {
    return (
      <Card>
        <CardTitle />
        <CardText>
          <Input label="First Name" floating />
          <Input label="Last Name" floating />
          <CardActions>
            <Button />
          </CardActions>
        </CardText>
      </Card>
    );
  }
}

export default connect(
  state => ({
    // Map state to props
  }),
  {
    // Map dispatch to props
  }
)(AddPerson);
