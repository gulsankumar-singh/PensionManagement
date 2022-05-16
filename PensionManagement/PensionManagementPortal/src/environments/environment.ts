// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  authServiceBaseUrl: 'http://20.121.165.207',
  pensionerDetailServiceBaseUrl: 'http://20.237.56.64',
  processPensionServiceBaseUrl: 'http://20.237.56.101',
  authServiceBaseUrlLocal: 'http://localhost:8000',
  pensionerDetailServiceBaseUrlLocal: 'http://localhost:8001',
  processPensionServiceBaseUrlLocal: 'http://localhost:8002',
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
