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
  }

  upvote(isUp : boolean)
  {
    this.postService.upvote(this.post.id, isUp, true).subscribe(() => {
      if(isUp) this.post.upvotes++
      else this.post.upvotes--
    }, error => {
      console.log(error)
    })
  }

}
