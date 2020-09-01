import { Component, OnInit, Input } from '@angular/core';
import { PostService } from 'src/app/services/post.service';
import { CreatePost } from 'src/app/models/post.model';
import { MessageService } from 'src/app/services/message.service';
import { Router } from '@angular/router';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material/chips';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  @Input() subForumName : string = null
  post : CreatePost = {
    title : "",
    content : "",
    tags : []
  }

  currentUser : string = null

  constructor(private postService : PostService,
              private messageService : MessageService,
              private authService : AuthService,
              private router : Router) { }

  ngOnInit(): void {
    
    this.setUser()

    this.authService.loged$.subscribe(value => {
      this.setUser()
    })

  }

  setUser() {
    this.authService.currentUser().subscribe(user => {
      this.currentUser = user.userName
    }, error => {
      this.currentUser = null
    })
  }

  createPost() {
    this.postService.createPost(this.post.title, this.post.content, this.post.tags, this.subForumName).subscribe(response => {
      console.log(response)
      this.messageService.showMessage("Post criado com sucesso!")
      
      this.router.navigate(['b/post/' + response.id])

    }, error => {
      if(error.status == 401) {
        this.messageService.showMessage("Sua sessão expirou, autentique-se para continuar.")
        this.authService.loged$.next(false)
      }
    })
  }

  add(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    if ((value || '').trim()) {
      this.post.tags.push(value.trim());
    }

    if (input) {
      input.value = '';
    }
  }

  remove(tag: string): void {
    const index = this.post.tags.indexOf(tag);

    if (index >= 0) {
      this.post.tags.splice(index, 1);
    }
  }

}
