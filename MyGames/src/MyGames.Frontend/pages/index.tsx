import type { NextPage } from 'next';
import { useContext, useEffect, useState } from 'react';
import DefaultHome from '../components/Home/DefaultHome';
import UserHome from '../components/Home/UserHome/UserHome';
import Layout from '../components/Layout/Layout';
import AppContext from '../context/AppContext';
import { User } from '../models/user/user.model';

interface HomeProps {
  currentUser: User;
}

const Home: NextPage<HomeProps> = ({ currentUser }) => {
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
