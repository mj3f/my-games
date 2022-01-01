import { useReducer } from "react";
import AppContext, { InitialAppContextState } from "../context/AppContext";
import Reducer from "./Reducer";


const Store: React.FC = ({ children }) => {
    const [appState, dispatch] = useReducer(Reducer, InitialAppContextState);

    return (
        <AppContext.Provider value={[appState, dispatch]}>
            {children}
        </AppContext.Provider>
    );
};

export default Store;