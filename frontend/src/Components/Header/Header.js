import React from 'react';
import 'tachyons';
import '../../App.css';

const Header = ({ onRouteChange, isSignedIn }) => {
  if (isSignedIn) {
    return (
      <div>
        <nav>
          <p
            onClick={() => onRouteChange('home')}
            class="link dim black b f1 f-headline-ns tc db pointer m-0"
            title="Home"
            style={{fontFamily: 'Monospace, Sans-Serif'}}
          >
            DOC-IT
          </p>
          <div class="tc">
            <p
              //   onClick={() => onRouteChange('signin')}
              className="link dim gray f6 f5-ns dib mr3 pointer"
            >
              Gad Jacobs
            </p>
            <p
              onClick={() => onRouteChange('signin')}
              className="link dim gray f6 f5-ns dib mr3 pointer"
            >
              Sign Out
            </p>
          </div>
        </nav>
      </div>
    );
  } else {
    return (
      <div>
        <nav>
          <a
            class="link dim black b f1 f-headline-ns tc db mb3 mb4-ns"
            href="#0"
            title="Home"
          >
            DOC-IT
          </a>
          <div class="tc pb3">
            <p
              onClick={() => onRouteChange('signin')}
              className="link dim gray f6 f5-ns dib mr3 pointer"
            >
              Sign In
            </p>
            <p
              onClick={() => onRouteChange('register')}
              className="link dim gray f6 f5-ns dib mr3 pointer"
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
