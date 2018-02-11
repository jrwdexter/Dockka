import * as signalR from '@aspnet/signalr-client';
import { ApplicationState } from './store/ApplicationState';
import { Store } from 'redux';
import { personAdded, Person } from './store/Person';

// This connection string would have to be changed to run locally
// Also, this should be part of a configuration passed from somewhere else (to window, for example)
export const connection = new signalR.HubConnection('http://localhost:8001/persons');

export async function startSignalR(store: Store<ApplicationState>) {
  connection.on('personUpdate', (person: Person) => {
    store.dispatch(personAdded(person));
  });

  await connection.start();
  return connection;
}