import type { NextPage } from 'next';
import Layout from '../components/Layout/Layout';

const About: NextPage = () => {
  return (
    <Layout>
      <div id="what">
        <h2 className="pt-4 text-4xl font-semibold">What is this?</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
           Proin blandit ligula ut nisi pretium tristique. Phasellus
           pretium tellus et erat hendrerit, nec pharetra nulla venenatis.
           Etiam erat justo, ullamcorper vel elementum placerat, tincidunt a ligula.
           Praesent et volutpat arcu. Maecenas suscipit facilisis nisi, et mattis leo.
           Pellentesque vestibulum orci a ipsum finibus cursus. Vivamus efficitur diam nisi.
           Vestibulum bibendum in nibh non sollicitudin. Sed libero neque,
           mattis ut fermentum et, mattis vel dolor. Vivamus commodo lectus in nunc pharetra,
           vel vestibulum elit condimentum. Suspendisse rhoncus lorem sit amet nibh tempor sagittis.
           Sed nec sagittis metus. Duis id feugiat sapien. Etiam at dui suscipit, consequat elit at,
           aliquam dui. Aliquam ultricies eros urna, vel porttitor purus rutrum eu. Cras id congue tortor.
        </p>
      </div>
      <div id="why">
        <h2 className="pt-4 text-4xl font-semibold">Why build this?</h2>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.
           aliquam dui. Aliquam ultricies eros urna, vel porttitor purus rutrum eu. Cras id congue tortor.
        </p>
      </div>
      <div id="credits">
        <h2 className="pt-4 text-2xl font-semibold">Acknowledgements</h2>
        <p>The game data is fetched from the RAWG API (link here).</p>
      </div>
      
    </Layout>
  );
}

export default About;
