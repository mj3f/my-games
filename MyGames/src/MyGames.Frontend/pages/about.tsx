import type { NextPage } from 'next';
import { useRouter } from 'next/router';
import Button from '../components/Button/Button';
import Layout from '../components/Layout/Layout';

const About: NextPage = () => {
  const router = useRouter();

  return (
    <Layout>
      <div className="pl-2 text-white bg-gray-800 h-full flex flex-col">
        <div id="what" className="pt-2">
          <h2 className="pt-4 text-4xl font-semibold">What is this?</h2>
          <p>myGames is an application for keeping track of your video game library.
            With myGames, you can add games to your library, keep track of your current progress,
            add games to your backlog or wishlist, and more!
          </p>
        </div>
        <div id="why" className="pt-10">
          <h2 className="text-4xl font-semibold">Why build this?</h2>
          <p>I am building this purely for fun in my spare time to track the progress of my
            games library.
          </p>
        </div>
        <div id="credits" className="pt-10">
          <h2 className="pt-4 text-2xl font-semibold">Acknowledgements</h2>
          <p>The game data you see throughout this site upon logging in comes from IGDB.</p>
          <a href="https://igdb.com/" className="text-green-500 hover:underline">Click here to go to the IGDB website!</a>
        </div>
        <div className="flex justify-center items-center pt-20">
          <h2 className="text-2xl">Ready to start? (Sorry! we're not quite there yet!)</h2>
        </div>
        <div className="flex justify-center items-center pt-2">
          <Button onClick={() => router.push('/sign-up')} disabled>Sign Up</Button>
        </div>
      </div>
    </Layout>
  );
}

export default About;
