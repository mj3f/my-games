import { createContext, Dispatch } from "react";
import { AppState } from "../models/app-state.interface";
import { AuthState } from "../models/auth/auth-state.interface";

export const InitialAppContextState: AppState = {
    authState: {
        isAuthenticated: false,
        currentUser: undefined,
        token: ''
    } as AuthState
};

const AppContext =
    createContext<[appState: AppState, dispatch: Dispatch<any>]>([
        InitialAppContextState,
        () => null
    ]);

export default AppContext;