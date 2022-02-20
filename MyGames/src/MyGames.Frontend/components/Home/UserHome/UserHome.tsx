import { useContext, useEffect, useState } from "react";
import AppContext from "../../../context/AppContext";
import { GameStatus } from "../../../models/game/game-status.enum";
import { User } from "../../../models/user/user.model";
import Button from "../../Button/Button";
import UserGamesCollection from "../UserGamesCollection";
import Modal from "../../Modal/Modal";
import AddGame from "./AddGame";
import { IgdbGame } from "../../../models/game/igdb-game.model";
import { UsersService } from "../../../services/users.service";
import { Game } from "../../../models/game/game.model";


// Logged in user home page.
const UserHome: React.FC = () => { // TODO: types in props.
    const [appState, dispatch] = useContext(AppContext);
    const [user, setUser] = useState<User>(null!);
    const [showAddGameModal, setShowAddGameModal] = useState(false);
    const usersService = new UsersService(); // TODO: Inject this?

    // On mount, get the logged in user and store it in state in case changes are made.
    useEffect(() => {
        if (appState.authState?.currentUser) {
            setUser(appState.authState.currentUser);
        }

        if (user && appState.updateGame) {
            updateGame(appState.updateGame)
                .then(_ => dispatch({ type: 'UPDATE_GAME', payload: null})) // clear it from state.
        }

        if (user && appState.gameIdToRemove) {
            removeGameFromUsersLibrary(appState.gameIdToRemove)
                .then(_ => dispatch({ type: 'REMOVE_GAME', payload: null }));
        }
    },
    [appState]);

    const openAddGameModal = () => {
        setShowAddGameModal(true);
    };

    const addGameToUsersLibrary = async (game: IgdbGame) => {
        // TODO: addGame should return the game, then we can add it to the users games.
        await usersService.addGameToUsersLibrary(user.username, game)
            .then(res => {
                user.games.push(res);
                setShowAddGameModal(false);
            })
            .catch(error => console.error(error));
    };

    const removeGameFromUsersLibrary = async (gameId: string) => {
        await usersService.removeGameFromUsersLibrary(user.username, gameId)
            .then(_ => {
                const userCopy = { ...user };
                const gameToRemove = userCopy.games.find(g => g.id === appState.gameIdToRemove);
                // @ts-ignore
                const index = userCopy.games.indexOf(gameToRemove);
                userCopy.games.splice(index, 1);
                setUser(userCopy); // re-render games collection for user.
            })
            .catch(error => console.error(error));
    };

    const updateGame = async (game: Game) => {
        if (user) {
            const username = user.username;
            await usersService.updateGameInUsersLibrary(username, game)
                .then(() => {
                    const updatedUser = { ...user };
                    const existingGame: Game | undefined =
                        updatedUser.games.find(g => g.id === game.id);
                    if (existingGame) {
                        existingGame.gameStatus = game.gameStatus;
                        existingGame.notes = game.notes;
                    }
                    setUser(updatedUser);
                })
                .catch(error => console.error(error));
        }
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
