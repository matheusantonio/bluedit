import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-new-reply',
  templateUrl: './new-reply.component.html',
  styleUrls: ['./new-reply.component.css']
})
export class NewReplyComponent implements OnInit {

  currentUser : string
  @Input() postId : string

  constructor(private authService : AuthService) { }

  ngOnInit(): void {

    this.authService.currentUser().subscribe(user => {
      this.currentUser = user.userName
    }, error => {
      this.currentUser = null
    })

  }

}
