Feature: Navigation menu can be toggled

A short summary of the feature

@Oneil
Scenario: Visitor clicks the navbar-toggler
	Given Given a visitor is on the homepage
	When When the vistor clicks the navbar-toggler button
	Then the navigation menu should appear on the screen

Scenario: Visitor closes the navigation menu
	Given the vistor has opened the navigation menu
	When the vistor clicks the "Close" button on the navigation menu
	Then navigation menu should disappear from the screen
