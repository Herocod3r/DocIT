import React from 'react';
import 'tachyons';

const Header = () => {
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
            About
          </a>
          <a class="f6 link dib white dim mr3 mr4-ns" href="#0">
            Sign In
          </a>
          <a class="f6 link dib white dim mr3 mr4-ns" href="#0">
            Sign Up
          </a>
        </div>
      </nav>
    </div>
  );
};

export default Header;
