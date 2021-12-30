import { User } from "../user/user.model";

export interface AuthState {
    currentUser: User;
    isAuthenticated: boolean;
    token: string;
}