import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, Subject, tap, throwError } from 'rxjs';
import { AuthResponseData } from '../../shared/interfaces/interfaces';
import { User } from '../user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private tokenExpirationTimer: any;
  user = new BehaviorSubject<User>(null);

  constructor(private http: HttpClient, private router: Router) {}

  login(userName: string, password: string) {
    return this.http
      .post<AuthResponseData>(
        'http://localhost:8000/api/Authentication/Authenticate',
        {
          userName: userName,
          password: password,
        }
      )
      .pipe(
        catchError((errorRes) => {
          let errorMessage = 'An unknown error occured!';
          return throwError(() => new Error(errorMessage));
        }),
        tap((resData) => {
          this.handleAuthentication(
            userName,
            resData.response.token,
            resData.response.expiration
          );
        })
      );
  }

  getUser() {
    const userData: {
      userName: string;
      token: string;
      tokenExpirationDate: string;
    } = JSON.parse(localStorage.getItem('userData'));

    return localStorage.getItem('userData');
  }

  private handleAuthentication(
    userName: string,
    token: string,
    expiration: string
  ) {
    const expirationDate = new Date(expiration);
    const user = new User(userName, token, expirationDate);
    this.user.next(user);
    console.log('Handle authentication', expirationDate.getTime());
    console.log('user', user);

    this.autoLogout(expirationDate.getTime());
    localStorage.setItem('userData', JSON.stringify(user));
  }

  autologin() {
    console.log('autologin call');
    const userData: {
      userName: string;
      _token: string;
      _tokenExpirationDate: string;
    } = JSON.parse(localStorage.getItem('userData'));

    console.log(userData);
    if (!userData) {
      return;
    }

    const loadUser = new User(
      userData.userName,
      userData._token,
      new Date(userData._tokenExpirationDate)
    );

    console.log('loaded user', loadUser);

    if (loadUser.token) {
      console.log('load user token check success');
      this.user.next(loadUser);
      const expirationDuration =
        new Date(userData._tokenExpirationDate).getTime() -
        new Date().getTime();

      console.log('exp', expirationDuration);
      this.autoLogout(expirationDuration);
    }
  }

  logout() {
    console.log('logout call');
    this.user.next(null);
    this.router.navigate(['/login']);
    localStorage.removeItem('userData');
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }

    this.tokenExpirationTimer = null;
  }

  autoLogout(expirationDuration: number) {
    this.tokenExpirationTimer = setInterval(() => {
      const time = new Date().getTime();

      if (time > expirationDuration) {
        this.logout();
      }
    }, 1000);
  }
}
