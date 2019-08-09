# com.marcueberall.localization

## Installation

Open the package manager and add a package from disk and select the package.json file within the com.marcueberall.localization directory.

## Usage

Create a new GameObject and add the LocalizationManager. To create a localization sheet, just click on the button in the inspector. After editing the localization sheet click on the "Generate Localization Data" button to process the Excel sheet and cache the supported languages.

To automatically localize a UI component just add the corresponding localization helper to the game object.

## Important

- Close the Excel sheet before generating the localization data to prevent a file access violation.
- Don't forget to generate the localization data to refresh the database.

## Credits

This project uses the Excel Data Reader which can be found here https://github.com/ExcelDataReader/ExcelDataReader.