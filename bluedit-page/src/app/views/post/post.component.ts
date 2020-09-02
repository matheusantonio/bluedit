import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/post.model';
import { PostService } from '../../services/post.service'
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-post-view',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostViewComponent implements OnInit {

  post : Post

  constructor(private route : ActivatedRoute,
              private postService : PostService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get("id")
    this.postService.postById(id).subscribe(post => {
      this.post = post
      this.postService.userUpvote(this.post.id).subscribe(userVote => {
        this.post.userVote = userVote
      }, error => {
        console.log(error)
      })
    })
  }

}
