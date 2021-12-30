import { AppState } from "../models/app-state.interface";
import { AuthState } from "../models/auth/auth-state.interface";

export type ReducerActionType = 
    | 'LOG_IN'
    | 'LOG_OUT'

export interface ReducerAction {
    type: ReducerActionType;
    payload?: any;
}

const Reducer = (state: AppState, action: ReducerAction) => {
    switch (action.type) {
        case 'LOG_IN':
            const newState: AppState = {
                ...state,
                authState: action.payload as AuthState
            };
            return newState;
        default:
            return state;
    }
};

export default Reducer;