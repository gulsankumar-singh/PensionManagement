import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  notificationConfig = {
    closeButton: true,
    progressBar: true,
    timeOut: 5000,
    positionClass: 'toast-top-right',
  };
  constructor(private toastr: ToastrService) {}

  showSuccess(message: string, title: string) {
    this.toastr.success(message, title, this.notificationConfig);
  }

  showError(message: string, title: string) {
    this.toastr.error(message, title, this.notificationConfig);
  }

  showInfo(message: string, title: string) {
    this.toastr.info(message, title, this.notificationConfig);
  }

  showWarning(message: string, title: string) {
    this.toastr.warning(message, title, this.notificationConfig);
  }
}
