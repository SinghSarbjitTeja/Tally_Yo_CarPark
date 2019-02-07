import React, { Component } from 'react';

export class Home extends Component {
  displayName = Home.name

  render() {
    return (
      <div>
        <h1>Hello, Welcome to 'Tall-yo' car park!</h1>
        <ul>
                <li><strong>Please click on Car Park Bill to generate your Bill</strong></li>
        </ul>
      </div>
    );
  }
}
