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
    } as AuthState
};

// This wont work since nextjs is SSR, need to store state in an API or something.
// const appStateString = 'APP_STATE';
// // const cachService = new CacheService();

// if (cachService.isCached(appStateString)) {
//     const state: string | null = cachService.getCacheData(appStateString);
    
//     if (state) {
//         const cachedAppContextState: AppState = JSON.parse(state);
//         InitialAppContextState = cachedAppContextState;
//     }
// }

const AppContext =
    createContext<[appState: AppState, dispatch: Dispatch<ReducerAction>]>([
        InitialAppContextState,
        () => null
    ]);

export default AppContext;