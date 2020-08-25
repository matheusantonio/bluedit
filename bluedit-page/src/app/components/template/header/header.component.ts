import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service'


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  currentUser : string = null

  constructor(private authService : AuthService) { }

  ngOnInit(): void {
    
    this.authService.currentUser().subscribe(response => {
      this.currentUser = response.username
    }, error => {
      this.currentUser = null
    })

    
  }

  logout() {
    this.authService.logout()
    this.currentUser = null
  }

}
