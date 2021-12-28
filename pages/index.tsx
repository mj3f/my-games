import type { NextPage } from 'next';
import Head from 'next/head';
import Layout from '../components/Layout/Layout';
import Image from 'next/image';
import background from '../public/wallpaper.jpg';

const Home: NextPage = () => {
  return (
    <div className="h-screen">
      <Head>
        <title>myGames</title>
        <meta name="description" content="My games" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <Layout>
        <div className="flex flex-col w-full px-32 h-full justify-center items-center bg-gray-800">
          <div className="z-10 text-green-500">
            <h1 className="text-4xl font-semibold text-center">Your Games, Your Time</h1>
            <p className="text-2xl text-center">Keep track of your library, what you're currently playing,
            where you're up to, and your backlog.</p>
          </div>
        </div>
      </Layout>

    </div>
  );
}

export default Home;
