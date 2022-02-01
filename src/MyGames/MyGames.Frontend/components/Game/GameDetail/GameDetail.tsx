import { Game } from "../../../models/game/game.model";
import Image from "next/image";
import GameDetailButton from "./GameDetailButton";
import { UsersService } from "../../../services/users.service";
import AppContext from "../../../context/AppContext";
import { useContext } from "react";
import { GameStatus } from "../../../models/game/game-status.enum";

export interface GameDetailProps {
    game: Game;
}

const GameDetail: React.FC<GameDetailProps> = ({ game }) => {
    const [appState, _] = useContext(AppContext);
    const userService = new UsersService();

    const updateGame = (status: string) => {

        if (appState.authState?.currentUser) {
            const username = appState.authState.currentUser.username;
            const updatedGame: Game = { ...game, gameStatus: status };
            userService.updateGameInUsersLibrary(username, updatedGame);
        }
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
    
    const moveToBacklogButton = <GameDetailButton onClick={() => updateGame(GameStatus.Backlog)}>Move to Backlog</GameDetailButton>
    const moveToWishlistButton = <GameDetailButton onClick={() => updateGame(GameStatus.Wishlist)}>Move to Wishlist</GameDetailButton>
    const startProgressButton = <GameDetailButton onClick={() => updateGame(GameStatus.InProgress)}>Start Progress</GameDetailButton>

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
                {game.notes.map(note => <div>{note.content}</div>)}
            </div>
        </div>
    );
};

export default GameDetail;