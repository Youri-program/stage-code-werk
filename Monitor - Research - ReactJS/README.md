
Orginally in React Version 18.2.0
# Try-Catch
\+ Catches all errors that are in the try-catch statement.\
\+ Useful for debugging.\
\+ Useful for manually sending errors to the API.\
\- Can't be used as a global error handler.

## How to use
```javascript
try {
//code
}
catch(error) {
  if(error instanceof Error) {
    // Code that runs when an error occurs
  }
}
```

# Window.eventlistener/ Window.onerror
\+ Can catch thrown errors.\
\+ Can get a lot of information out of the error.\
\+ With addEventListener you can use different events to catch data to send to the API.\
\- Doesn't catch errors that are directly sent to the console.

## How to use
You can use `window.onerror` or `window.addEventListener` to catch errors that are thrown. With `window.addEventlistener` you can also use other events.

```javascript
window.onerror = function(msg, src, linenum, colnum, error) 
{
  // Event, fires when the window breaks (error)
}

// Basically same as above
window.addEventListener('error', function (e) { })
```

# Console.error = error/warn/log
\+ Catches most errors in the console error/warn/log\
\- Some console errors it doesn't catch. 

## How to use
You only need to use this code once and this will be run every time console.error/log/warn is used.

```javascript
// Errors and warnings will not be passed down as logs.

console.error = error => {
  // Event, fires when console receives an error
}

console.warn = warn => {
  // Event, fires when console receives a warning
}

console.log = log => {
  // Event, fires when console receives a log
}
```

# Error boundary 
\+ Catches different types of errors.\
\- Only catches errors that are created when the page loads/renders.\
\- Doesn't catch errors that come from button press events.

## How to use

```javascript
import { Component } from 'react';


// Create an ErrorBoundary class
export class ErrorBoundary extends Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false };
  }

  // Checks whether there is an error in a component,
  // and sets hasError to true if so.
  componentDidCatch(error, info) { 
    this.setState({ hasError: true });
    alert(error, info);
  }

  // Renders an error page if there was an error, else renders the component normally
  render() {
    if (this.state.hasError) {
      return <h1>Something went wrong.</h1>;
    }
    return this.props.children;
  }
}
```

In the `index.js` add the error boundary to the `root.render` like this. 

```javascript
<ErrorBoundary>
  <App>
    // Add all the components you want to use to render the root between the <ErrorBoundary> </ErrorBoundary>
  <App/>
<ErrorBoundary/>
```