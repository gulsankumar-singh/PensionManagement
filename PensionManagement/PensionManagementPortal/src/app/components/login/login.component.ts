import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { AuthResponseData } from 'src/app/shared/interfaces/interface';
import { NotificationService } from 'src/app/shared/services/notification-service/notification.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  isLoading = false;
  error: string = '';
  constructor(
    private authService: AuthService,
    private notificationService: NotificationService,
    private router: Router,
    private loadingSpinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.authService.logout();
  }

  onSubmit(form: NgForm) {
    if (!form.valid) {
      this.error = 'Username and password are mandatory';
      return;
    }
    this.error = '';
    this.loadingSpinner.show();

    this.authService.login(form.value.userName, form.value.password).subscribe({
      next: (res) => this.handleAPIResponse(res),
      error: (err) => this.handleAPIError(err),
    });
  }

  private handleAPIResponse(resData: AuthResponseData) {
    if (resData.status == 'Success') {
      this.notificationService.showSuccess('Login Successful', 'Success');
      this.router.navigate(['/']);
    }
    this.loadingSpinner.hide();
  }

  private handleAPIError(error: any) {
    this.loadingSpinner.hide();
  }
}
