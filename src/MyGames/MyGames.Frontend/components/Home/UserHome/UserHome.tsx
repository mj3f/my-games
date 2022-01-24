import { useContext, useEffect, useState } from "react";
import AppContext from "../../../context/AppContext";
import { GameStatus } from "../../../models/game/game-status.enum";
import { User } from "../../../models/user/user.model";
import Button from "../../Button/Button";
import UserGamesCollection from "../UserGamesCollection";
import Modal from "../../Modal/Modal";
import AddGame from "./AddGame";
import {IgdbGame} from "../../../models/game/igdb-game.model";
import {UsersService} from "../../../services/users.service";


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

    const openAddGameModal = () => {
        setShowAddGameModal(true);
    };

    const addGameToUsersLibrary = async (game: IgdbGame) => {
        const usersService = new UsersService(); // TODO: Inject this?
        // TODO: addGame should return the game, then we can add it to the users games.
        await usersService.addGameToUsersLibrary(user.username, game)
            .then(res => console.log(res))
            .catch(error => console.error(error));
    };

    if (!user) {
        return null;
    }

    return (
        <div className="px-2 flex flex-col justify-start">
            <div className="flex flex-row pt-2 justify-between">
                <p className="text-4xl font-semibold">{user?.username}&apos;s Library</p>
                <Button onClick={openAddGameModal}>Add Game</Button>
            </div>
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.InProgress)} title="In Progress" />
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.Backlog)} title="Backlog" />
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.Wishlist)} title="Wishlist" />
            <Modal
                isOpen={showAddGameModal}
                onClose={() => setShowAddGameModal(false)}
                title="Add Game"
                hideSubmitButton>
                    <AddGame selectGame={addGameToUsersLibrary}></AddGame>
            </Modal>
        </div>
    );
};

export default UserHome;
