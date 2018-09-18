import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import {RouterModule, Routes} from "@angular/router";
import { HomeComponent } from './home/home.component';
import { PicturesComponent } from './pictures/pictures.component';

/**
 * Module for the Lifecycle Demo Application
 * @author Andrew Jarombek
 * @since 9/17/2018
 */

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {path: 'pictures', component: PicturesComponent},
    {path: '**', redirectTo: ''}
];

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        PicturesComponent
    ],
    imports: [
        BrowserModule,
        RouterModule.forRoot(routes)
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
