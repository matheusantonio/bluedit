import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './views/home/home.component'
import { SubComponent } from './views/sub/sub.component'
import { LoginComponent } from './views/login/login.component'
import { RegisterComponent } from './views/register/register.component'

const routes: Routes = [
  {
    path: "",
    component: HomeComponent
  },
  {
    path: "b/:name",
    component: SubComponent
  },
  {
    path : "login",
    component: LoginComponent
  },
  {
    path: "register",
    component: RegisterComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
