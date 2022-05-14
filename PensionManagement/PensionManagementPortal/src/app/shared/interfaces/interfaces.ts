export interface AuthResponseData {
  status: string;
  message: string;
  response: {
    expiration: string;
    token: string;
  };
}
