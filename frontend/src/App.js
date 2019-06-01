import React, { Component } from 'react';
import './App.css';
import 'tachyons';
import Header from './Components/Header/Header';
import Home from './Components/Home/Home';
import Footer from './Components/Footer/Footer';
import SignIn from './Components/SignIn/SignIn';
import Register from './Components/Register/Register';

const initialState = {
  route: 'home',
  isSignedIn: true,
  user: {
    id: '',
    name: '',
    email: ''
  }
};

class App extends Component {
  constructor() {
    super();
    this.state = initialState;
  }

  onRouteChange = route => {
    if (route === 'home') {
      this.setState({ isSignedIn: true });
    } else if (route === 'signout') {
      this.setState(initialState);
    }
    this.setState({ route: route });
  };

  render() {
    const { route, isSignedIn } = this.state;
    return (
      <div>
        <Header isSignedIn={isSignedIn} onRouteChange={this.onRouteChange} />
        {this.state.route === 'home' ? (
          <Home />
        ) : route === 'signin' ? (
          <SignIn />
        ) : (
          <Register />
        )}
        <Footer />
      </div>
    );
  }
}

export default App;
