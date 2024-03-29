﻿# Sample Scenarios for Selenium based search tests.
# Scenerio Names become the test names
#
# Feature name appended with StepDefinitions is the default step C# class name
Feature: BDD Driven Search Engines
	In order to protect tradmark holders
	As a search that includes a trademark
	I want the results to include the tradmark holder site

#Scenario: Example - Search with Google
#	Given I search the internet using site "google"
#	When I use the term "microsoft"
#	Then There should be at least 1 links with the trademark holder site "microsoft.com" in them
#	And My search term should be in the title bar

Scenario: Example - Search with Bing
	Given I search the internet using site "bing"
	When I use the term "microsoft"
	Then There should be at least 1 links with the trademark holder site "microsoft.com" in them
#	And My search term should be in the title bar

Scenario Outline: Example - Search and Title Matches
	Given I search the internet using site "<engine>"
	When I use the term "<criteria>"
	Then There should be at least <domain count> links with the trademark holder site "<domain>" in them
#	And My search term should be in the title bar
	Examples: 
	  | engine | criteria | domain       | domain count |
	  | bing   | amazon   | amazon.com   | 1            |
	  | bing   | facebook | facebook.com | 1            |
#	  | google | facebook | facebook.com | 1            |
#	  | google | amazon   | amazon.com   | 1            |
