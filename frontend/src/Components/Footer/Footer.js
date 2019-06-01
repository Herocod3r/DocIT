import React from 'react';

const Footer = () => {
  return (
    <div>
      <hr />
      <footer class="pv4 ph3 ph5-m ph6-l mid-gray">
        <small class="f6 db tc">
          © 2019 <b class="ttu">DOC IT Inc</b>. All Rights Reserved
        </small>
        <div class="tc mt3">
          <a href="/" title="Language" class="f6 dib ph2 link mid-gray dim">
            Language
          </a>
          <a href="/" title="Terms" class="f6 dib ph2 link mid-gray dim">
            Terms of Use
          </a>
          <a href="/" title="Privacy" class="f6 dib ph2 link mid-gray dim">
            Privacy
          </a>
        </div>
      </footer>
    </div>
  );
};

export default Footer;
