import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCheckboxChange, MatCheckboxModule } from '@angular/material/checkbox';
import { BehaviorSubject, Observable } from 'rxjs';

import { ConsoleAppender, NgSsmComponent, Store } from 'ngssm-store';
import { NgssmRegexComponent, NgssmStringPartsExtractorComponent, StringPartsExtractor } from 'ngssm-regex-tools';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatCardModule,
    MatCheckboxModule,
    NgssmStringPartsExtractorComponent,
    NgssmRegexComponent
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent extends NgSsmComponent {
  private readonly _regexRequired$ = new BehaviorSubject<boolean>(false);

  public readonly extractorControl = new FormControl<StringPartsExtractor | undefined>(undefined, Validators.required);

  public readonly regexControl = new FormControl<string | undefined>(undefined);

  constructor(store: Store, consoleAppender: ConsoleAppender) {
    super(store);
    consoleAppender.start();

    this.extractorControl.valueChanges.subscribe((v) => {
      console.log('AppComponent - new extractor', v);
    });
  }

  public get regexRequired$(): Observable<boolean> {
    return this._regexRequired$.asObservable();
  }

  public setRegexControlDisabled(event: MatCheckboxChange): void {
    if (event.checked) {
      this.regexControl.disable();
    } else {
      this.regexControl.enable();
    }
  }

  public setRegexControlRequired(event: MatCheckboxChange): void {
    this._regexRequired$.next(event.checked);
  }
}
