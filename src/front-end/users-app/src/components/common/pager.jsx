import React, { Component } from "react";
import _ from "lodash";
class Pager extends Component {
  render() {
    return (
      <React.Fragment>
        <nav aria-label="Page navigation example">
          <ul className="pagination">
            {/* <li className="page-item"><a className="page-link" href="/ffg">Previous</a></li> */}
            {this.getPagerElements()}
            {/* <li className="page-item"><a className="page-link" href="/fgfgf">Next</a></li> */}
          </ul>
        </nav>
      </React.Fragment>
    );
  }

  getPagerElements() {
    const { totalItemsCount, pageSize, currentPageIndex } = this.props;
    const pagesCount = Math.ceil(totalItemsCount / pageSize);
    if (pagesCount === 1) return null;
    const pageArray = _.range(1, pagesCount + 1);
    return pageArray.map(page => (
      <li
        key={page}
        className={currentPageIndex === page ? "page-item active" : "page-item"}
      >
        <button
          className="page-link"
          onClick={() => this.props.onPageChanged(page)}
        >
          {page}
        </button>
      </li>
    ));
  }
}
export default Pager;

export function getCurrentPageItems(totalItems, currentPageIndex, pageSize) {
  return totalItems.slice(
    currentPageIndex * pageSize - pageSize,
    currentPageIndex * pageSize
  );
}
