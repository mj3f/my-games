import { Game } from "../../../models/game/game.model";
import Image from "next/image";

export interface GameDetailProps {
    game: Game;
}

const GameDetailButton: React.FC = ({ children }) =>
    <button
        className="w-fit p-2 bg-gray-200 rounded hover:bg-gray-300 mr-2">
            {children}
    </button>;

const GameDetail: React.FC<GameDetailProps> = ({ game }) => {
    const image = game.coverArtUrl ? 
        <Image
            className="rounded"
            src={'https:' + game.coverArtUrl}
            alt="game"
            layout="fill"
            objectFit="cover"
            quality={100} /> :
        null;
    
    const moveToBacklogButton = <GameDetailButton>Move to Backlog</GameDetailButton>
    const moveToWishlistButton = <GameDetailButton>Move to Wishlist</GameDetailButton>
    const startProgressButton = <GameDetailButton>Start Progress</GameDetailButton>

    return (
        <div className="flex flex-row h-full w-full">
            <div className="w-32 h-32 relative">
                {image}
            </div>
            <div className="flex flex-col pl-2 w-full">
                <div className="flex flex-row">
                    <p>Status in your library: <span className="font-semibold">{game.gameStatus}</span></p>
                    <div className="flex pl-2 w-full justify-end">
                        {moveToBacklogButton}
                        {moveToWishlistButton}
                        {startProgressButton}
                    </div>
                </div>
                <p>Your notes about this game:</p>
            </div>
        </div>
    );
};

export default GameDetail;