import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './views/home/home.component'
import { SubComponent } from './views/sub/sub.component'
import { LoginViewComponent } from './views/login/login.component'
import { RegisterViewComponent } from './views/register/register.component'
import { PostViewComponent } from './views/post/post.component';
import { NewSubComponent } from './views/sub/new-sub/new-sub.component';

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
    path: "b/post/:id",
    component: PostViewComponent
  },
  {
    path: "b",
    component: NewSubComponent
  },
  {
    path : "login",
    component: LoginViewComponent
  },
  {
    path: "register",
    component: RegisterViewComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
