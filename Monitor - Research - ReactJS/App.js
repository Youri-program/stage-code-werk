import './App.css';
import React, { Component} from 'react';


   function App() {
    ConsoleErrorCatcher();
    WindowsErrorEvent();
    return (
      //loads the class button
      <Buttons></Buttons>
    );
  }

  //catches all the console errors/warns/logs 
   function ConsoleErrorCatcher () {
    console.error = error => {
       if(error instanceof Error)  {
        alert(error)
       }
       alert(error);
    }
    console.warn = warn => { 
      if(warn instanceof Error)  {
        throw warn;
      }
      alert(warn)
    }
  console.log = log => { 
    if(log instanceof Error)  {
      alert(log)
    }
  }
}
  
  //this function uses the window.addEventListener to catch errors.
  //you can also use the window.onerror to catch the errors if you uncomment it
  function WindowsErrorEvent(){
    //window.onerror = function(msg, src, lineno, colno, error) { alert("error: " + error + " msg: " + msg + "src");  }
    // window.onerror("error", (event) => {
    //     console.log(event.message);
    // });
    window.addEventListener("error", (event) => {
      alert("error happend");
      event.error.hasBeenCaught = true;
    }); 
  }
  //exports the app function
export default App;
//the class that renders all the buttons that create the errors
export class Buttons extends Component {

  render() {
    return(
      <div style={{margin: "50px"}}>
        <button onClick={this.error1}>error console</button>
        <button onClick={this.error2}>error throw</button>
        <button onClick={this.error3}>error http</button>
        <button onClick={this.error4}>error Uncaught RangeError</button>
        <button onClick={this.error5}>error TypeError: ‘undefined’ is not an object</button>
        <button onClick={this.error6}>error unknown: Script error (nog niet catchable)</button>
      </div>
    )
    }
  // error console button calls this function. it creates a simple console errors 
  error1(){
    console.error("error1");
  }
  //error throw button calls this function. it throws a simple error
  error2(){
   throw new Error("error 2");
  }
  //error http button calls this function
  //it tries to fetch data from an url and this returns an error code
  error3(){
    fetch("http://httpstat.us/500")
    .then(function(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    }).then(function(response) {
        console.log("ok");
    }).catch(function(error) {
        console.log(error);
    });
  }
  //error Uncaught RangeError calls this button 
  //creates an out of range error
  error4(){
    var arr = new Array(1)
    const recurfn = (arr) => {
      arr[0] = new Array(1)
      recurfn(arr[0])
    }
    recurfn(arr)
  }
  //error TypeError: ‘undefined’ is not an object button calls this function
  //tries to call a function call from A Var
  error5(){
    var func
    func.call()
  }
  //error unknown: Script error (nog niet catchable) calls this function
  //tries to fetch something that doesnt exist
  error6(){
    fetch("http://google.com/json.json")
  }
}