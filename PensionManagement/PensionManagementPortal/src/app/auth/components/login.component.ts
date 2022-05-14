import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthResponseData } from 'src/app/shared/interfaces/interfaces';
import { NotificationService } from 'src/app/shared/services/notification-service/notification.service';
import { AuthService } from '../services/auth.service';

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
    private router: Router
  ) {}

  ngOnInit(): void {}

  onSubmit(form: NgForm) {
    if (!form.valid) {
      return;
    }
    let authObserable: Observable<AuthResponseData> = this.authService.login(
      form.value.userName,
      form.value.password
    );

    authObserable.subscribe(
      (resData) => {
        if (resData.status == 'Success') {
          this.notificationService.showSuccess(resData.message, 'Success');
          this.router.navigate(['/']);
        } else {
          this.notificationService.showError(resData.message, 'Error');
        }
      },
      (error) => {
        console.log(error);
        this.notificationService.showError(error, 'Error');
      }
    );
    form.reset();
  }
}
