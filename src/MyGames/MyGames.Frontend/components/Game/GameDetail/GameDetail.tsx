import { Game } from "../../../models/game/game.model";
import Image from "next/image";
import GameDetailButton from "./GameDetailButton";
import AppContext from "../../../context/AppContext";
import { useContext } from "react";
import { GameStatus } from "../../../models/game/game-status.enum";

export interface GameDetailProps {
    game: Game;
}

const GameDetail: React.FC<GameDetailProps> = ({ game }) => {
    const [_, dispatch] = useContext(AppContext);
    
    const updateGame = (status: string) => {
        dispatch({ type: 'UPDATE_GAME', payload: { ...game, gameStatus: status }});
    };

    const image = game.coverArtUrl ? 
        <Image
            className="rounded"
            src={'https:' + game.coverArtUrl}
            alt="game"
            layout="fill"
            objectFit="cover"
            quality={100} /> :
        null;

    return (
        <div className="flex flex-row h-full w-full">
            <div className="w-32 h-32 relative">
                {image}
            </div>
            <div className="flex flex-col pl-2 w-full">
                <div className="flex flex-row">
                    <p>Status in your library: <span className="font-semibold">{game.gameStatus}</span></p>
                    <div className="flex pl-2 w-full justify-end">
                        <GameDetailButton onClick={() => updateGame(GameStatus.Backlog)}>Move to Backlog</GameDetailButton>
                        <GameDetailButton onClick={() => updateGame(GameStatus.Wishlist)}>Move to Wishlist</GameDetailButton>
                        <GameDetailButton onClick={() => updateGame(GameStatus.InProgress)}>Start Progress</GameDetailButton>
                    </div>
                </div>
                <p>Your notes about this game:</p>
                {game.notes.map(note => <div>{note.content}</div>)}
            </div>
        </div>
    );
};

export default GameDetail;