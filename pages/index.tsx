import type { NextPage } from 'next';
import Head from 'next/head';
import Layout from '../components/Layout/Layout';

const Home: NextPage = () => {
  return (
    <div>
      <Head>
        <title>myGames</title>
        <meta name="description" content="My games" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <Layout>
        <p>Hello there!</p>
      </Layout>

    </div>
  );
}

export default Home;
