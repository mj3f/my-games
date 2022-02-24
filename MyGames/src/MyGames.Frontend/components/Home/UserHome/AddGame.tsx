import {useEffect, useState} from "react";
import { GamesService } from "../../../services/games.service";
import {IgdbGame} from "../../../models/game/igdb-game.model";

export interface AddGameProps {
    selectGame: (game: IgdbGame) => void;
}

const AddGame: React.FC<AddGameProps> = ({ selectGame }) => {
    const [games, setGames] = useState<IgdbGame[]>([]);
    const [showSpinner, setShowSpinner] = useState(false);

    const gamesService = new GamesService();

    const searchForGame = async (name: string) => {
        setShowSpinner(true);
        await gamesService.searchForGame(name)
            .then((games: IgdbGame[]) => {
               setGames(games);
            });
    };

    useEffect(() => setShowSpinner(false), [games]);

    const inputClass = 'rounded w-full h-8 mt-1 pl-1 outline-green-500 ring ring-green-500';
    
    return (
        <div className="flex flex-col w-full">
            <input 
                className={inputClass}
                type="text"
                name="game"
                onChange={(e) => searchForGame(e.target.value)} />
            <p className="pt-2 font-semibold">Results</p>
            {showSpinner ?
                <div className="text-center">
                    Fetching...
                </div> :
                <ul className="pt-2">
                    {games.map(game =>
                        <li key={game.id}
                            className="px-2 py-1 rounded hover:cursor-pointer hover:bg-green-500"
                            onClick={() => selectGame(game)}>{game.name}</li>)}
                </ul>
            }
        </div>
    );
};

export default AddGame;
