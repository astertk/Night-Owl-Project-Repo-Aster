Feature: Home "Hello World" for SpecFlow and Selenium

A short summary of the feature

@Jade
Scenario: Home page contains Footer with Company Name
	Given I am a visitor
	When I am on the "Home" page
	Then The page footer contains "Night Owl Studios"
