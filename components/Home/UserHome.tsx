import { useContext } from "react";
import AppContext from "../../context/AppContext";

// Logged in user home page.
const UserHome: React.FC = () => {
    const [appState, _] = useContext(AppContext);
    return (
        <div className="px-2">
            <p className="text-3xl font-semibold">Welcome {appState.authState?.currentUser?.username}</p>
        </div>
    );
};

export default UserHome;