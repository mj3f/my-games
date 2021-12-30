import { AppState } from "../models/app-state.interface";
import { AuthState } from "../models/auth/auth-state.interface";
import { User } from "../models/user/user.model";

export interface ReducerAction {
    type: string;
    payload?: any;
}

const Reducer = (state: AppState, action: ReducerAction) => {
    switch (action.type) {
        case 'LOG_IN':
            const newState: AppState = {
                ...state,
                authState: action.payload as AuthState
            };
            console.log('222222222222')
            return newState;
        default:
            return state;
    }
};

export default Reducer;