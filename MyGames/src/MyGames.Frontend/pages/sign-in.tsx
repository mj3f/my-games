import axios from "axios";
import { NextPage } from "next";
import Link from "next/link";
import { useRouter } from "next/router";
import { FormEvent, useContext, useState } from "react";
import Button from "../components/Button/Button";
import AppContext from "../context/AppContext";
import { AuthState } from "../models/auth/auth-state.interface";
import { AuthService } from "../services/auth.service";
import { UsersService } from "../services/users.service";

const SignIn: NextPage = () => {
    const [username, setUsername] = useState(''); // not ideal after each keystroke, refactor this.
    const [password, setPassword] = useState('');
    const [errorMsg, setErrorMsg] = useState('');
    const [showSpinner, setShowSpinner] = useState(false);

    const [_, dispatch] = useContext(AppContext);
    const router = useRouter();
    const authService = new AuthService();
    const usersService = new UsersService();

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        setShowSpinner(true);
        e.preventDefault(); // prevent page refresh.
        await authService.login(username, password)
            .then(res => {
                getUser(username);
                setShowSpinner(false);
            })
            .catch(error => {
                setErrorMsg(error.response.data);
                setShowSpinner(false);
            });
    };

    // TODO: can this be done in the home component get props function?
    const getUser = async (username: string) => {
        await usersService.getUser(username)
            .then(user => {
                const authState: AuthState = {
                    isAuthenticated: true,
                    currentUser: user,
                    token: 'dummy_token'
                };

                dispatch({ type: 'LOG_IN', payload: authState});
                router.push('/');
            })
            .catch(error => console.error(error));

        // TODO: Handle errors when username/password inputs don't match any db entries.
    };

    const clearErrorMsg = () => errorMsg.length === 0 ?? setErrorMsg('');
    
    const formClass = 'rounded h-8 mt-1 pl-1 focus:outline-none focus:ring focus:ring-green-500';

    const containerContent = showSpinner ?
        <div className="h-full w-full flex items-center justify-center">Signing in....</div> :
        (
            <>
                <h2 className="text-2xl font-semibold pt-4 flex justify-center">Sign In</h2>
                <form className="flex flex-col h-full px-4" onSubmit={handleSubmit}>
                    <div className="flex flex-col py-4">
                        <label htmlFor="username">Username</label>
                        <input
                            type="text"
                            id="username"
                            className={formClass}
                            onChange={(e) => { setUsername(e.target.value); clearErrorMsg() }} />
                    </div>
                    <div className="flex flex-col pb-4">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            className={formClass}
                            onChange={(e) => { setPassword(e.target.value); clearErrorMsg() }} />
                    </div>
                    <div className="flex flex-col pb-4">
                        <Button
                            disabled={username.length === 0 || password.length === 0}
                            type="submit">Sign In</Button>
                        <p className="mb-2 text-center text-red-600">{errorMsg}</p>
                    </div>
                </form>
            </>
        );
    
    return (
        <div className="flex flex-col justify-center items-center h-screen w-full bg-gray-800">
            <div className="text-white text-2xl font-semibold p-2 flex w-1/2">
                <Link href="/">Go back</Link>
            </div>
            <div id="form-container" className="flex flex-col justify-start rounded w-1/2 h-72 bg-gray-200">
                {containerContent}
            </div>
        </div>
    );
};

export default SignIn;
