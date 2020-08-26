import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service'


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  currentUser : string

  constructor(private authService : AuthService) {
    
  }

  ngOnInit(): void {

    this.checkCurrentUser()

    this.authService.loged$.subscribe(loged => {
      if(loged && this.currentUser == null){
        console.log("Usuário logado")
        this.checkCurrentUser()
      } else {
        console.log("Usuário deslogado")
        this.currentUser=null
      }
    })
  }

  checkCurrentUser() {
    this.authService.currentUser().subscribe(response => {
      this.currentUser = response.userName
    }, error => {
      this.authService.loged$.next(false)
      console.log(error)
    })
  }

  logout() {
    this.authService.logout()
  }

}
