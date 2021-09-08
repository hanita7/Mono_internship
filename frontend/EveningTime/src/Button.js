import React from "react";

class Button extends React.Component {
    constructor(props) {
      super(props);
      this.state = { activity: null};
    }
  
    activities = ["Movie", "Bath", "Mau Mau", "Battleship", "Massage"];
  
    handleClick = () => {
      this.setState(
        { activity: this.activities[Math.floor(Math.random() * this.activities.length)] }
        );
    };
  
    render() {
      return (
        <div>
          <button onClick = {this.handleClick}>Activity</button>
          {this.state.activity}
        </div>
      );
    }
}

export default Button;