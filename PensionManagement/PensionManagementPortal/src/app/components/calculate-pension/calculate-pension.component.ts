import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { CalculatePensionService } from 'src/app/services/calculate-pension-service/calculate-pension.service';
import { PensionDetailResponse } from 'src/app/shared/interfaces/interface';
import { PensionInput } from 'src/app/shared/models/processInput';
import { NotificationService } from 'src/app/shared/services/notification-service/notification.service';

@Component({
  selector: 'app-calculate-pension',
  templateUrl: './calculate-pension.component.html',
  styleUrls: ['./calculate-pension.component.css'],
})
export class CalculatePensionComponent implements OnInit {
  constructor(
    private notificationService: NotificationService,
    private calculatePensionService: CalculatePensionService,
    private spinnerService: NgxSpinnerService
  ) {}

  isPensionCalculated: boolean = false;
  validationError: string = '';

  pensionDetail: {
    name: string;
    panNumber: string;
    aadharNumber: string;
    salaryEarned: string;
    allowances: string;
    pensionType: string;
    pensionAmount: string;
    bankServiceCharge: string;
  };

  ngOnInit(): void {}

  onSubmit(form: NgForm) {
    console.log(form);
    if (!form.valid) {
      const aadhaarNumber: string = form.value.aadharNumber;
      console.log('form', form.value);
      console.log('aadd', aadhaarNumber);
      if (aadhaarNumber === '') {
        this.validationError = 'Please enter aadhaar number!';
      } else if (!aadhaarNumber.match(/^[0-9]+$/)) {
        this.validationError = 'Only integers allowed!';
      } else if (aadhaarNumber.length != 12) {
        this.validationError = 'Length of the aadhaar number should 12';
      }

      return;
    }

    this.validationError = '';
    this.spinnerService.show();
    // const pensionInput = new PensionInput(
    //   form.value.userName,
    //   form.value.aadhaarNumber,
    //   form.value.panNumber,
    //   Number(form.value.pensionType)
    // );
    this.isPensionCalculated = false;
    const pensionInput = new PensionInput(form.value.aadharNumber);
    this.calculatePensionService.calculatePension(pensionInput).subscribe({
      next: (response) => this.handleResponse(response),
      error: (error) => this.handleResError(error),
    });
  }

  private handleResponse(response: PensionDetailResponse) {
    if (response.status == 'Success') {
      this.notificationService.showSuccess(response.message, response.status);
      this.pensionDetail = response.response;
      this.isPensionCalculated = true;
    }
    this.spinnerService.hide();
  }

  private handleResError(error: any) {
    this.spinnerService.hide();
  }
}
