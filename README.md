[![Build Status](https://travis-ci.org/jcopperman/NUnit.Web.Tests.svg?branch=master)](https://travis-ci.org/jcopperman/NUnit.Web.Tests)

# Project: hotelcalifornia
This repository is a story about an intrepid adventurer who seeks to uncover the secrets of a fictitious hotel by testing and flexing it's booking system.

> Warning: You can check-out anytime you like but you can never leave.

## PART 1: Manual test approach

### Summary
The test approach and documentation thereof follows the Behaviour Driven Development (BDD) design pattern which aims to provide clarity in adherence with the Product Owner and/or Stakeholder requirements which should be easily followed by a developer and software quality analyst in implementation and testing. The following test cases are intended to cover testing for basic CRUD operations (Create, Read, Update & Delete) and form validation.

#### Considerations
As this web app is hosted for the purpose of assessments no load/performance testing was performed

#### TEST CASE 001: The intrepid adventurer is able to find the hotel's homepage
1. Navigate to the Hotel Booking URL
2. Verify that the correct landing page is displayed

- **EXPECTED RESULT:** The [Website Under Test] landing page is loaded correctly
- **ACTUAL RESULT:** As expected

#### TEST CASE 002: The intrepid adventurer is able to view existing booking records
1. Given you have navigated to the Hotel Booking URL
2. When the correct landing page is displayed
3. Then you should see a list of existing bookings

- **EXPECTED RESULT:** A list of hotel booking records is displayed
- **ACTUAL RESULT:** As expected

#### TEST CASE 003: The intrepid adventurer is able to create a new booking record
1. Given you have navigated to the Hotel Booking URL
2. When the correct landing page is displayed
3. And you enter a new record with the correct fields
4. When you click "Save"
5. Then your new record is persisted to the list of records

- **EXPECTED RESULT:** A new booking record is persisted to the list
- **ACTUAL RESULT:** As expected

#### TEST CASE 004: The intrepid adventurer is able to remove their booking record
1. Given you have navigated to the Hotel Booking URL
2. When the correct landing page is displayed

- **EXPECTED RESULT:** An existing record is successfully removed from the list
- **ACTUAL RESULT:** As expected

#### TEST CASE 005: The intrepid adventurer is able to edit an existing booking record
1. Given you have navigated to the Hotel Booking URL
2. When the correct landing page is displayed
3. And you enter a new record with the correct fields
4. When you click "Save"
5. Then your new record is persisted to the list of records

- **EXPECTED RESULT:** An existing record is updated successfully
- **ACTUAL RESULT:** As expected

#### TEST CASE 006: The intrepid adventurer is not able to add a blank record
1. Given you have navigated to the Hotel Booking URL
2. When the correct landing page is displayed
3. And you enter a new record with no data
4. When you click "Save"
5. Then the blank record is NOT persisted to the list of records

- **EXPECTED RESULT:** An empty record is not persisted to the list of records
- **ACTUAL RESULT:** As expected

#### TEST CASE 007: The intrepid adventurer cannot check-in a past date
1. Given you have navigated to the Hotel Booking URL
2. When the correct landing page is displayed
3. And you enter a new record and set the Check-in date to the past
4. When you click "Save"
5. Then form validation should inform the user that the Check-in date cannot be set to the past

- **EXPECTED RESULT:** The user is informed that the check-in date cannot be set to the past
- **ACTUAL RESULT:** The record is persisted with no validation

#### TEST CASE 008: The intrepid adventurer cannot check-out a past date
1. Given you have navigated to the Hotel Booking URL
2. When the correct landing page is displayed
3. And you enter a new record and set the Check-out date to the past
4. When you click "Save"
5. Then form validation should inform the user that the Check-out date cannot be set to the past

- **EXPECTED RESULT:** The user is informed that the check-out date cannot be set to the past
- **ACTUAL RESULT:** The record is persisted with no validation

#### TEST CASE 009: The intrepid adventurer ascertains whether the hotel's site is secure
1. Given you have navigated to the Hotel Booking URL
2. When the correct landing page is displayed
3. Then verify that the website has a valid SSL certificate

- **EXPECTED RESULT:** The browser indicates the site is secure with a padlock and the url contains HTTPS
- **ACTUAL RESULT:** The browser indicates that the site is not secure and the url contains HTTP


## PART 2: Test Automation with .NET

### Automation Summary
Technology: .NET Core project using NUnit and Selenium Webdriver which is deployed via Travis CI



