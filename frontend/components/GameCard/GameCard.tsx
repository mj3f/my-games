import Image from "next/image";
import massEffectWallpaper from "../../public/mass-effect-background.png";

const GameCard: React.FC = () => {
    return (
        <div className="flex flex-col h-60 w-60 rounded bg-gray-700 mx-2 hover:h-96">
            <div className="h-40 w-full rounded relative">
                <Image className="rounded" src={massEffectWallpaper} alt="game" layout="fill" objectFit="cover" />
            </div>
            <div className="rounded w-full">
                <p className="text-2xl pt-2 font-semibold text-center text-white">Mass Effect Legendary Edition</p>
                <div id="extra-info" className="px-2 text-white hidden hover:inline-block">
                    <p>Added on: 01/01/2022</p>
                    <p>more placeholder text here!</p>
                </div>
            </div>
        </div>
    );
};

export default GameCard;