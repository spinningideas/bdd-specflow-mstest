Feature: Simple Addition Feature
	In order to see specflow work
	As a specflow user
	I want to see simple addition of two numbers

@basicscenario
Scenario: Add two numbers
	Given the first number is 5
	And the second number is 7
	When the two numbers are added
	Then the result should be 12