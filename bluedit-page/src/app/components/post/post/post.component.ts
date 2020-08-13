import { Component, OnInit } from '@angular/core';
import { Post } from '../post.model';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  post : Post = {
    id : "",
    title : "",
    tags : [],
    subForum : "",
    author : "",
    content : "",
    replies : [],
    time : null,
    upvotes : 0
  }

  constructor() { }

  ngOnInit(): void {
  }

}
