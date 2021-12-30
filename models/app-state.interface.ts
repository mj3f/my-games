import { AuthState } from "./auth/auth-state.interface";

export interface AppState {
    authState: AuthState;
}