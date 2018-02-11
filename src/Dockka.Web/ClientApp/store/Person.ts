import { Reducer } from 'redux';
import { createAction, handleActions, Action } from 'redux-actions';
import { Dispatch } from 'redux';
import * as s from '@aspnet/signalr-client';
import { ApplicationState } from './ApplicationState';
import { connection } from '../signalR';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface Person {
    firstName: string;
    lastName: string;
}

export default Person;

export const PERSON_CREATED = 'person/PERSON_CREATED';

export const personAdded = createAction<Person>(PERSON_CREATED);

export const createPerson = (person:Person) => (dispatch:Dispatch<any>) => {
    connection.send('addPerson', person);
}

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

export const reducer = handleActions<any>({
    [PERSON_CREATED]: (state: Person, action: Action<Person>) => {
        return action.payload
    }}, {firstName: '', lasteName: ''});
