import './style.css'
import javascriptLogo from './javascript.svg'
import viteLogo from '/vite.svg'
import { setupCounter } from './counter.js'

document.querySelector('#app').innerHTML = `
  <div>
    <a href="https://vite.dev" target="_blank">
      <img src="${viteLogo}" class="logo" alt="Vite logo" />
    </a>
    <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript" target="_blank">
      <img src="${javascriptLogo}" class="logo vanilla" alt="JavaScript logo" />
    </a>
    <h1>Hello Vite!</h1>
    <div class="card">
      <button id="counter" type="button"></button>
    </div>
    <p class="read-the-docs">
      Click on the Vite logo to learn more
    </p>
  </div>
`

setupCounter(document.querySelector('#counter'))

import { HubConnectionBuilder } from '@aspnet/signalr'

let connection = new HubConnectionBuilder()
  .withUrl("http://localhost:5170/chat")
  .build();

const sendMessage = (text) => {
  connection.invoke("send", { "time": new Date(), "message": text})
}

connection.on("send", data => {
  console.log(data);
});

await connection.start()
  .then(() => {
    connection.invoke("send", { "time": new Date(), "message": "Hello [v1]"})
    document.querySelector('#counter')
      .addEventListener('click', () => sendMessage(document.querySelector('#counter').innerHTML))
  });
