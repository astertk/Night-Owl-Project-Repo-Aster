Feature: NotLoggedIn

Asserting that users that are not logged in get redirected to the login/register page.

@Jade
Scenario: World Nav-Bar Link Redirects to Login Page if Not Logged In
	Given I am a visitor
	And I am not logged in
	When I am on the "Home" page
	And I click on the "World" NavBar Link
	Then I should be redirected to "https://localhost:7282/Identity/Account/Login?ReturnUrl=%2FWorld"

	Scenario: Maps Nav-Bar Link Redirects to Login Page if Not Logged In
	Given I am a visitor
	And I am not logged in
	When I am on the "Home" page
	And I click on the Maps NavBar Link
	Then I should be redirected to "https://localhost:7282/Identity/Account/Login?ReturnUrl=%2FMaps"

	

