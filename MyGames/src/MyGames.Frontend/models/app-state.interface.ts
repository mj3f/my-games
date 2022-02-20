import { AuthState } from "./auth/auth-state.interface";
import { Game } from "./game/game.model";

export interface AppState {
    authState: AuthState;
    updateGame: Game | undefined;
    gameIdToRemove: string;
}