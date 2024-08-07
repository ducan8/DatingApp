import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  @Output() cancelRegister = new EventEmitter();

  model: any = {};

  constructor(
    private AccountService: AccountService,
    private toastr: ToastrService
  ) {}

  register() {
    console.log(this.model);
    this.AccountService.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: (err) => {
        this.toastr.error(err.error);
      },
    });
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
