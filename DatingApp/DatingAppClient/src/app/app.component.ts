import { Component, NgModule, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;

  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.getUsers();
  }

  private getUsers() {
    this.http.get('https://localhost:44320/api/users').subscribe(response => {
      this.users = response;
      console.log(this.users);
    }, error => {
      console.log(error);
    });
  }
}
