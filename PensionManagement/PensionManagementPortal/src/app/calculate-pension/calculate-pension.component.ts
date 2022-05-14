import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NotificationService } from '../shared/services/notification-service/notification.service';
import { CalculatePensionService } from './services/calculate-pension.service';

// export class PensionInput {
//   constructor(
//     public userName: string,
//     public aadhaarNumber: number,
//     public panNumber: string,
//     public pensionType: number
//   ) {}
// }

export class PensionInput {
  constructor(public aadhaarNumber: number) {}
}

@Component({
  selector: 'app-calculate-pension',
  templateUrl: './calculate-pension.component.html',
  styleUrls: ['./calculate-pension.component.css'],
})
export class CalculatePensionComponent implements OnInit {
  constructor(
    private notificationService: NotificationService,
    private calculatePensionService: CalculatePensionService
  ) {}

  // pensionDetail: {
  //   status: string,
  // message: string,
  // response: {
  //   name: string,
  //   panNumber: string,
  //   aadhaarNumber: string,
  //   salaryEarned: string,
  //   allowances: string,
  //   pensionType: string,
  //   pensionAmount: string,
  //   bankServiceCharge: string,
  // }
  // };

  isPensionCalculated: boolean = false;

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
    if (!form.valid) {
      this.notificationService.showError(
        'Please enter all the mandatory fields',
        'Error'
      );
      return;
    }

    // const pensionInput = new PensionInput(
    //   form.value.userName,
    //   form.value.aadhaarNumber,
    //   form.value.panNumber,
    //   Number(form.value.pensionType)
    // );
    this.isPensionCalculated = false;
    const pensionInput = new PensionInput(form.value.aadharNumber);
    this.calculatePensionService
      .calculatePension(pensionInput)
      .subscribe((response) => {
        // this.pensionDetail = data;
        console.log(response);
        if (response.status == 'Error') {
          this.notificationService.showError(response.message, response.status);
          return;
        } else {
          this.notificationService.showSuccess(
            response.message,
            response.status
          );
          this.pensionDetail = response.response;
          this.isPensionCalculated = true;
        }
      });
  }
}
