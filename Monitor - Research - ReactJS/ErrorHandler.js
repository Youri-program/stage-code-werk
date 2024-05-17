import React, { Component} from 'react';

export class ErrorBoundary extends Component {
    constructor(props) {
      super(props);
      this.state = { hasError: false };
    }
    // Update state so the next render will show the fallback UI.    
    static getDerivedStateFromError(error) {    
      return { hasError: true }; 
    }
    //this catches all the errors that happend in components that are in the errorboundary
    componentDidCatch(error, errorInfo) {   
      alert(error, errorInfo);  
    }

    //returns a <h1>something went wrong </h1> when there is an error else it just loads everything like normal
    render() {    
      if (this.state.hasError) {       
        return <h1>Something went wrong.</h1>;    
      }
      return this.props.children; 
    }
  }