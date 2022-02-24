export class Game {
    constructor(
        public id: string,
        public igdbId: number,
        public name: string,
        public gameStatus: string,
        public coverArtUrl?: string) {}
}
