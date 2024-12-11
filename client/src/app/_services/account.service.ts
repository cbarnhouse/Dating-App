import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  baseURL = "http://localhost:5001/api/";
  currentUser = signal<User | null>(null);

  login(model: any){
    return this.http.post<User>(this.baseURL + "account/login", model).pipe(
      map(user => {
        localStorage.setItem('user', JSON.stringify(user));
        this.currentUser.set(user);
      }
    ))
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }

}