import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable, Subject } from 'rxjs';
import { PostPreview } from '../models/post.preview.model'
import { Post } from '../models/post.model'
import { SubForum, SubforumInfo } from '../models/subforum.model';
import { UpdatedUpvotes } from '../models/upvotes.model'
import { AuthService } from './auth.service';
import { Reply } from '../models/reply.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  baseUrl = "https://localhost:5002/bluedit"

  subforumCreated$ = new Subject<boolean>() 

  constructor(private http: HttpClient,
              private authService : AuthService) { }

  readPosts() : Observable<PostPreview[]> {
    
    const url = this.baseUrl + '/posts'
    
    return this.http.get<PostPreview[]>(url)
  }

  readSubForum(subforum : string) : Observable<SubForum> {
    const url = this.baseUrl + '/subforum?name=' + subforum

    return this.http.get<SubForum>(url)
  }

  postById(id : string) : Observable<Post> {

    const url = this.baseUrl + '/posts/' + id

    return this.http.get<Post>(url)
  }

  allSubforums() : Observable<SubforumInfo[]> {
    const url = this.baseUrl + '/subforum'

    return this.http.get<SubforumInfo[]>(url)
  }

  topSubForums() : Observable<string[]> {
    const url = this.baseUrl + '/subforum/top'

    return this.http.get<string[]>(url)
  }

  createSubforum(name : string, description : string) : Observable<SubforumInfo> {
    const url = this.baseUrl + '/subforum'

    return this.http.post<SubforumInfo>(url, {
      name : name,
      description : description
    }, this.authService.getAuthorizationHeader())
  }

  createPost(title : string, content : string, tags : string[], subForum : string = null) : Observable<Post> {

    const url = this.baseUrl + '/posts'

    return this.http.post<Post>(url, {
      title: title,
      content : content,
      subForum : subForum,
      tags : tags
    }, this.authService.getAuthorizationHeader())

  }

  createReply(content : string, postId : string = null, replyId : string = null) : Observable<Reply> {

    const url = this.baseUrl + '/posts/reply'

    return this.http.post<Reply>(url, {
      content : content,
      postId : postId,
      replyId : replyId
    }, this.authService.getAuthorizationHeader())

  }

  userUpvote(postId : string) : Observable<boolean> {

    const url = this.baseUrl + '/posts/upvotes/' + postId

    return this.http.get<boolean>(url, this.authService.getAuthorizationHeader())
  }

  upvote(postId : string, isUp : boolean, isReply : boolean = false) : Observable<UpdatedUpvotes> {

    const url = this.baseUrl + '/posts/upvotes'

    return this.http.post<UpdatedUpvotes>(url, {
      postId : postId,
      isUp : isUp,
      isReply : isReply
    }, this.authService.getAuthorizationHeader())
  }

}
