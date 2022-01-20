import axios from "axios";
import { User } from "../models/user/user.model";
import { BaseService } from "./base.service";

export class UsersService extends BaseService {

    public async getUser(): Promise<User> {
        return await await axios.get(`${this.apiUrl}/users/dummy`)
        .then(res => res.data);
    }
}