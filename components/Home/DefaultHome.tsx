import { useRouter } from "next/router";
import Button from "../Button/Button";

const DefaultHome: React.FC = () => {
    const router = useRouter();

    const goToPage = (href: string) => router.push(href);

    return (
        <div className="flex flex-col w-full px-32 h-full justify-center items-center bg-gray-800">
            <div className="z-10 text-green-500">
                <h1 className="text-4xl font-semibold text-center">Your Games, Your Time</h1>
                <p className="text-2xl text-center">Keep track of your library, what you&apos;re currently playing,
                    where you&apos;re up to, and your backlog.</p>
                <div className="flex justify-center pt-2">
                    <div>
                        <Button onClick={() => goToPage('/about')}>Learn more</Button>
                    </div>
                    <div className="ml-4">
                        <Button onClick={() => goToPage('/sign-up')}>Sign Up</Button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default DefaultHome;