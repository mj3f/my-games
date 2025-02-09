import Link from 'next/link';
import { useContext, useEffect, useState } from 'react';
import AppContext from '../../../context/AppContext';

interface NavbarLinkProps {
    children: any;
    href: string;
    onClick?: () => void;
}

const NavbarLink: React.FC<NavbarLinkProps> = ({ href, children, onClick }) => (
    <Link href={href} passHref>
        <a className="px-1 text-2xl font-semibold text-green-500 hover:text-white hover:cursor-pointer" onClick={onClick}>{children}</a>
    </Link>
);

const Navbar: React.FC = () => {
    const [appState, dispatch] = useContext(AppContext);
    const [showLoggedInNavLinks, setShowLoggedInNavLinks] = useState(false);


    useEffect(() => {
        setShowLoggedInNavLinks(appState.authState?.isAuthenticated);
    }, [appState]);

    const defaultNavLinks = (
        <>
            <NavbarLink href='/'>Home</NavbarLink>
            <NavbarLink href='/about'>About</NavbarLink>
            <NavbarLink href='/sign-in'>Sign In</NavbarLink>
        </>
    );

    const loggedInNavLinks = (
        <>
            <NavbarLink href='/'>Library</NavbarLink>
            <NavbarLink onClick={() => dispatch({type: 'LOG_OUT'})} href='/'>Sign Out</NavbarLink>
        </>
    );


    return (
        <div className="flex w-full h-16 justify-between items-center bg-gray-900 z-10">
            <h1 className="text-3xl font-semibold pl-2 text-green-500">myGames</h1>
            <div id="links-container" className="h-full px-2 flex items-center justify-end">
                {showLoggedInNavLinks ? loggedInNavLinks : defaultNavLinks}
            </div>
        </div>
    );
};

export default Navbar;