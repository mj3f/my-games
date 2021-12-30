import { NextPage } from "next";
import { useRouter } from "next/router";
import { FormEvent, useContext, useState } from "react";
import Button from "../components/Button/Button";
import AppContext from "../context/AppContext";
import { AuthState } from "../models/auth/auth-state.interface";
import { User } from "../models/user/user.model";

const SignIn: NextPage = () => {
    const [username, setUsername] = useState(''); // not ideal after each keystroke, refactor this.
    const [password, setPassword] = useState('');
    const [_, dispatch] = useContext(AppContext);
    const router = useRouter();

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault(); // prevent page refresh.
        console.log('Hello there!');

        const authState: AuthState = {
            isAuthenticated: true,
            currentUser: new User('Dummy user'),
            token: 'dummy_token'
        };

        dispatch({ type: 'LOG_IN', payload: authState});
        router.push('/');
    };
    
    const formClass = 'rounded h-8 mt-1 pl-1 focus:outline-none focus:ring focus:ring-green-500';
    return (
        <div className="flex justify-center items-center h-screen w-full bg-gray-800">
            <div id="form-container" className="flex flex-col justify-start rounded w-1/2 bg-gray-200">
                <h2 className="text-2xl font-semibold pt-4 flex justify-center">Sign In</h2>
                <form className="flex flex-col h-full px-4" onSubmit={handleSubmit}>
                    <div className="flex flex-col py-4">
                        <label htmlFor="username">Username</label>
                        <input
                            type="text"
                            id="username"
                            className={formClass}
                            onChange={(e) => setUsername(e.target.value)} />
                    </div>
                    <div className="flex flex-col py-4">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password"
                            id="password"
                            className={formClass}
                            onChange={(e) => setPassword(e.target.value)} />
                    </div>
                    <div className="flex flex-col py-4">
                        <Button
                            disabled={username.length === 0 || password.length === 0}
                            type="submit">Sign In</Button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default SignIn;