import { Component, ChangeDetectionStrategy, Optional, Self, Input, HostBinding } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NgSsmComponent, Store } from 'ngssm-store';
import { MatFormFieldControl } from '@angular/material/form-field';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { noop, Subject } from 'rxjs';
import { StringPartsExtractor } from '../../model';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { EditStringPartsExtractorAction } from '../../actions';

@Component({
  selector: 'ngssm-string-parts-extractor',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ngssm-string-parts-extractor.component.html',
  styleUrls: ['./ngssm-string-parts-extractor.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [{ provide: MatFormFieldControl, useExisting: NgssmStringPartsExtractorComponent }]
})
export class NgssmStringPartsExtractorComponent
  extends NgSsmComponent
  implements MatFormFieldControl<StringPartsExtractor>, ControlValueAccessor
{
  private static nextId = 0;

  private onChangeCallback: (_: any) => void = noop;
  private _required = false;
  private _disabled = false;

  constructor(store: Store, @Optional() @Self() public ngControl: NgControl) {
    super(store);

    // Replace the provider from above with this.
    if (this.ngControl != null) {
      // Setting the value accessor directly (instead of using
      // the providers) to avoid running into a circular import.
      this.ngControl.valueAccessor = this;
    }
  }

  @HostBinding('id') public id = `parts-extractor-${NgssmStringPartsExtractorComponent.nextId++}`;
  public controlType = 'ngssm-string-parts-extractor';
  public placeholder = '';
  public focused = false;
  public stateChanges = new Subject<void>();
  public value: StringPartsExtractor | null = null;
  public autofilled?: boolean | undefined;
  public userAriaDescribedBy?: string | undefined;

  public get empty(): boolean {
    return this.value === null;
  }

  public get shouldLabelFloat(): boolean {
    return this.value !== null;
  }

  @Input()
  public get required(): boolean {
    return this._required;
  }

  public set required(value: boolean) {
    this._required = coerceBooleanProperty(value);
    this.stateChanges.next();
  }

  @Input()
  public get disabled(): boolean {
    return this._disabled;
  }

  public set disabled(value: boolean) {
    this._disabled = coerceBooleanProperty(value);
    this.stateChanges.next();
  }

  public get errorState(): boolean {
    return this.ngControl?.invalid ?? false;
  }

  public writeValue(obj: StringPartsExtractor | null | undefined): void {
    console.log('writeValue', obj);
    this.updateValue(obj ?? null);
  }

  public registerOnChange(fn: any): void {
    this.onChangeCallback = fn;
  }

  public registerOnTouched(fn: any): void {
    // nothing to do for now.
  }

  public setDisabledState?(isDisabled: boolean): void {}

  public setDescribedByIds(ids: string[]): void {
    // nothing to do for now
  }

  public onContainerClick(event: MouseEvent): void {
    this.dispatchAction(new EditStringPartsExtractorAction(this.id, this.value ?? undefined));
  }

  private updateValue(extractor: StringPartsExtractor | null): void {
    this.value = extractor;
    this.stateChanges.next();
    this.onChangeCallback(this.value);
  }
}
