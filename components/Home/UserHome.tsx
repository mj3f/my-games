import { useContext } from "react";
import AppContext from "../../context/AppContext";

// Logged in user home page.
const UserHome: React.FC = () => {
    const [appState, _] = useContext(AppContext);
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