import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';
import { Router } from '@angular/router';
import { timer } from 'rxjs';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'app-new-reply',
  templateUrl: './new-reply.component.html',
  styleUrls: ['./new-reply.component.css']
})
export class NewReplyComponent implements OnInit {

  currentUser : string
  @Input() postId : string = null
  @Input() replyId : string = null

  replyContent : string

  constructor(private authService : AuthService,
              private postService : PostService,
              private router : Router) { }

  ngOnInit(): void {
    this.setUser()

    this.authService.loged$.subscribe(value => {
      this.setUser()
    })
  }

  setUser() {
    this.authService.currentUser().subscribe(user => {
      this.currentUser = user.userName
    }, error => {
      this.currentUser = null
    })
  }

  createReply() {

    const postId = this.replyId == null ? this.postId : null

    this.postService.createReply(this.replyContent, postId, this.replyId).subscribe(response => {

      this.router.navigate(['/']).then(() => this.router.navigate(['/b/post/' + this.postId]))
      

    }, error => {
      console.log(error)
    })
  }

}
