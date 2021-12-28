import Link from 'next/link';

interface NavbarLinkProps {
    href: string;
    children: any;
}

const NavbarLink: React.FC<NavbarLinkProps> = ({ href, children }) => (
    <Link href={href} passHref>
        <a className="px-1 text-2xl font-semibold text-green-500 hover:text-white hover:cursor-pointer">{children}</a>
    </Link>
);

const Navbar: React.FC = () => {
    return (
        <div className="flex w-full h-16 justify-between items-center bg-gray-900">
            <h1 className="text-3xl font-semibold pl-2 text-green-500">myGames</h1>
            <div id="links-container" className="h-full px-2 flex items-center justify-end">
                <NavbarLink href='/'>Home</NavbarLink>
                <NavbarLink href='/about'>About</NavbarLink>
                <NavbarLink href='/sign-in'>Sign In</NavbarLink>
            </div>
        </div>
    );
};

export default Navbar;