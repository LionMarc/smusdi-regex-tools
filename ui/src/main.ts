import { importProvidersFrom } from '@angular/core';
import { MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS } from '@angular/material/dialog';
import { bootstrapApplication, BrowserModule } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';

import { NGSSM_REGEX_TOOLS_DATE_FORMATS, provideNgssmStringPartsExtraction } from 'ngssm-regex-tools';
import { useDefaultErrorStateMatcher } from 'ngssm-toolkit';

import { AppComponent } from './app/app.component';

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom([BrowserModule, MatDialogModule]),
    provideAnimations(),
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: { position: { top: '40px' }, closeOnNavigation: false }
    },
    useDefaultErrorStateMatcher,
    provideNgssmStringPartsExtraction(),
    { provide: NGSSM_REGEX_TOOLS_DATE_FORMATS, useValue: ['yyyy-MM-dd', 'dd/MM/yyyy', 'MM/dd/yyyy', 'yyyyMMdd', 'dd MM yyyy'] }
  ]
});
