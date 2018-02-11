import * as React from "react";
import { connect } from "react-redux";
import { Card, CardTitle, CardText } from "react-toolbox/lib/card";
import { List, ListItem } from "react-toolbox/lib/list";
import Person from "../../store/Person";
import { ApplicationState } from "../../store/ApplicationState";

interface LastPersonAddedProps {
  person: Person;
}

interface LastPersonAddedState {}

class LastPersonAdded extends React.Component<
  LastPersonAddedProps,
  LastPersonAddedState
> {
  public render(): JSX.Element {
    let { person } = this.props;
    console.log(person);
    return (
      <Card style={{height: '100%'}}>
        <CardTitle>Last Person Added</CardTitle>
        <CardText>
          <List>
            {person && person.firstName ? (
              <ListItem
                caption={`${this.props.person.firstName} ${this.props.person.lastName}`}
                leftIcon="person"
              />
            ) : null}
          </List>
        </CardText>
      </Card>
    );
  }
}

export default connect<LastPersonAddedProps, {}, {}>(
  (state: ApplicationState) => ({
    person: state.person
  }),
  {
    // Map dispatch to props
  }
)(LastPersonAdded);
