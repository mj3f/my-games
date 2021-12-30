import axios from "axios";
import { useContext, useEffect } from "react";
import AppContext from "../../context/AppContext";
import secrets from "../../secrets.json";

// Logged in user home page.
const UserHome: React.FC = () => { // TODO: types in props.
    const [appState, _] = useContext(AppContext);

    useEffect(() => {
        console.log('user home mounted!!!!');
        async function authWithTwitch() {
            // TODO: Can this be done in getStaticProps?
            console.log('sectets = ', secrets.clientId);
            axios.post(`https://id.twitch.tv/oauth2/token?client_id=${secrets.clientId}&client_secret=${secrets.clientSecret}&grant_type=client_credentials`)
                .then((res) => console.log(res))
                .catch(err => console.error(err));

                // example request: https://api.igdb.com/v4/games/85031?fields=name,cover,genres,platforms
                // see docs: https://api-docs.igdb.com/#game
        }
        authWithTwitch();
    }, []);

    return (
        <div className="px-2 flex flex-col justify-start">
            <p className="text-4xl font-semibold">{appState.authState?.currentUser?.username}&apos;s Library</p>
            <div className="pt-4">
                <p className="text-3xl font-semibold">In Progress</p>
                <div id="collection" className="flex flex-row justify-start">
                    <p>test 1</p>
                    <p>test 2</p>
                    <p>test 3</p>
                </div>
            </div>
        </div>
    );
};

interface TwitchLogin {
    access_token: string;
    expires_in: number;
    token_type: string;
}

export default UserHome;