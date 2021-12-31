import axios, { AxiosRequestConfig } from "axios";
import { GetStaticProps } from "next";
import { useContext, useEffect } from "react";
import AppContext from "../../context/AppContext";
import { TwitchLogin } from "../../models/igdb/twitch-login.interface";
import secrets from "../../secrets.json";


// Logged in user home page.
const UserHome: React.FC = () => { // TODO: types in props.
    const [appState, _] = useContext(AppContext);

    useEffect(() => {
        const getData = async () => {
            let twitchLogin: TwitchLogin = {
                access_token: '',
                expires_in: 0,
                token_type: ''
            };
        
            await axios.post(`https://id.twitch.tv/oauth2/token?client_id=${secrets.clientId}&client_secret=${secrets.clientSecret}&grant_type=client_credentials`)
                        .then((res) => twitchLogin = res.data)
                        .catch(err => console.error(err));

            const config: AxiosRequestConfig = {
                headers: {
                    'Client-ID': secrets.clientId,
                    'Authorization': 'Bearer ' + twitchLogin.access_token
                }
            };

            axios.get('https://api.igdb.com/v4/games/85031?fields=name,cover,genres,platforms', config)
                .then(res => console.log(res))
                .catch(err => console.error(err));
        };

        getData();

        // example request: https://api.igdb.com/v4/games/85031?fields=name,cover,genres,platforms
        // see docs: https://api-docs.igdb.com/#game
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

export default UserHome;