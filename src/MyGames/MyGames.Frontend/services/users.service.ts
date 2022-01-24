import axios from "axios";
import { User } from "../models/user/user.model";
import { BaseService } from "./base.service";
import {IgdbGame} from "../models/game/igdb-game.model";

export class UsersService extends BaseService {

    public async getUser(): Promise<User> {
        return await axios.get(`${this.apiUrl}/users/dummy`)
            .then(res => res.data);
    }

    public async addGameToUsersLibrary(username: string, game: IgdbGame): Promise<string> {
        return await axios.put(`${this.apiUrl}/users/${username}/add-game`, game)
            .then(res => res.data);
    }
}
