import { Component, OnInit, Input } from '@angular/core';
import { PostPreview } from '../../../models/post.preview.model'
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-preview',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.css']
})
export class PreviewComponent implements OnInit {

  @Input() post : PostPreview

  constructor(private postService : PostService) { }

  ngOnInit(): void {
    this.postService.userUpvote(this.post.id).subscribe(userVote => {
      this.post.userVote = userVote
    }, error => {
      console.log(error)
    })
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
