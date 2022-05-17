import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, take, throwError } from 'rxjs';
import { AuthService } from 'src/app/services/auth-service/auth.service';
import { Router } from '@angular/router';
import { NotificationService } from '../services/notification-service/notification.service';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  constructor(
    private authService: AuthService,
    private router: Router,
    private notificationService: NotificationService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.authService.getAuthToken();

    if (token) {
      // If we have a token, we set it to the header
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${token}` },
      });
    }

    return next.handle(request).pipe(
      catchError((err) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            this.authService.logout();
            this.router.navigate(['/session-expired']);
          } else if (err.status === 400) {
            this.notificationService.showError(err.error.message, 'Error');
          } else if (err.status == 404) {
            if (err.error) {
              this.notificationService.showError(err.error.message, 'Error');
            }
          }
        }
        return throwError(() => new Error(err));
      })
    );
  }
}
