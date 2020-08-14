import { Component, OnInit } from '@angular/core';
import { PostService } from '../../services/post.service'
import { PostPreview } from '../../models/post.preview.model'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  posts : PostPreview[]

  constructor(private postService : PostService) { }

  ngOnInit(): void {
    this.postService.readPosts().subscribe(posts => {
      
      this.posts = posts
    })
  }

}
