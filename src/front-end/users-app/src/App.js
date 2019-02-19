import React, { Component } from 'react';
import { Route,Redirect, Switch, NavLink } from 'react-router-dom'
import Movies from './components/movie'
import MovieDetails from './components/movie-details'
import Customers from './components/customers'
import Rentals from './components/rentals'
import NavBar from './components/common/navBar'
import Vidly from './components/vidly'
import Notfound from './components/notFound';
import './App.css';
import Register from './components/registerForm';
class App extends Component {
  state = {
  };

  render() {
    return (
      <main className="container">
        <NavBar />
        <br/>
        <Switch>
          <Route path="/customers" component={Customers}></Route>
          <Route path="/movie-details/:id" component={MovieDetails}></Route>
          <Route path="/movies" component={Movies}></Route>
          <Route path="/rentals" component={Rentals}></Route>
          <Route path="/vidly" component={Vidly}></Route>
          <Route path="/register" component={Register}></Route>
          <Route path="/notfound" component={Notfound}></Route>
          <Route path="/" exact component={Movies}></Route>
          <Redirect to="/notfound"></Redirect>
        </Switch>
      </main>
    );
  }
}

export default App;
