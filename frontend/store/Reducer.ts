import { AppState } from "../models/app-state.interface";
import { AuthState } from "../models/auth/auth-state.interface";
import { CacheService } from "../services/CacheService";

export type ReducerActionType = 
    | 'LOG_IN'
    | 'LOG_OUT'

export interface ReducerAction {
    type: ReducerActionType;
    payload?: any;
}

const Reducer = (state: AppState, action: ReducerAction) => {
    // @ts-ignore
    let newState: AppState = null;

    switch (action.type) {
        case 'LOG_IN':
            newState = {
                ...state,
                authState: action.payload as AuthState
            };
            cacheState(newState);
            return newState;
        case 'LOG_OUT':
            newState = { // AppState
                ...state,
                authState: {
                    isAuthenticated: false,
                    currentUser: undefined,
                    token: ''
                }
            };
            cacheState(newState);
            return newState;
        default:
            return state;
    }
};

const cacheState = (state: AppState) => {
    // const cacheService = new CacheService();
    // cacheService.setCacheData('APP_STATE', JSON.stringify(state));
};

export default Reducer;