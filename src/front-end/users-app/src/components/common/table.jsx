import React, { Component } from "react";
import TableHeader from "./tableHeader";
import TableBody from "./tableBody";
import Pager from "./pager";
import { getCurrentPageItems } from "./pager";

class Table extends Component {
  state = {
    currentPageItems: [],
    currentPageIndex: 1,
    pageSize: 3
  };
  handlePagechanged = page => {
    this.setState({ currentPageIndex: page });
  };

  render() {
    const { currentPageIndex, pageSize } = this.state;

    const { data, columns, onDelete } = this.props;
    const currentPageItems = getCurrentPageItems(
      data,
      currentPageIndex,
      pageSize
    );
    const totalItemsCount = data.length;
    return (
      <React.Fragment>
        <table className="table">
          <TableHeader columns={columns} />
          <TableBody
            columns={columns}
            data={currentPageItems}
            onDelete={onDelete}
          />
        </table>
        <Pager
          currentPageIndex={currentPageIndex}
          pageSize={pageSize}
          totalItemsCount={totalItemsCount}
          onPageChanged={page => this.handlePagechanged(page)}
        />
      </React.Fragment>
    );
  }
}

export default Table;
