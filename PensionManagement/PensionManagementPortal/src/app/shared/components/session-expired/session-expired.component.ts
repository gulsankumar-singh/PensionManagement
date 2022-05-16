import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth-service/auth.service';

@Component({
  selector: 'app-session-expired',
  templateUrl: './session-expired.component.html',
  styleUrls: ['./session-expired.component.css'],
})
export class SessionExpiredComponent implements OnInit {
  constructor(private authService: AuthService) {}

  ngOnInit(): void {}
}
