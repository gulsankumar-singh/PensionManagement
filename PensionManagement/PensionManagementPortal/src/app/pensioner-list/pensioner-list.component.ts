import { Component, OnInit } from '@angular/core';
import { PensionerListService } from './services/pensioner-list.service';

@Component({
  selector: 'app-pensioner-list',
  templateUrl: './pensioner-list.component.html',
  styleUrls: ['./pensioner-list.component.css'],
})
export class PensionerListComponent implements OnInit {
  tableCols = [
    'Name',
    'Date of Birth',
    'Aadhaar Number',
    'PAN',
    'Salary',
    'Allowance',
    'Pension Type',
  ];

  pensionerList = [
    {
      name: '',
      dateOfBirth: '',
      panNumber: '',
      aadharNumber: '',
      salaryEarned: '',
      allowances: '',
      pensionType: '',
    },
  ];
  constructor(private pensionerListService: PensionerListService) {}

  ngOnInit(): void {
    this.fetchPensionerList();
  }

  fetchPensionerList() {
    this.pensionerListService.fetchPensionerList().subscribe((data) => {
      this.pensionerList = data;
      console.log(this.pensionerList);
    });
  }
}
