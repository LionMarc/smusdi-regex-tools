import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ConsoleAppender, NgSsmComponent, Store } from 'ngssm-store';
import { NgssmStringPartsExtractorComponent, StringPartsExtractor } from 'ngssm-regex-tools';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatToolbarModule, MatFormFieldModule, MatCardModule, NgssmStringPartsExtractorComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent extends NgSsmComponent {
  public readonly extractorControl = new FormControl<StringPartsExtractor | undefined>({ expression: '^(?<date>\\d[8})_', parts: [] });

  constructor(store: Store, consoleAppender: ConsoleAppender) {
    super(store);
    consoleAppender.start();

    this.extractorControl.valueChanges.subscribe((v) => {
      console.log('AppComponent - new extractor', v);
    });
  }
}
