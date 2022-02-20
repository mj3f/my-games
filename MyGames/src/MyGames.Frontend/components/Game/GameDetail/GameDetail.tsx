import { Game } from "../../../models/game/game.model";
import Image from "next/image";
import GameDetailButton from "./GameDetailButton";
import AppContext from "../../../context/AppContext";
import {useContext, useState} from "react";
import { GameStatus } from "../../../models/game/game-status.enum";

export interface GameDetailProps {
    game: Game;
    onClose: () => void;
}

const GameDetail: React.FC<GameDetailProps> = ({ game, onClose }) => {
    const [_, dispatch] = useContext(AppContext);
    const [showYesNoButtons, setShowYesNoButtons] = useState(false);
    
    const updateGame = (status: string) => {
        dispatch({ type: 'UPDATE_GAME', payload: { ...game, gameStatus: status }});
        onClose();
    };

    const removeGameFromLibrary = () => {
        dispatch({ type: 'REMOVE_GAME', payload: game.id });
        setShowYesNoButtons(false);
        onClose();
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
                <div className="flex flex-row">
                    {showYesNoButtons ?
                        <div>
                            <button className="w-fit p-2 rounded text-white bg-blue-500 hover:bg-blue-600" onClick={removeGameFromLibrary}>Confirm</button>
                            <button className="w-fit p-2 rounded bg-gray-200 hover:bg-gray-300" onClick={() => setShowYesNoButtons(false)}>Cancel</button>
                        </div>
                        : <GameDetailButton onClick={() => setShowYesNoButtons(true)} colorIsRed>Remove Game</GameDetailButton>}
                </div>
                <p>Your notes about this game:</p>
                {game.notes.map(note => <div>{note.content}</div>)}
            </div>
        </div>
    );
};

export default GameDetail;