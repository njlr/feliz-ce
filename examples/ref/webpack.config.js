const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
  entry: './App.fs.js',
  mode: "development",
  plugins: [
    new HtmlWebpackPlugin(),
  ],
};
