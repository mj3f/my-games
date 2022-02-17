
export interface GameDetailButtonProps {
    onClick: () => void;
}

const GameDetailButton: React.FC<GameDetailButtonProps> = ({ onClick, children }) => (
    <button
        className="w-fit p-2 bg-gray-200 rounded hover:bg-gray-300 mr-2"
        onClick={onClick}>
            {children}
    </button>
);

export default GameDetailButton;