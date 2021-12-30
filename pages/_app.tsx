import '../styles/globals.css'
import type { AppProps } from 'next/app'
import Store from '../store/Store';
import Head from 'next/head';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <Store>
      <Head>
        <title>myGames</title>
        <meta name="description" content="My games" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <Component {...pageProps} />
    </Store>);
}

export default MyApp
