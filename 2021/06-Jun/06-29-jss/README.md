### Overview

Demo of JSS without any frontend framework or library.

### Commands

```bash
# Use NodeJS version 14.*
nvm install lts/fermium
npm install yarn -g

# Install project dependencies
yarn

# Bundle the JavaScript with webpack
nvm use lts/fermium
yarn build
yarn start
```

### Files

| Filename                 | Description                                                                |
|--------------------------|----------------------------------------------------------------------------|
| `index.html`             | HTML entrypoint for the demo application.                                  |
| `index.js`               | JavaScript source code for the JSS demo application.                       |
| `server.js`              | NodeJS/Express code for a server that hosts the JSS demo.                  |
| `package.json`           | Entry point for the npm application.  Contains dependency definitions.     |
| `webpack.config.js`      | Configuration file for the Webpack bundler.                                |
| `.yarn.lock`             | Yarn dependency versions.                                                  |
