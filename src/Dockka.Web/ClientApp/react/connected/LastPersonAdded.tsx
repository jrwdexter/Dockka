import * as React from 'react';
import { connect } from 'react-redux';

interface LastPersonAddedProps {};

interface LastPersonAddedState {};

class LastPersonAdded extends React.Component<LastPersonAddedProps, LastPersonAddedState> {
  public render(): JSX.Element {
    return (<span>LastPersonAdded</span>);
  }
}

export default connect(
  (state) => ({
    // Map state to props
  }),
  {
    // Map dispatch to props
  })(LastPersonAdded);
