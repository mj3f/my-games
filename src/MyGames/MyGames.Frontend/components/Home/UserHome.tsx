import { useContext, useEffect, useState } from "react";
import AppContext from "../../context/AppContext";
import { User } from "../../models/user/user.model";
import GameCard from "../GameCard/GameCard";


// Logged in user home page.
const UserHome: React.FC = () => { // TODO: types in props.
    const [appState, _] = useContext(AppContext);
    const [user, setUser] = useState<User>(null!);

    useEffect(() => {
        console.log('appState = ', appState);
        if (appState.authState?.currentUser) {
            setUser(user);
        }
    },
    [appState]);

    return user ? (
        <div className="px-2 flex flex-col justify-start">
            <p className="text-4xl font-semibold">{user?.username}&apos;s Library</p>
            <div className="pt-4">
                <p className="text-3xl font-semibold">In Progress</p>
                <div id="collection" className="flex flex-row justify-start">
                    {user?.games.map(g => <GameCard game={g} />)}
                </div>
            </div>
        </div>
    ) : <h1>No user found!</h1>;
};

export default UserHome;