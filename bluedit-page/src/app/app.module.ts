import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './components/template/header/header.component';
import { HomeComponent } from './views/home/home.component';

import { HttpClientModule } from '@angular/common/http'

import { FormsModule } from '@angular/forms'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input'
import { MatSnackBarModule } from '@angular/material/snack-bar'
import { MatButtonModule } from '@angular/material/button'
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { MatDialogModule } from '@angular/material/dialog';

import { SubComponent } from './views/sub/sub.component';
import { PostComponent } from './components/post/post/post.component'
import { PostViewComponent } from './views/post/post.component'
import { LoginViewComponent } from './views/login/login.component';
import { RegisterViewComponent } from './views/register/register.component';
import { ReplyComponent } from './components/post/reply/reply.component';
import { PreviewComponent } from './components/post/preview/preview.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { CreateComponent } from './components/post/create/create.component';
import { NewReplyComponent } from './components/post/new-reply/new-reply.component';
import { NewSubComponent } from './views/sub/new-sub/new-sub.component'
import { CreateSubforumComponent } from './components/subforum/create/create.component'

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    SubComponent,
    PostComponent,
    LoginViewComponent,
    RegisterViewComponent,
    ReplyComponent,
    PreviewComponent,
    PostViewComponent,
    LoginComponent,
    RegisterComponent,
    CreateComponent,
    NewReplyComponent,
    NewSubComponent,
    CreateSubforumComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatCardModule,
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule,
    FormsModule,
    MatButtonModule,
    MatChipsModule,
    MatIconModule,
    MatExpansionModule,
    MatSelectModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
