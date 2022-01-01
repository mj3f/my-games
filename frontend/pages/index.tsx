import type { NextPage } from 'next';
import { useContext, useEffect, useState } from 'react';
import DefaultHome from '../components/Home/DefaultHome';
import UserHome from '../components/Home/UserHome';
import Layout from '../components/Layout/Layout';
import AppContext from '../context/AppContext';

const Home: NextPage = () => {
  const [appState, _] = useContext(AppContext);
  const [showUserHomePage, setShowUserHomePage] = useState(false);

  useEffect(() => {
    setShowUserHomePage(appState.authState?.isAuthenticated);
  }, [appState]);
  
  return (
    <Layout>
      {showUserHomePage ? <UserHome /> : <DefaultHome />}
    </Layout>
  );
}

export default Home;
