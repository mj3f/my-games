import { useContext, useEffect, useState } from "react";
import AppContext from "../../context/AppContext";
import { Game } from "../../models/game/game.model";
import { User } from "../../models/user/user.model";
import GameCard from "../GameCard/GameCard";


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

    return (
        <div className="px-2 flex flex-col justify-start">
            <p className="text-4xl font-semibold">{user?.username}&apos;s Library</p>
            <div className="pt-4">
                <p className="text-3xl font-semibold">In Progress</p>
                <div id="collection" className="flex flex-row justify-start">
                    {user?.games.map((g: Game, idx: number) => <GameCard game={g} key={idx} />)}
                </div>
            </div>
        </div>
    );
};

export default UserHome;