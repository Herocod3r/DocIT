import React, { Component } from 'react';
import './App.css';
import 'tachyons';
import Header from './Components/Header/Header';
import Home from './Components/Home/Home';
import Footer from './Components/Footer/Footer';
import SignIn from './Components/SignIn/SignIn';
import Register from './Components/Register/Register';
import Dashboard from './Components/Dashboard/Dashboard';

const initialState = {
  route: 'dashboard',
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

  loadUser = data => {
    this.setState({
      user: {
        id: data.id,
        name: data.name,
        email: data.email
      }
    });
  };

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
        {this.state.route === 'dashboard' ? (
          <div>
            <Header
              isSignedIn={isSignedIn}
              onRouteChange={this.onRouteChange}
            />
            <Dashboard />
            <Footer />
          </div>
        ) : (
          <div>
            <Header
              isSignedIn={isSignedIn}
              onRouteChange={this.onRouteChange}
            />
            <Home isSignedIn={isSignedIn} onRouteChange={this.onRouteChange} />
            {this.state.route === 'home' ? (
              <SignIn
                loadUser={this.loadUser}
                onRouteChange={this.onRouteChange}
              />
            ) : route === 'signin' ? (
              <SignIn
                loadUser={this.loadUser}
                onRouteChange={this.onRouteChange}
              />
            ) : (
              <Register
                loadUser={this.loadUser}
                onRouteChange={this.onRouteChange}
              />
            )}
            <Footer />
          </div>
        )}
      </div>
    );
  }
}

export default App;
