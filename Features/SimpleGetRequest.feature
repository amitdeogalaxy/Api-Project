Feature: SimpleGetRequest

@smoke testing
Scenario: Creating a simple Get Request
	Given I have Api url
	And I have URI
	When I hit a Restsharp Get Request
	Then I Verify the Result
