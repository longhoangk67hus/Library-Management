import React from 'react';
import './App.css';
import ApiTestComponent from './components/ApiTestComponent';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Library Management System</h1>
        <p>API Testing Interface</p>
      </header>
      <main>
        <ApiTestComponent />
      </main>
    </div>
  );
}

export default App;