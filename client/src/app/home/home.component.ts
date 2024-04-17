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

  constructor() {}
}
