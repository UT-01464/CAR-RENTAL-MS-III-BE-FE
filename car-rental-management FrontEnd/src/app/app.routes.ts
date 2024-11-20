import { Routes } from '@angular/router';
import { LoginComponent } from './User/login/login.component';
import { RegisterComponent } from './User/register/register.component';
import { NavBarComponent } from './LandingPage/nav-bar/nav-bar.component';


export const routes: Routes = [
    {path:'login',component:LoginComponent},
    {path:"register",component:RegisterComponent},

    {path:"nav",component:NavBarComponent},
];
