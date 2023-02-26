import { ExtractedPart, StringPartsExtractor } from '../model';

export interface StringPartsExtractorEditor {
  controlId?: string;
  extractor?: StringPartsExtractor;
  expression?: string;
  parts: ExtractedPart[];
}

export const getDefaultStringPartsExtractorEditor = (): StringPartsExtractorEditor => ({
  parts: []
});
