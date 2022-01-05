import { useContext, useEffect, useState } from "react";
import AppContext from "../../context/AppContext";
import { GameStatus } from "../../models/game/game-status.enum";
import { Game } from "../../models/game/game.model";
import { User } from "../../models/user/user.model";
import GameCard from "../GameCard/GameCard";
import UserGamesCollection from "./UserGamesCollection";


// Logged in user home page.
const UserHome: React.FC = () => { // TODO: types in props.
    const [appState, _] = useContext(AppContext);
    const [user, setUser] = useState<User>(null!);

    // On mount, get the logged in user and store it in state in case changes are made.
    useEffect(() => {
        if (appState.authState?.currentUser) {
            setUser(appState.authState.currentUser);
        }
    },
    [appState]);

    if (!user) {
        return null;
    }

    return (
        <div className="px-2 flex flex-col justify-start">
            <p className="text-4xl font-semibold">{user?.username}&apos;s Library</p>
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.InProgress)} title="In Progress" />
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.Backlog)} title="Backlog" />
            <UserGamesCollection games={user?.games.filter(g => g.gameStatus === GameStatus.Wishlist)} title="Wishlist" />
        </div>
    );
};

export default UserHome;