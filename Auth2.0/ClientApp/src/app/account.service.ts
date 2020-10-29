import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = "http://localhost:5001/api/account/login";

  constructor(private http: HttpClient) { }


  login(model: any) {
    return this.http.post(this.baseUrl, model).pipe(
      map((user: any) => {
        console.log(user)
      }, error => {
          console.log(error);
      }));

  }
}
