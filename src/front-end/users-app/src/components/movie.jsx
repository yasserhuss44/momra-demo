import React, { Component } from "react";
import { NavLink } from "react-router-dom";
import { getUsers } from "../services/userService";
import { deleteMovie, getMovies } from "../services/fakeMovieService";
import Like from "./common/like";
import Table from "./common/table";
import { getCurrentPageItems } from "./common/pager";
import axios from "axios";

class Movies extends Component {
  state = {
    users: [],
    columns: [
      {
        title: "",
        path: "title",
        content: movie => (
          <NavLink to={`/movie-details/${movie.id}`}>{movie.title}</NavLink>
        )
      },
      {
        title: "name",
        path: "name"
      },
      {
        title: "Address",
        path: "address"
      },
      {
        title: "Age",
        path: "age"
      },
      {
        title: "",
        path: "like",
        content: movie => (
          <Like movie={movie} ontoggleLike={this.handleToggleLike} />
        )
      },
      {
        title: "",
        path: "delete",
        content: movie => (
          <button
            movie={movie}
            onClick={() => this.handleDelete(movie)}
            className="btn btn-danger"
          >
            Delete
          </button>
        )
      }
    ]
  };

  handleDelete = movie => {
    console.log(movie);
    deleteMovie(movie.id);
    this.setState({ movies: getMovies() });
  };
  async componentDidMount() {
    const promise = axios.get("http://localhost:60356/api/account");
    const response = await promise;
    console.log(response);
    this.setState({ users: response.data.itemsList });
  }
  handleToggleLike = movie => {
    const currentPageItems = [...this.state.users];
    const index = currentPageItems.indexOf(movie);
    currentPageItems[index] = movie;
    if (currentPageItems[index].isLiked === true)
      currentPageItems[index].isLiked = false;
    else currentPageItems[index].isLiked = true;

    this.setState({ users: currentPageItems });
  };

  viewsummary = () => {
    const { length: count } = this.state.users;
    if (count > 0) {
      return `Showing ${count} movies in the database.`;
    } else {
      return "There is no movies in the database.";
    }
  };

  render() {
    const users = this.state.users;

    return <Table data={users} columns={this.state.columns} />;
  }
}

export default Movies;
