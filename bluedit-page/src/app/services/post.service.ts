import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { PostPreview } from '../models/post.preview.model'
import { Post } from '../models/post.model'

@Injectable({
  providedIn: 'root'
})
export class PostService {

  baseUrl = "https://localhost:5002/bluedit"

  constructor(private http: HttpClient) { }

  readPosts(subforum: string = null) : Observable<PostPreview[]> {
    
    var url = ''

    if(subforum != null) {
      url = this.baseUrl + '/subforum?name=' + subforum
    } else {
      url = this.baseUrl + '/posts'
    }
    
    return this.http.get<PostPreview[]>(url)
  }

  postById(id : string) : Observable<Post> {

    const url = this.baseUrl + '/posts/' + id

    return this.http.get<Post>(url)
  }

  topSubForums() : Observable<string[]> {
    const url = this.baseUrl + '/subforum/top'

    return this.http.get<string[]>(url)
  }



}
