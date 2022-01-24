import axios from "axios";
import { Game } from "../models/game/game.model";
import { BaseService } from "./base.service";
import {IgdbGame} from "../models/game/igdb-game.model";


export class GamesService extends BaseService {
    public async searchForGame(name: string): Promise<IgdbGame[]> {
        return await axios.get<IgdbGame[]>(`${this.apiUrl}/games?name=${name}`)
            .then(res => res.data);
    }
}
