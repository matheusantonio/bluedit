import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './components/template/header/header.component';
import { HomeComponent } from './views/home/home.component';

import { MatToolbarModule } from '@angular/material/toolbar'
import { MatCardModule } from '@angular/material/card';
import { PostPreviewComponent } from './components/home/post-preview/post-preview.component';
import { SubComponent } from './views/sub/sub.component';
import { PostComponent } from './components/sub/post/post.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { ReplyComponent } from './components/post/reply/reply.component';
import { PreviewComponent } from './components/post/preview/preview.component'

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    HomeComponent,
    PostPreviewComponent,
    SubComponent,
    PostComponent,
    LoginComponent,
    RegisterComponent,
    ReplyComponent,
    PreviewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatCardModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
