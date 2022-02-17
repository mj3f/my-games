import { createContext, Dispatch } from "react";
import { AppState } from "../models/app-state.interface";
import { AuthState } from "../models/auth/auth-state.interface";
import { CacheService } from "../services/CacheService";
import { ReducerAction } from "../store/Reducer";

export const InitialAppContextState: AppState = {
    authState: {
        isAuthenticated: false,
        currentUser: undefined,
        token: ''
    } as AuthState,
    updateGame: undefined
};

const AppContext =
    createContext<[appState: AppState, dispatch: Dispatch<ReducerAction>]>([
        InitialAppContextState,
        () => null
    ]);

export default AppContext;