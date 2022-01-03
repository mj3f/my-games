import { useRouter } from "next/router";
import Particles, { EasingType, IOptions, RecursivePartial } from "react-tsparticles";
import Button from "../Button/Button";

const DefaultHome: React.FC = () => {
    const router = useRouter();

    const goToPage = (href: string) => router.push(href);

    const particleOptions: RecursivePartial<IOptions> = {
        fpsLimit: 60,
        interactivity: {
          events: {
            onHover: {
              enable: true,
              mode: "repulse",
            },
            resize: true,
          },
          modes: {
            attract: {
                distance: 200,
                duration: 0.5,
                easing: EasingType.easeOutCubic,
                factor: 1,
                maxSpeed: 25,
                speed: 1
            },
            bubble: {
                distance: 400,
                duration: 2,
                mix: false,
                opacity: 0.8,
                size: 40
            },
            connect: {
                distance: 80,
                radius: 60
            }
          },
        },
        particles: {
          color: {
            value: "#22C55E",
          },
          links: {
            color: "#22C55E",
            distance: 150,
            enable: true,
            opacity: 0.5,
            width: 1,
          },
          collisions: {
            enable: true,
          },
          move: {
            direction: "none",
            enable: true,
            outMode: "bounce",
            random: false,
            speed: 2,
            straight: false,
          },
          number: {
            density: {
              enable: true,
              area: 800,
            },
            value: 80,
          },
          opacity: {
            value: 0.5,
          },
          shape: {
            type: "circle",
          },
          size: {
            random: true,
            value: 5,
          },
        },
        detectRetina: true,
    };

    return (
        <div className="flex flex-col w-full px-32 h-full justify-center items-center bg-gray-800">
            <Particles id="tsparticles" options={particleOptions} />
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