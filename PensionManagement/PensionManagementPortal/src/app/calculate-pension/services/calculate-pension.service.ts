import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PensionInput } from '../calculate-pension.component';
import { tap } from 'rxjs';

export interface PensionDetailResponse {
  status: string;
  message: string;
  response: {
    name: string;
    panNumber: string;
    aadharNumber: string;
    salaryEarned: string;
    allowances: string;
    pensionType: string;
    pensionAmount: string;
    bankServiceCharge: string;
  };
}
@Injectable({ providedIn: 'root' })
export class CalculatePensionService {
  constructor(private httpClient: HttpClient) {}

  calculatePension(pensionInput: PensionInput) {
    return this.httpClient.post<PensionDetailResponse>(
      'http://localhost:8002/api/ProcessPension/ProcessPension',
      pensionInput
    );
  }
}
