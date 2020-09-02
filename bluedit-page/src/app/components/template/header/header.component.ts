import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service'
import { ThrowStmt } from '@angular/compiler';
import { Router } from '@angular/router';
import { SubforumInfo } from 'src/app/models/subforum.model';
import { PostService } from 'src/app/services/post.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  currentUser : string
  currentForum : string
  currentForumIcon : string

  subforumList : SubforumInfo[]

  constructor(private authService : AuthService,
              private postService : PostService,
              private router : Router) {

  }

  ngOnInit(): void {

    this.checkCurrentUser()

    this.checkSubforumList()

    this.authService.loged$.subscribe(loged => {
      if(loged &&  this.currentUser==null) this.checkCurrentUser()
      else if(!loged) this.currentUser = null
    })

    this.postService.subforumCreated$.subscribe(() => {
      this.checkSubforumList()
    })
  }

  toRoute(route : string) {
    if(route == '') this.currentForum = 'home'
    else this.currentForum=route

    this.router.navigate(['']).then(() => {
      this.router.navigate([route])
    })
    
  }

  checkSubforumList() {
    this.postService.allSubforums().subscribe(subforumList => {
      this.subforumList = subforumList
    })
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
