import { Component, OnInit, Input } from '@angular/core';
import { Reply } from 'src/app/models/reply.model';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-reply',
  templateUrl: './reply.component.html',
  styleUrls: ['./reply.component.css']
})
export class ReplyComponent implements OnInit {

  @Input() postId : string
  @Input() reply : Reply
  replyOpen : boolean = false

  constructor(private postService : PostService) { }

  ngOnInit(): void {
  }

  toggleReplyForm() {
    this.replyOpen = !this.replyOpen
  }

  upvote(isUp : boolean)
  {
    this.postService.upvote(this.reply.id, isUp, true).subscribe(response => {
      this.reply.upvotes = response.updatedCount
      this.reply.userUpvote = response.isUp
    }, error => {
      console.log(error)
    })
  }

}
