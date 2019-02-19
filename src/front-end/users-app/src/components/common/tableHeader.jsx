import React, { Component } from 'react';

class TableHeader extends Component {
    state = {}
    render() {
        const { columns, sortBy, onSort } = this.props;
        return (
            <thead>
                <tr>
                    {
                        columns.map(column =>
                            <th key={column.path}>{column.title}</th>
                        )
                    }
                </tr>
            </thead>
        );
    }
}

export default TableHeader;     
