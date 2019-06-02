import React from 'react';

const Home = ({ onRouteChange, isSignedIn }) => {
  return (
    <div>
      <main>
        <article class="bg-white">
          <div class="ph4 ph5-m ph6-l">
            <div class="pv4 f4 f2-ns center">
              {/* <h1 class="heading fw6 f1 fl w-100 black-70 tc mt0 mb3">
                About Doc-it
              </h1> */}
              <p class="db black-70 serif mv0 baskerville">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed vel
                lectus porta, mollis dolor ut, auctor eros. Sed placerat
                tincidunt sem a congue. Praesent mauris eros, aliquam quis felis
                at, rhoncus auctor sem. Curabitur tempor vulputate risus, et
                bibendum lacus venenatis id. Cras nisl erat, aliquet ut eleifend
                vulputate, vestibulum a libero. Vivamus sagittis pulvinar nunc,
                eget euismod ligula fringilla at.
              </p>
            </div>
          </div>
        </article>
        <article class="cf">
          <div
            onClick={() => onRouteChange('signin')}
            style={{ marginLeft: '25%' }}
            class="fl w-25 bg-near-white tc pointer"
          >
            <p className="link mh0 dim gray f6 f5-ns dib mr3">Sign In</p>
          </div>
          <div
            onClick={() => onRouteChange('register')}
            class="fl w-25 bg-light-gray tc pointer"
          >
            <p className="link mh0 dim gray f6 f5-ns dib mr3">Register</p>
          </div>
        </article>
      </main>
    </div>
  );
};

export default Home;
