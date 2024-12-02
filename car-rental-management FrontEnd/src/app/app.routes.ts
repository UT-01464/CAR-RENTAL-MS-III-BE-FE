import { Routes } from '@angular/router';
import { LoginComponent } from './User/login/login.component';
import { NavBarComponent } from './LandingPage/nav-bar/nav-bar.component';
import { UserpageComponent } from './User/userpage/userpage.component';
import { AllCarsComponentComponent } from './LandingPage/all-cars-component/all-cars-component.component';
import { DashboardComponent } from './Admin/dashboard/dashboard.component';
import { ListCustomerComponent } from './Admin/Customers/list-customer/list-customer.component';
import { CustomerReportsComponent } from './Admin/Reports/customer-reports/customer-reports.component';
import { ListCarsComponent } from './Admin/Cars/list-cars/list-cars.component';
import { AddCarComponent } from './Admin/Cars/add-car/add-car.component';


export const routes: Routes = [
    {path:'login',component:LoginComponent},
   

    {path:"navbar",component:NavBarComponent},

    
    { path: 'userpage', component: UserpageComponent }, 
   
    { path: '', redirectTo: '/login', pathMatch: 'full' },
    {path:'all-cars',component:AllCarsComponentComponent},

    {path:'admin',component:DashboardComponent },
    {path:'customer',component:ListCustomerComponent},
    {path:'reports',component:CustomerReportsComponent},
    {path:'cars',component:ListCarsComponent},
    {path:'addCar',component:AddCarComponent}


];
