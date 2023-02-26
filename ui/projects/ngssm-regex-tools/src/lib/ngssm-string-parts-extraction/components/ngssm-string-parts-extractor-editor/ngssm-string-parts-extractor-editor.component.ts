import { Component, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormControl, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';

import { NgSsmComponent, Store } from 'ngssm-store';
import { StringPartsExtractorValidationResult } from '../../model';
import { selectNgssmStringPartsExtractionState } from '../../state';

@Component({
  selector: 'ngssm-ngssm-string-parts-extractor-editor',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatCardModule, MatButtonModule, MatInputModule],
  templateUrl: './ngssm-string-parts-extractor-editor.component.html',
  styleUrls: ['./ngssm-string-parts-extractor-editor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NgssmStringPartsExtractorEditorComponent extends NgSsmComponent {
  public readonly expressionControl = new FormControl<string | undefined>(undefined, [
    Validators.required,
    (c) => this.validateExpression(c)
  ]);

  constructor(store: Store) {
    super(store);

    this.watch((s) => selectNgssmStringPartsExtractionState(s).stringPartsExtractorEditor.expression).subscribe((value) =>
      this.expressionControl.setValue(value, { emitEvent: false })
    );
  }

  private validateExpression(control: AbstractControl): ValidationErrors | null {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const validator = (window as any)?.smusdi?.RegexToolsApi;
    if (!validator) {
      return null;
    }

    const validationResult: string = validator.ValidateStringPartsExtractor(JSON.stringify({ expression: control.value }));
    if (!validationResult) {
      return null;
    }

    const result: StringPartsExtractorValidationResult = JSON.parse(validationResult);
    if (result.isValid || !result.errors['expression']) {
      return null;
    }

    return {
      regex: result.errors['expression'][0]
    };
  }
}
