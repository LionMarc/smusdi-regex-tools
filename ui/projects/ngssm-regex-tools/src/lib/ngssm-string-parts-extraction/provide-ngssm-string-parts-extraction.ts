import { EnvironmentProviders, makeEnvironmentProviders } from '@angular/core';

import { stringPartsExtractorEditorEffectProvider } from './effects';
import { stringPartsExtractorEditorReducerProvider } from './reducers';

export const provideNgssmStringPartsExtraction = (): EnvironmentProviders => {
  return makeEnvironmentProviders([stringPartsExtractorEditorEffectProvider, stringPartsExtractorEditorReducerProvider]);
};
