import { Injectable, Provider } from '@angular/core';

import { Reducer, State, Action, NGSSM_REDUCER } from 'ngssm-store';

import { EditStringPartsExtractorAction, NgssmStringPartsExtractionActionType } from '../actions';
import { updateNgssmStringPartsExtractionState } from '../state';

@Injectable()
export class StringPartsExtractorEditorReducer implements Reducer {
  public readonly processedActions: string[] = [NgssmStringPartsExtractionActionType.editStringPartsExtractor];

  public updateState(state: State, action: Action): State {
    switch (action.type) {
      case NgssmStringPartsExtractionActionType.editStringPartsExtractor: {
        const editStringPartsExtractorAction = action as EditStringPartsExtractorAction;
        return updateNgssmStringPartsExtractionState(state, {
          stringPartsExtractorEditor: {
            controlId: { $set: editStringPartsExtractorAction.controlId },
            expression: { $set: editStringPartsExtractorAction.extractor?.expression },
            parts: { $set: [...(editStringPartsExtractorAction.extractor?.parts ?? [])] }
          }
        });
      }
    }

    return state;
  }
}

export const stringPartsExtractorEditorReducerProvider: Provider = {
  provide: NGSSM_REDUCER,
  useClass: StringPartsExtractorEditorReducer,
  multi: true
};
