import Navbar from "./Navbar/Navbar";

const Layout: React.FC = ({ children }) => {
    return (
        <div className="flex flex-col w-full h-full">
            <Navbar />
            {children}
        </div>
    );
};

export default Layout;