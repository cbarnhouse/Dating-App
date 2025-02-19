import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_models/member';
import { AccountService } from './account.service';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);
  baseUrl = environment.apiURL;
  members = signal<Member[]>([]);

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + "users").subscribe({
     next: response => this.members.set(response)
    })
  }

  getMember(username: string) {
    if (this.members.length > 0) {
      const member = this.members().find(x => x.username === username);
      return of(member)
    }

    return this.http.get<Member>(this.baseUrl + "users/" + username);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + "users", member).pipe(
      tap(() => {
        this.members.update(members => members.map(m => m.username === member.username ? member : m))
      })
    );
  }
}
