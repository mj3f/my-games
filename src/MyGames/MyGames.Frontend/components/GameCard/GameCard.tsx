import Image from "next/image";
import { Game } from "../../models/game/game.model";

export interface GameCardProps {
    game: Game;
}

const GameCard: React.FC<GameCardProps> = ({ game }) => {

    const image = game?.coverArtUrl ?
        <Image className="rounded" src={'https:' + game.coverArtUrl} alt="game" layout="fill" objectFit="cover" /> :
        <p className="text-white text-center">No cover art for this game</p>;

    return (
        <div className="flex flex-col h-60 w-60 rounded bg-gray-700 mx-2 hover:cursor-pointer">
            <div className="h-40 w-full rounded relative">
                {image}
            </div>
            <div className="rounded w-full">
                <p className="text-2xl pt-2 font-semibold text-center text-white">{game.name}</p>
            </div>
        </div>
    );
};

export default GameCard;
