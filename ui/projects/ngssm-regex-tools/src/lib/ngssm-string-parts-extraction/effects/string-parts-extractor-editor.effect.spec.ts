import { TestBed } from '@angular/core/testing';

import { StoreMock } from 'ngssm-store';

import { StringPartsExtractorEditorEffect } from './string-parts-extractor-editor.effect';

describe('StringPartsExtractorEditorEffect', () => {
  let effect: StringPartsExtractorEditorEffect;
  let store: StoreMock;

  beforeEach(() => {
    store = new StoreMock({});
    TestBed.configureTestingModule({
      imports: [],
      providers: [StringPartsExtractorEditorEffect]
    });
    effect = TestBed.inject(StringPartsExtractorEditorEffect);
  });

  [].forEach((actionType: string) => {
    it(`should process action of type '${actionType}'`, () => {
      expect(effect.processedActions).toContain(actionType);
    });
  });
})
