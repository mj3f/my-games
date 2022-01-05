import { Game } from "../../models/game/game.model";
import GameCard from "../GameCard/GameCard";

interface UserGamesCollectionProps {
    games: Game[];
    title: string;
}

const UserGamesCollection: React.FC<UserGamesCollectionProps> = ({ games, title }) => (
    <div className="pt-4">
        <p className="text-3xl font-semibold">{title}</p>
        <div id="collection" className="flex flex-row justify-start">
            {games.map((g: Game, idx: number) => <GameCard game={g} key={idx} />)}
        </div>
    </div>
);

export default UserGamesCollection;