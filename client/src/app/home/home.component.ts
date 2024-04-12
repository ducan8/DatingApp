import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  registerMode: boolean = false;
  users: any = {};

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(value: boolean) {
    this.registerMode = value;
  }

  constructor(private http: HttpClient) {
    this.getUsers();
  }

  getUsers() {
    this.http.get('https://localhost:7152/api/Users').subscribe({
      next: (res) => (this.users = res),
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        console.log('resquest has completed');
      },
    });
  }
}
