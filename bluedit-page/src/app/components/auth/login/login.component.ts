import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { AuthService } from '../../../services/auth.service';
import { MessageService } from '../../../services/message.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginUser : {
    username : string
    password : string
  } = {
    username: '',
    password: ''
  }

  constructor(private authService : AuthService,
              private messageService : MessageService,
              private router : Router) { }

  ngOnInit(): void {
  }

  requestLogin() {

    this.authService.login(this.loginUser.username, this.loginUser.password)
      .subscribe( response => {
        localStorage.setItem('app-token', response.token)
        localStorage.setItem('app-user', response.user)
        this.router.navigate([''])
        this.messageService.showMessage('Login realizado com sucesso!')
      }, error => {
        console.log(error)
        error.error.forEach(element => {
          this.messageService.showMessage(element.description)
        });
      })
    }
}
