Feature: Home "Hello World" for SpecFlow and Selenium

A short summary of the feature

@Jade
Scenario: Home page contains Footer with Company Name
	Given I am a visitor
	When I am on the "Home" page
	Then The page footer contains "Night Owl Studios"

@Oneil
Scenario: Home page title contains Home Page
	Given I am a visitor
	When I am on the "Home" page
	Then The page title contains "Home Page"

@Jazz
Scenario: Home Page contains Header with Well Met
	Given I am a visitor
	When I am on the "Home" page
	Then the page header contains "Well Met!"
