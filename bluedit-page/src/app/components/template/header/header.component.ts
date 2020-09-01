import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service'
import { ThrowStmt } from '@angular/compiler';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  currentUser : string
  currentForum : string
  currentForumIcon : string

  constructor(private authService : AuthService,
              private router : Router) {
    
  }

  ngOnInit(): void {

    this.checkCurrentUser()

    this.authService.loged$.subscribe(loged => {
      if(loged &&  this.currentUser==null) this.checkCurrentUser()
      else if(!loged) this.currentUser = null
    })
  }

  toRoute(route : string) {
    this.router.navigate([route])
  }

  checkCurrentUser() {
    this.authService.currentUser().subscribe(response => {
      this.authService.loged$.next(true)
      this.currentUser = response.userName
    }, error => {
      this.authService.loged$.next(false)
    })
  }

  logout() {
    this.authService.logout()
  }

}
