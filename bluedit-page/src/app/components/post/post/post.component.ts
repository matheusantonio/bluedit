import { Component, OnInit, Input } from '@angular/core';
import { Post } from '../../../models/post.model';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  @Input() post : Post

  constructor(private postService : PostService) { }

  ngOnInit(): void {
  }

  upvote(isUp : boolean)
  {
    this.postService.upvote(this.post.id, isUp).subscribe(response => {
      this.post.upvotes = response.updatedCount
      this.post.userVote = response.isUp
    }, error => {
      console.log(error)
    })
  }

}
