Feature: Home "Hello World" for SpecFlow and Selenium

A short summary of the feature

@Jade
Scenario: Home page contains Footer with Company Name
	Given I am a visitor
	When I am on the "Home" page
	Then The page footer contains "Night Owl Studios"


Scenario: Home Nav-Bar Link Redirects to Home/Index
	Given I am a visitor
	When I am on the "Home" page
	And I click on the "Home" Nav-Bar Link
	Then I should be redirected to "https://localhost:7282/"