#!/usr/bin/env bash

# Setup Angular project
# Author: Andrew Jarombek
# Date: 1/16/2019

npm install --save-dev @angular/cli@latest
ng new change-detection

ng set --global packageManager=yarn
cd change-detection/
ng serve

# Alter the angular app to use scss for stylesheets
ng config schematics.@schematics/angular:component.styleext scss

ng generate component cat-pic-i
ng generate component cat-pic-ii