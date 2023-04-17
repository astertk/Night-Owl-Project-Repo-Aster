Feature: RandomCharacter

A short summary of the feature
Background: 
	Given the following users exist
	  | UserName				| Email						| FirstName | LastName | Password	|
	  | oneilmagno@gmail.com	| oneilmagno@gmail.com		| Oneil     | Magno    | Leafyear29!|
	And I am a user with the User name 'oneilmagno@gmail.com'
	And I login
	And I select the "Characters" page
	And I select the "Random" button
@Oneil
Scenario: Clicking on Character page link
	Given I am logged in as a registered user
	When I select the "Characters" link on the homepage
	Then I am redirected to the Characters page
