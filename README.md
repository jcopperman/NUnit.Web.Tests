Build Summary: [![Build Status](https://travis-ci.com/jcopperman/hotelcalifornia.svg?branch=master)](https://travis-ci.com/jcopperman/hotelcalifornia)

## PART 1: Manual test approach

### Summary
The following test cases are intended to cover testing for basic CRUD operations (Create, Read, Update & Delete). 
In this exercise only Create and Delete are verified with both positive and negative scenarios (negative values, past dates, unexpected characters and empty values).

#### TEST CASE 001: Create a valid booking record
1. Navigate to the Hotel Booking URL
2. Verify that the correct landing page is displayed
3. Enter valid information for Firstname, Surname, Price, Check-in and Check-out
4. Click Save

- **EXPECTED RESULT:** A new booking record is persisted to the list
- **ACTUAL RESULT:** As expected

#### TEST CASE 002: Delete an existing record
1. Navigate to the Hotel Booking URL
2. Verify that the correct landing page is displayed
3. Identify a record to be deleted
4. Click Delete

- **EXPECTED RESULT:** The selected record is removed from the list 
- **ACTUAL RESULT:** As expected

#### TEST CASE 003: Create an invalid booking record
1. Navigate to the Hotel Booking URL
2. Verify that the correct landing page is displayed
3. Enter invalid information for Firstname(123), Surname(123), Price(-500), Check-in(DateTime.Now) and Check-out(DateTime.Now - 5)
4. Click Save

- **EXPECTED RESULT:** The user is informed that one or more entries are incorrect and the record is not persisted to the list
- **ACTUAL RESULT:** The form does not have any field validation and the record is saved successfully

#### TEST CASE 004: Create a blank record
1. Navigate to the Hotel Booking URL
2. Verify that the correct landing page is displayed
3. Leave fields blank (test iteratively)
4. Click Save

- **EXPECTED RESULT:** An empty record is not persisted to the list of records
- **ACTUAL RESULT:** As expected

## PART 2: Test Automation with .NET

### Automation Summary
Technology: .NET Core project using NUnit and Selenium Webdriver which is deployed via Travis CI

Run locally from Visual Studio with NUnit Test Runner:

Prequisites:
1. Clone this repository with ``` git clone https://github.com/jcopperman/NUnit.Web.Tests.git ```
1. Install Visual Studio 2019 Community
2. Install Git for Windows
2. Run Visual Studio as Administrator
4. Build Solution and run Tests from the Test Explorer







