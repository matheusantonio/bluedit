import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { PostPreview } from '../models/post.preview.model'
import { Post } from '../models/post.model'
import { SubForum } from '../models/subforum.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  baseUrl = "https://localhost:5002/bluedit"

  constructor(private http: HttpClient) { }

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

  topSubForums() : Observable<string[]> {
    const url = this.baseUrl + '/subforum/top'

    return this.http.get<string[]>(url)
  }



}
