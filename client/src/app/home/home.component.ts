import { Component, inject } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  router = inject(Router);
  accountService = inject(AccountService);
  registerMode = false;

  registerToggle() {
    this.router.navigateByUrl("/register");
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
