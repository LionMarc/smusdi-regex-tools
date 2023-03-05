import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

import { NgssmExtractedPartComponent } from './ngssm-extracted-part.component';

describe('NgssmExtractedPartComponent', () => {
  let component: NgssmExtractedPartComponent;
  let fixture: ComponentFixture<NgssmExtractedPartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NoopAnimationsModule, NgssmExtractedPartComponent]
    }).compileComponents();

    fixture = TestBed.createComponent(NgssmExtractedPartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
