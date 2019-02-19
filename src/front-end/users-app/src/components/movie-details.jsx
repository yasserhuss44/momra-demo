import React, { Component } from 'react';
import { parse } from 'query-string';
class MovieDetails extends Component {
    state = {}
    handleSave=()=>{
        this.props.history.push('/movies');
    }
    render() {
        const { id } = this.props.match.params;
        return (
            <React.Fragment>
                <h1>Movie:  {id}</h1>
                <button onClick={this.handleSave}>Save</button>
            </React.Fragment>
        );
    }
}

export default MovieDetails; //