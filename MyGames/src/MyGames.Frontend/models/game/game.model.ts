import { GameNote } from "./game-note.model";

export class Game {
    constructor(
        public id: string,
        public igdbId: number,
        public name: string,
        public gameStatus: string,
        public notes: GameNote[],
        public coverArtUrl?: string) {}
}
