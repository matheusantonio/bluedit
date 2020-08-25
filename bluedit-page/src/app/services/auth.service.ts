import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable } from 'rxjs';
import { AuthResponse, CurrentUser } from '../models/auth.model'

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = "https://localhost:5002/bluedit"

  constructor(private http : HttpClient) { }


  login(username : string, password : string) : Observable<AuthResponse> {

    const url = this.baseUrl + '/user/login'

    return this.http.post<AuthResponse>(url, {
      "UserName" : username,
      "Password" : password
    })
  }

  register(username : string, password : string, email : string) : Observable<AuthResponse> {
    const url = this.baseUrl + '/user/register'

    return this.http.post<AuthResponse>(url, {
      "UserName" : username,
      "Password" : password,
      "Email" : email
    })
  }

  logout() : void {
    localStorage.removeItem('app-token')
    localStorage.removeItem('app-user')
  }


  currentUser() : Observable<CurrentUser> {
    const url = this.baseUrl + '/user/current'

    return this.http.get<CurrentUser>(url, this.getAuthorizationHeader())
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
