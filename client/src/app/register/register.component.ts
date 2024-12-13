import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  private accountService = inject(AccountService);
  private toasterService = inject(ToastrService);
  private router = inject(Router);
  @Output() cancelRegister = new EventEmitter();
  model: any = {}

  ngOnInit(): void {
  }

  register() {
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response)
        this.cancel();
      },
      error: error => this.toasterService.error(error.error)
    });
  }

  cancel() {
    this.router.navigateByUrl("/");
  }

  /*
  cancel() {
    this.cancelRegister.emit(false);
  }
  */
}
