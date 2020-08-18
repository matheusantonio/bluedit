import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private authService : AuthService) { }

  ngOnInit(): void {
  }

  login() {

    this.authService.login('matheus', 'matheus')

  }

  current () {
    this.authService.currentUser()
  }

  logout() {
    this.authService.logout()

  }



}
