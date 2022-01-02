import { GameStatus } from "src/games/schemas/game.schema";

export class AddGameDto {
    constructor(
        public username: string,
        public gameName: string,
        public gameStatus: GameStatus,
        public gameIgdbId: string) {}
}