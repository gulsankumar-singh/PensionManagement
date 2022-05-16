export interface AuthResponseData {
  status: string;
  message: string;
  response: {
    expiration: string;
    token: string;
  };
}

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

export interface PensionerList {
  name: string;
  dateOfBirth: string;
  panNumber: string;
  aadharNumber: string;
  salaryEarned: string;
  allowances: string;
  pensionType: string;
}
