import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { AuthService } from '../../../services/auth.service';
import { MessageService } from '../../../services/message.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerUser : {
    username : string
    email : string
    emailConfirmation : string
    password : string
    passwordConfirmation : string
  } = {
    username : "",
    email : "",
    emailConfirmation : "",
    password : "",
    passwordConfirmation : ""
  }

  constructor(private authService : AuthService,
              private messageService : MessageService,
              private router : Router) { }

  ngOnInit(): void {
  }

  requestRegister() {

    if(this.registerUser.email !== this.registerUser.emailConfirmation){
      this.messageService.showMessage("O email precisa ser o mesmo")
    }
    if(this.registerUser.password !== this.registerUser.passwordConfirmation) {
        this.messageService.showMessage("As senhas não coincidem")
    }

    this.authService.register(
      this.registerUser.username, 
      this.registerUser.password, 
      this.registerUser.email).subscribe(response => {
        localStorage.setItem('app-token', response.token)
        localStorage.setItem('app-user', response.user)
        this.router.navigate([''])
        this.messageService.showMessage('Usuário cadastrado com sucesso!')
      }, error => {
        console.log(error)
        error.error.forEach(element => {
          this.messageService.showMessage(element.description)
        });
      })
  }
}
