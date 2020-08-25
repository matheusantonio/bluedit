import { Component, OnInit } from '@angular/core';
import { SubForum } from 'src/app/models/subforum.model';
import { ActivatedRoute } from '@angular/router';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-sub',
  templateUrl: './sub.component.html',
  styleUrls: ['./sub.component.css']
})
export class SubComponent implements OnInit {

  subForum : SubForum

  constructor(private route : ActivatedRoute,
              private postService : PostService) { }

  ngOnInit(): void {
    const subForumName = this.route.snapshot.paramMap.get('name')
    this.postService.readSubForum(subForumName).subscribe(response => {
      this.subForum = response
    }, error => {
      console.log(error)
    })
  }

}
