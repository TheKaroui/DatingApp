import { User } from './../_models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// const httpOptions = {
//   headers: new HttpHeaders({
//     'Authorization': 'Bearer ' + localStorage.getItem('token')
//   })
// };
// the problem of this method is that it takes
// short time after login to store token on local storage on certain
// machines so the best way is doing it on bootstrap of the
// application in appmodule.ts in imports section

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'users'
    // ,httpOptions
    );
  }

  getUser(id): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'users/' + id);
  }
}
