import React from 'react';
import 'tachyons';
import '../../App.css';

const Header = ({ onRouteChange, isSignedIn }) => {
  if (!isSignedIn) {
    return (
      <div>
        <nav>
          <p
            onClick={() => onRouteChange('home')}
            className="mv0 heading link dim black b f1 tc db pointer m-0"
            title="Home"
          >
            Doc-it!
          </p>
          {/* <div class="tc">
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
          </div> */}
        </nav>
        <hr />
      </div>
    );
  } else {
    return (
      <div>
        <nav>
          <p
            onClick={() => onRouteChange('home')}
            className="mv1 heading link dim black b f1 fw6 tc db pointer m-0"
            title="Home"
          >
            Doc-it!
          </p>
          {/* <div class="tc pb3">
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
          </div> */}
        </nav>
        <hr />
      </div>
    );
  }
};

export default Header;
