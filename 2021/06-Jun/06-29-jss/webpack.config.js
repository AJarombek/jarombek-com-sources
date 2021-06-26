/**
 * Basic webpack configuration for bundling JavaScript code.
 * @author Andrew Jarombek
 * @since 4/10/2021
 */

const path = require('path');

module.exports = {
    entry: './index.js',
    mode: 'development',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'dist')
    }
};
