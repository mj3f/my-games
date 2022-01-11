import { useContext, useEffect, useState } from "react";
import AppContext from "../../context/AppContext";
import { GameStatus } from "../../models/game/game-status.enum";
import { User } from "../../models/user/user.model";
import Button from "../Button/Button";
import UserGamesCollection from "./UserGamesCollection";
import Modal from "../Modal/Modal";


// Logged in user home page.
const UserHome: React.FC = () => { // TODO: types in props.
    const [appState, _] = useContext(AppContext);
    const [user, setUser] = useState<User>(null!);
    const [showAddGameModal, setShowAddGameModal] = useState(false);

    // On mount, get the logged in user and store it in state in case changes are made.
    useEffect(() => {
        if (appState.authState?.currentUser) {
            setUser(appState.authState.currentUser);
        }
    },
    [appState]);

    const addGame = () => {
        setShowAddGameModal(true);
    };

    if (!user) {
        return null;
    }

    return (
        <div className="px-2 flex flex-col justify-start">
            <div className="flex flex-row pt-2 justify-between">
                <p className="text-4xl font-semibold">{user?.username}&apos;s Library</p>
                <Button onClick={addGame}>Add Game</Button>
            </div>
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.InProgress)} title="In Progress" />
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.Backlog)} title="Backlog" />
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.Wishlist)} title="Wishlist" />
            <Modal isOpen={showAddGameModal} onClose={() => setShowAddGameModal(false)} title="Add Game">
                <p>Show me what you got!</p>
            </Modal>
        </div>
    );
};

export default UserHome;
