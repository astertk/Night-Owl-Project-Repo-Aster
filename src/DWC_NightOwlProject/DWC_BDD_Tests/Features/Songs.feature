Feature: Songs

A short summary of the feature

Background:
	Given the following users exist
	  | UserName   | Email                 | FirstName | LastName | Password  |
	  | jleemcmichael18@mail.wou.edu    | jleemcmichael18@mail.wou.edu     | Jade     | Mcmichael    | Beans12! |

	And the following users do not exist
	  | UserName | Email               | FirstName | LastName | Password  |
	  | AndreC   | colea@example.com   | Andre     | Cole     | 0a9dfi3.a |
	  | JoannaV  | valdezJ@example.com | Joanna    | Valdez   | d9u(*dsF4 |

@Jade
Scenario: Logged in user can navigate to songs index

	Given I am a user with first name '[string]'
	When I navigate to the songs index page
	Then I should be redirected to "https://localhost:7282/Songs"

Scenario: Logged in user clicks stop on a song, the page should refresh

	Given I am a user with first name '[string]'
	When I navigate to the songs index page
	And I click the stop button
	Then the page should refresh

Scenario: Logged in user clicks Create Song with no form data

	Given I am a user with first name '[string]'
	When I navigate to the songs index page
	And I click the Create Song button
	Then the page should do nothing

Scenario: Logged in user clicks Create Song with form data

	Given I am a user with first name '[string]'
	When I navigate to the songs index page
	And I enter text into the song purpose box
	And I click the Create Song button
	And I enter text into the login box
	And I click submit
	Then I should see a new song appear
