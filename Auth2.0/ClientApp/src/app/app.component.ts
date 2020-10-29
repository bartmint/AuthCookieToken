import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { AccountService } from './account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent implements OnInit {
  data: any;
  constructor(private http: HttpClient) { }
  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.http.get("http://localhost:5001/api/account").subscribe(response => {
      this.data = response;
    }, error => {
      console.log(error);
    })
   }
}
  

