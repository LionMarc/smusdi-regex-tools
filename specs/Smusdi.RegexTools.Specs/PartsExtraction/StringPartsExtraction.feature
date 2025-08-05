Feature: String parts extraction

Scenario: Extracting parts with an invalid Date format
    Given the string part extractor with expression "^(?<date>\d{8})_" and parts
        | Name | Type | Format |
        | date | Date | gtyhu  |
    When I extract parts from string "20230304_test.txt"
    Then I get the extraction result
    """
    {
        "IsValid": false,
        "ExpectedPartsCount": 1,
        "Errors": {
            "date" : "String '20230304' was not recognized as a valid DateOnly."
        },
        "ExtractedParts": {
        }
    }
    """

Scenario: Extracting part of type Text
    Given the string part extractor with expression "^(?<date>\d{8})_" and parts
        | Name | Type | Format |
        | date | Text |        |
    When I extract parts from string "20230304_test.txt"
    Then I get the extraction result
    """
    {
        "IsValid": true,
        "ExpectedPartsCount": 1,
        "Errors": {
        },
        "ExtractedParts": {
            "date" : "20230304"
        }
    }
    """

Scenario: Extracting part of type Date
    Given the string part extractor with expression "^(?<date>\d{8})_" and parts
        | Name | Type | Format   |
        | date | Date | yyyyMMdd |
    When I extract parts from string "20230304_test.txt"
    Then I get the extraction result
    """
    {
        "IsValid": true,
        "ExpectedPartsCount": 1,
        "Errors": {
        },
        "ExtractedParts": {
            "date" : "2023-03-04"
        }
    }
    """

Scenario: Extracting part of type Date with extra whitespaces
    Given the string part extractor with expression "^(?<date>.*)_" and parts
        | Name | Type | Format     |
        | date | Date | MMM d yyyy |
    When I extract parts from string "Aug  7 2025_test.txt"
    Then I get the extraction result
    """
    {
        "IsValid": true,
        "ExpectedPartsCount": 1,
        "Errors": {
        },
        "ExtractedParts": {
            "date" : "2025-08-07"
        }
    }
    """

Scenario: Extracting part of type Number
    Given the string part extractor with expression "^Index: (?<close>.*)$" and parts
        | Name  | Type   | Format |
        | close | Number |        |
    When I extract parts from string "Index: 45.67"
    Then I get the extraction result
    """
    {
        "IsValid": true,
        "ExpectedPartsCount": 1,
        "Errors": {
        },
        "ExtractedParts": {
            "close" : 45.67
        }
    }
    """

Scenario: Extracting part of type Number with invalid format
    Given the string part extractor with expression "^Index: (?<close>.*)$" and parts
        | Name  | Type   | Format |
        | close | Number |        |
    When I extract parts from string "Index: 45R65"
    Then I get the extraction result
    """
    {
        "IsValid": false,
        "ExpectedPartsCount": 1,
        "Errors": {
            "close": "The input string '45R65' was not in a correct format."
        },
        "ExtractedParts": {
        }
    }
    """

Scenario Outline: Extracting part of type Time
    Given the string part extractor with expression "^Index: (?<timestamp>.*)$" and parts
        | Name      | Type | Format |
        | timestamp | Time |        |
    When I extract parts from string "Index: <timestampValue>"
    Then I get the extraction result
    """
    {
        "IsValid": true,
        "ExpectedPartsCount": 1,
        "Errors": {
        },
        "ExtractedParts": {
            "timestamp" : "<Expected>"
        }
    }
    """

    Examples:
        | timestampValue | Expected |
        | 13:23:00       | 13:23:00 |
        | 13:34          | 13:34:00 |

Scenario Outline: Extracting part of type Time with invalid format
    Given the string part extractor with expression "^Index: (?<timestamp>.*)$" and parts
        | Name      | Type | Format |
        | timestamp | Time |        |
    When I extract parts from string "Index: <timestampValue>"
    Then I get the extraction result
    """
    {
        "IsValid": false,
        "ExpectedPartsCount": 1,
        "Errors": {
            "timestamp": "String '<timestampValue>' was not recognized as a valid TimeOnly."
        },
        "ExtractedParts": {
        }
    }
    """

    Examples:
        | timestampValue |
        | 13:67          |
        | abc            |
