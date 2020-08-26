import { Component, OnInit, Input } from '@angular/core';
import { PostService } from 'src/app/services/post.service';
import { CreatePost } from 'src/app/models/post.model';
import { MessageService } from 'src/app/services/message.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  @Input() subForumName : string = null
  post : CreatePost = {
    title : "",
    content : ""
  }

  constructor(private postService : PostService,
              private messageService : MessageService,
              private router : Router) { }

  ngOnInit(): void {
  }

  createPost() {
    this.postService.createPost(this.post.title, this.post.content, this.subForumName).subscribe(response => {
      console.log(response)
      this.messageService.showMessage("Post criado com sucesso!")
      
      this.router.navigate(['b/post/' + response.id])

    }, error => {
      console.log(error)
      this.messageService.showMessage(error.message)
    })
  }

}
