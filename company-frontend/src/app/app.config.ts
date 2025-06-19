import { ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';


import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { ApiModule } from './api/api.module';
import { environment } from 'environments/environment';

export const appConfig: ApplicationConfig = {
  providers: [
    // importProvidersFrom(ApiModule.forRoot({ rootUrl: 'http://localhost:4200' })),
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient()
  ]
};
