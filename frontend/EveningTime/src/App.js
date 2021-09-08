import React from 'react';
import Button from './Button'

class DinnerForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {value : 'Sandwiches'};
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({value: event.target.value})
  }

  handleSubmit(event) {
    alert("For dinner we're having " + this.state.value);
    event.preventDefault();
  }

  render() {
    return (
      <body>
      <form onSubmit = {this.handleSubmit}>
        <label>What are we having for dinner tonight? </label>
          <select value = {this.state.value} onChange = {this.handleChange}>
            <option value = "Sandwiches">Sandwiches</option>
            <option value = "Fries">Fries</option>
            <option value = "Noodles">Noodels</option>
            <option value = "Nuggets">Nuggets</option>
            <option value = "Pancakes">Pancakes</option>
            <option value = "Pizza">Pizza</option>
            <option value = "Spaghetti">Spaghetti</option>
            <option value = "Take-out">Take-out</option>
          </select>
        <input type = "submit" value = "Submit" />
      </form>
      <Button />
      </body>
    );
  }
}

export default DinnerForm;