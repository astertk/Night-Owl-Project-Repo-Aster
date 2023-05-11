Feature: Maps


Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  |
	  | jleemcmichael18@mail.wou.edu    | jleemcmichael18@mail.wou.edu     | Jade     | Mcmichael    | Beans12! |

	And the following users do not exist
	  | UserName | Email               | FirstName | LastName | Password  |
	  | AndreC   | colea@example.com   | Andre     | Cole     | 0a9dfi3.a |
	  | JoannaV  | valdezJ@example.com | Joanna    | Valdez   | d9u(*dsF4 |

@Jade
Scenario: Maps redirects back to Index
	Given I am a user with first name '<FirstName>'
	
	Then I am redirected to the '<Page>' page
	When I navigate to the "Maps/Index" page
	And I click on the "Template" Button
	And I fill out the "create_template_form"
	And I click on the "Create Map" Button
	Then I should be redirected to "https://localhost:7282/Maps/Index"
