import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/services/post.service';
import { MessageService } from 'src/app/services/message.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-subforum',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateSubforumComponent implements OnInit {

  subforumName : string
  description : string

  constructor(private postsService : PostService,
              private messageService : MessageService,
              private router : Router) { }

  ngOnInit(): void {
  }

  createSubforum() {
    this.postsService.createSubforum(this.subforumName, this.description).subscribe(response => {
      this.messageService.showMessage("Subforum " + this.subforumName + " criado com sucesso!")
      this.postsService.subforumCreated$.next(true)
      this.router.navigate(['b/'+this.subforumName])
    }, error => {
      if(error.status == 409) {
        this.messageService.showMessage("Já existe um subforum com esse nome!")
      }
    })
  }

}
