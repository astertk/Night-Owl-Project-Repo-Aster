Feature: Backstory "Hello World" for SpecFlow and Selenium


//Background: 
	Given the following users exist
	| Username            | Email              | FirstName | LastName | Password  |
	| johnk@example.com   | johnk@example.com  | John      | Karma    | Hello123! |
	| juliaj@example.com  | juliaj@example.com | Julia     | Jam      | Hello123! |

@Jazz
Scenario: Backstory Page Shows Login/Register Partial
	Given I am a visitor
		And I am on the "Home" page
	When I click the "Backstory" button
	Then I should be shown the "Login" partial


@Jazz
Scenario: Backstory Page contains the Login Button
	Given I am a visitor
	When I am on the "Backstory" Page
	Then I should be shown the "Login" Button

@Jazz
Scenario: Backstory Page contains the Forgot Password 
	Given I am a visitor
	When I am on the "Backstory" Page
	Then I should be shown the "Forgot Password" text
