# Sample Scenarios for Selenium based search tests.
# Scenerio Names become the test names
#
# Feature name is the default step class name
Feature: BDD Driven Google Search
	In order to find stuff on the web
	As a google user
	I want to to search for stuff

Scenario: Example - Search with Google
	Given I want to search with "google"
	When When I search for "microsoft"
	Then My search term should be in the title bar
	And There should be at least 10 links with the search term in thm
	And I can click on the first link

Scenario: Example - Search with Bing
	Given I want to search with "bing"
	When When I search for "microsoft"
	Then My search term should be in the title bar
	And There should be at least 10 links with the search term in thm
	And I can click on the first link

Scenario Outline: Example - Search and Title Matches
	Given I want to search with "<engine>"
	When When I search for "<criteria>"
	Then My search term should be in the title bar
	Examples: 
	  | engine | criteria |
	  | google | amazon   |
	  | bing   | amazon   |
	  | google | facebook |
	  | bing   | facebook |
