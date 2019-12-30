/**
 * Setup Webpack for bundling JavaScript and CSS files.
 * @author Andrew Jarombek
 * @since 12/30/2019
 */

const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    entry: {
        app: path.join(__dirname, "src/index.js"),
        styles: path.join(__dirname, "src/index.css")
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader'
                }
            },
            {
                test: /\.css$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader
                    },
                    {
                        loader: 'css-loader'
                    }
                ]
            },
            {
                test: /\.html$/,
                use: {
                    loader: "html-loader"
                }
            }
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: "[name].css"
        })
    ],
    output: {
        path: path.join(__dirname, "dist/"),
        filename: '[name].js',
        publicPath: '',
        libraryTarget: 'umd'
    }
};
