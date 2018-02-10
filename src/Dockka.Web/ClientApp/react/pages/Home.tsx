import * as React from "react";
import { RouteComponentProps } from "react-router-dom";
import { Cell, Grid } from "../core/Grid";
import AddPerson from '../connected/AddPerson';
import LastPersonAdded from '../connected/LastPersonAdded';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
  public render(): JSX.Element {
    return (
      <Grid>
        <Cell col={6}>
            <AddPerson />
        </Cell>
        <Cell col={6} >
            <LastPersonAdded />
        </Cell>
      </Grid>
    );
  }
}

export default Home;
