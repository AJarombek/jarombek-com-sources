# Commands to setup the project on PC.
# Author: Andrew Jarombek
# Date: 10/3/2020

# Install Python 3.8 using Chocolatey
choco install python --version=3.8.0 -y

pip install pipenv
pipenv --version

pipenv install
pipenv shell