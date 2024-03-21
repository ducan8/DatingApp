import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'vjppro  ';
  users: any;
  constructor(private http: HttpClient) {}
  ngOnInit(): void {
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
