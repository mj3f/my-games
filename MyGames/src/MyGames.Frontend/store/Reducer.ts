import { AppState } from "../models/app-state.interface";
import { AuthState } from "../models/auth/auth-state.interface";
import { Game } from "../models/game/game.model";
import { CacheService } from "../services/CacheService";

export type ReducerActionType = 
    | 'LOG_IN'
    | 'LOG_OUT'
    | 'UPDATE_GAME'
    | 'REMOVE_GAME'

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
            return newState;
        case 'UPDATE_GAME':
            newState = {
                ...state,
                updateGame: action.payload as Game
            }
            return newState;
        case 'REMOVE_GAME':
            newState = {
                ...state,
                gameIdToRemove: action.payload as string
            };
            return newState;
        default:
            return state;
    }
};

export default Reducer;