import axios from "axios";
import { Game } from "../models/game/game.model";
import { BaseService } from "./base.service";


export class GamesService extends BaseService {

    public async searchForGame(name: string): Promise<Game[]> {
        return await axios.get<Game[]>(`${this.apiUrl}/games?name=${name}`)
            .then(res => res.data);
    }

    // public async addGame(game: Game): Promise<string> {
    //     return await axios.post<string>()
    // }
}
