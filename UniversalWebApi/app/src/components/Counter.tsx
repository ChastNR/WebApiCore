import React from "react";
import { observable, action, computed } from "mobx";
import { observer } from "mobx-react";

@observer
export class Counter extends React.Component {
  @observable counter = 0;

  @action
  handleIncrement = () => this.counter++;

  @action
  handleDecrement = () => this.counter--;

  @computed
  get multiply() {
    return this.counter < 5
      ? Math.pow(this.counter, 2)
      : Math.pow(this.counter, 5);
  }

  render() {
    return (
      <div>
        <div>{this.counter}</div>
        <div>{this.multiply}</div>

        <button onClick={this.handleDecrement}>-1</button>
        <button onClick={this.handleIncrement}>+1</button>
      </div>
    );
  }
}
