/**
 * Basic express server for hosting the HTML/JavaScript code demonstrating JSS.
 * @author Andrew Jarombek
 * @since 6/26/2021
 */

const express = require('express');

const app = express();
const port = 9000;

app.use(express.static(__dirname));

app.listen(port);
console.log('Server listening on port ' + port);
