# Sample Scenarios for Selenium based search tests.
# Scenerio Names become the test names
#
# Feature name appended with StepDefinitions is the default step C# class name
Feature: BDD Driven Google Search
	In order to find good stuff on the web
	As a search engine user
	I want to to search for stuff

Scenario: Example - Search with Google
	Given I want to search with "google"
	When When I search for "microsoft"
	Then My search term should be in the title bar
	And There should be at least 1 links with the "microsoft.com" in them
	And I can click on the first link

Scenario: Example - Search with Bing
	Given I want to search with "bing"
	When When I search for "microsoft"
	Then My search term should be in the title bar
	And There should be at least 1 links with the "microsoft.com" in them
	And I can click on the first link

Scenario Outline: Example - Search and Title Matches
	Given I want to search with "<engine>"
	When When I search for "<criteria>"
	Then My search term should be in the title bar
	And There should be at least <domain count> links with the "<domain>" in them
	Examples: 
	  | engine | criteria | domain       | domain count |
	  | google | amazon   | amazon.com   | 1            |
	  | bing   | amazon   | amazon.com   | 1            |
	  | google | facebook | facebook.com | 1            |
	  | bing   | facebook | facebook.com | 1            |
