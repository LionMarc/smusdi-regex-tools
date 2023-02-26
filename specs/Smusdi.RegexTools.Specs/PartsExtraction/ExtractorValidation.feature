Feature: Extractor Validation

Scenario: Validating an extractor with an empty regular expression
    When I validate an extractor with expression "" and parts
        | Name | Type | Format |
    Then I get the validation errors
    """
    {
        "expression": [ "expression must be set." ]
    }
    """

Scenario: Validating an extractor with an invalid regular expression
    When I validate an extractor with expression "*" and parts
        | Name | Type | Format |
    Then I get the validation errors
    """
    {
        "expression": [ "Invalid pattern '*' at offset 1. Quantifier '*' following nothing." ]
    }
    """

Scenario: Validating an extractor with no named group
    When I validate an extractor with expression ".*" and parts
        | Name | Type | Format |
    Then I get the validation errors
    """
    {
        "expression": [ "At least one named group must be set." ]
    }
    """

Scenario: Validating an extractor with a named group not associated to a part
    When I validate an extractor with expression "^(?<date>\d{8})-(?<isin>\S+)-" and parts
        | Name | Type | Format |
        | isin | Text |        |
    Then I get the validation errors
    """
    {
        "parts": [ "Not found parts: date." ]
    }
    """

Scenario: Validating an extractor with a part not associated to a named group
    When I validate an extractor with expression "^(?<date>\d{8})-(?<isin>\S+)-" and parts
        | Name  | Type   | Format |
        | date  | Date   |        |
        | isin  | Text   |        |
        | price | Number |        |
    Then I get the validation errors
    """
    {
        "parts": [ "Missing parts: price." ]
    }
    """

Scenario: Validating an extractor with a part of type Date with no format
    When I validate an extractor with expression "^(?<date>\d{8})-(?<isin>\S+)-" and parts
        | Name  | Type   | Format |
        | date  | Date   |        |
        | isin  | Text   |        |
    Then I get the validation errors
    """
    {
        "parts[0]": [ "Missing format for 'Date' type." ]
    }
    """
