Feature: ItemCreation

A short summary of the feature

@Oneil
Scenario: User login, navigate, and creates Item
	Given I am on the login page
	When I enter valid credentials
	Then I should be at the Home Page

	Given I am on the Home Page
	When I navigate to the  "Items" page
	

	
    When I choose the rarity "Common"
    And I choose the type "Weapon"
    And I enter the key trait/word as "Powerful"
    Then I submit the form
    
