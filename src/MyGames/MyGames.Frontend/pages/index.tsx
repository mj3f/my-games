import axios from 'axios';
import type { GetServerSideProps, NextPage } from 'next';
import { useContext, useEffect, useState } from 'react';
import DefaultHome from '../components/Home/DefaultHome';
import UserHome from '../components/Home/UserHome';
import Layout from '../components/Layout/Layout';
import AppContext from '../context/AppContext';
import { User } from '../models/user/user.model';

interface HomeProps {
  currentUser: User;
}

// export const getServerSideProps: GetServerSideProps = async (context) => {
//   let user: User = null;
//   await axios.get('http://localhost:5109/api/v0/users/dummy')
//     .then(res => user = res.data)
//     .catch(err => console.error(err));

//   console.log('Home component getServerSideProps');
//   console.log('User = ', user);
//   return {
//     props: {
//       currentUser: user
//     }
//   }
// }

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
