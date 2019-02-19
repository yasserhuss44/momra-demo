import React, { Component } from 'react';

class Like extends Component {
    render() {
        const {movie,ontoggleLike}=this.props;
        return (
            <React.Fragment>
                <li className={movie.isLiked?"fa fa-heart":"fa fa-heart-o"} onClick={()=>ontoggleLike(movie)} style={this.style}></li>
            </React.Fragment>

        )
    }
    style = {
        cursor: 'pointer'
    }
}

export default Like;
