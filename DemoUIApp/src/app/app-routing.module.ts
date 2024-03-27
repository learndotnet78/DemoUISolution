import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomerDetailsComponent } from './customer-details/customer-details.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { HomeComponent } from './home/home.component';
import { CustomerDetailFormComponent } from './customer-details/customer-detail-form/customer-detail-form.component';
import { LoginComponent } from './login/login.component';
import { canActivateGuard } from './guards/auth-guard.service';

const routes: Routes = [
  {
    path :'customer',
    component : CustomerDetailsComponent,
    canActivate : [canActivateGuard]
  },
  {
    path :'order',
    component : OrderDetailsComponent,
    canActivate : [canActivateGuard]
  },
  {
    path :'home',
    component : HomeComponent,
    canActivate : [canActivateGuard]
  },
  {
    path :'addCustomer',
    component : CustomerDetailFormComponent,
    canActivate : [canActivateGuard]
  },
  { 
    path: 'addCustomer/:customerID/edit', 
    component: CustomerDetailFormComponent,
    canActivate : [canActivateGuard]
  },
  {
    path :'login',
    component : LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { 

}
