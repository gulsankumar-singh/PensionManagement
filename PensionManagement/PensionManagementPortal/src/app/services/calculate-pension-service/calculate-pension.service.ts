import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, throwError } from 'rxjs';
import { PensionInput } from 'src/app/shared/models/processInput';
import { PensionDetailResponse } from 'src/app/shared/interfaces/interface';
import { environment } from 'src/environments/environment';
import { ApiPaths } from 'src/app/shared/enums/api-paths';

@Injectable({ providedIn: 'root' })
export class CalculatePensionService {
  baseUrl: string = environment.processPensionServiceBaseUrl;
  constructor(private httpClient: HttpClient) {}

  calculatePension(pensionInput: PensionInput) {
    return this.httpClient.post<PensionDetailResponse>(
      `${this.baseUrl}/${ApiPaths.CalculatePension}`,
      pensionInput
    );
  }

  // private handleError(errorRes: HttpErrorResponse) {
  //   let errorMessage = 'An unknown error occured!';
  //   return throwError(() => new Error(errorMessage));
  // }
}
