
export interface GameDetailButtonProps {
    onClick: () => void;
    colorIsRed?: boolean;
}

const GameDetailButton: React.FC<GameDetailButtonProps> = ({ onClick, colorIsRed, children }) => {
    const buttonColor = colorIsRed ? 'bg-red-500 text-white' : 'bg-gray-200';
    const hoverButtonColor = colorIsRed ? 'hover:bg-red-600 text-white' : 'hover:bg-gray-300';
    return (
        <button
            className={`w-fit p-2 ${buttonColor} rounded ${hoverButtonColor} mr-2`}
            onClick={onClick}>
            {children}
        </button>
    );
}

export default GameDetailButton;