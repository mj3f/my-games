import { Game } from "../game/game.model";

export class User {
    
    constructor(
        public username: string,
        public games: Game[]) {}
}