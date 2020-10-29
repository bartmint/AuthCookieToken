import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})

export class NavMenuComponent implements OnInit{
  auth: any = {}
  isExpanded = false;
  model: any = {};

  

  constructor(private AccountService: AccountService, private http: HttpClient) { }
  ngOnInit(): void {
    this.http.get("http://localhost:5001/api/account").subscribe(x => {
      this.auth = x
    });
    }
  login() {
    this.AccountService.login(this.model).subscribe(response => {
      console.log(response);
    })

    
  }

}
