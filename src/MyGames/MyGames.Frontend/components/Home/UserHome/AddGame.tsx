import axios from "axios";
import { useState } from "react";
import { Game } from "../../../models/game/game.model";


const AddGame: React.FC = () => {
    const [games, setGames] = useState<Game[]>([]);

    const searchForGame = async (name: string) => {
        await axios.get(`http://localhost:5109/api/v0/games?name=${name}`)
        .then(res => setGames(res.data))
        .catch(err => console.error(err));
    };

    const inputClass = 'rounded w-full h-8 mt-1 pl-1 outline-green-500 ring ring-green-500';
    
    return (
        <div className="flex w-full">
            <input 
                className={inputClass}
                type="text"
                name="game"
                onChange={(e) => searchForGame(e.target.value)} />
            <ul>
                {games.map(game => <li key={game.id}>{game.name}</li>)}
            </ul>
        </div>
    );
};

export default AddGame;