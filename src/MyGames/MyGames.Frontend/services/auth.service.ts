import axios from "axios";
import { BaseService } from "./base.service";

class LoginRequest {
    constructor(
        public username: string,
        public password: string) {}
}

export class AuthService extends BaseService {
    
    public async login(username: string, password: string): Promise<string> {
        return await axios.post(`${this.apiUrl}/auth`, new LoginRequest(username, password))
            .then(res => res.data);
    }
}