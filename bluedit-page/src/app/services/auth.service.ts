import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { AuthResponse } from '../models/auth.model'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = "https://localhost:5002/bluedit"

  constructor(private http : HttpClient) { }


  login(username : string, password : string) : void {

    const url = this.baseUrl + '/user/login'

    this.http.post<AuthResponse>(url, {
      "UserName" : username,
      "Password" : password
    }).subscribe( response => {
      console.log(response)
      localStorage.setItem('app-token', response.token)
      localStorage.setItem('app-user', response.user)
    }, error => {
      console.log(error)
    })

  }


  logout() : void {
    localStorage.removeItem('app-token')
  }


  currentUser() : void {
    const url = this.baseUrl + '/user/current'

    this.http.get(url, this.getAuthorizationHeader()).subscribe(response => {
      console.log(response)
    })
  }


  getAuthorizationHeader(){
    return {
      headers : new HttpHeaders({
        'Content-Type' : 'application/json',
        'Authorization' : `Bearer ${localStorage.getItem('app-token')}`
      })
    }
  }
}
