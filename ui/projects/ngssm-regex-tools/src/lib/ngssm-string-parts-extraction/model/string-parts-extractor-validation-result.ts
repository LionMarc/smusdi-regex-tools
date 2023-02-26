export interface StringPartsExtractorValidationResult {
  isValid: boolean;
  errors: { [key: string]: string[] };
}
