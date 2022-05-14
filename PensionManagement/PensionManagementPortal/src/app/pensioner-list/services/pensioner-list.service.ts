import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map, tap, throwError } from 'rxjs';

export interface PensionerList {
  name: string;
  dateOfBirth: string;
  panNumber: string;
  aadharNumber: string;
  salaryEarned: string;
  allowances: string;
  pensionType: string;
}

@Injectable({ providedIn: 'root' })
export class PensionerListService {
  constructor(private httpClient: HttpClient) {}

  fetchPensionerList() {
    return this.httpClient
      .get<PensionerList[]>(
        'http://localhost:8001/api/PensionerDetail/GetAllPensioner'
      )
      .pipe(
        catchError((errorRes) => {
          let errorMessage = 'An unknown error occured!';
          return throwError(() => new Error(errorMessage));
        })
      );
  }
}
