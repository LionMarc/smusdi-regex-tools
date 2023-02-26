import { StringPartsExtractorEditorReducer } from './string-parts-extractor-editor.reducer';

describe('StringPartsExtractorEditorReducer', () => {
  let reducer: StringPartsExtractorEditorReducer;
  let state: { [key: string]: any };

  beforeEach(() => {
    reducer = new StringPartsExtractorEditorReducer();
    state = {};
  });

  [].forEach((actionType: string) => {
    it(`should process action of type '${actionType}'`, () => {
      expect(reducer.processedActions).toContain(actionType);
    });
  });

  it('should return input state when processing not valid action type', () => {
    const updatedState = reducer.updateState(state, { type: 'not-processed' });

    expect(updatedState).toBe(state);
  });
});
