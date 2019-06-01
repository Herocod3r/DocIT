import React from 'react';
import 'tachyons';
import '../../App.css';

const Header = ({ onRouteChange, isSignedIn }) => {
  if (!isSignedIn) {
    return (
      <div>
        <nav class="flex justify-between bb b--white-10">
          <a
            className="link white-70 hover-white no-underline flex items-center pa3"
            href="/"
          >
            <h3>DocIT</h3>
          </a>
          <div class="flex-grow pa3 flex items-center">
            <a class="f6 link dib white dim mr3 mr4-ns" href="#0">
              Gad Jacobs
            </a>
            <a class="f6 link dib white dim mr3 mr4-ns" href="#0">
              Sign Out
            </a>
          </div>
        </nav>
      </div>
    );
  } else {
    return (
      <div>
        <nav class="flex justify-between bb b--white-10">
          <a
            className="link white-70 hover-white no-underline flex items-center pa3"
            href="/"
          >
            <h3>Doc IT</h3>
          </a>
          <div class="flex-grow pa3 flex items-center">
            <p
              onClick={() => onRouteChange('signin')}
              className="f3 white link dim underline pointer"
            >
              Sign In
            </p>
            <p
              onClick={() => onRouteChange('register')}
              className="f3 white link dim underline pointer"
            >
              Register
            </p>
          </div>
        </nav>
      </div>
    );
  }
};

export default Header;
