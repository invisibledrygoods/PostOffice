using UnityEngine;
using System.Collections;
using Shouldly;
using Require;

public class HasAMailboxTest : TestBehaviour
{
    float moneyReceived;
    int helloHandled;
    bool returnValue;
    HasAMailbox it;

    public override void Spec()
    {
        Given("it has a mailbox")
            .And("it receives the letter 'hello'")
            .When("it looks for a hello letter")
            .Then("the hello handler should be run 1 times")
            .Because("it should receive letters");
            
        Given("it has a mailbox")
            .And("it receives the letter 'hello'")
            .And("it receives the letter 'hello'")
            .When("it looks for a hello letter")
            .Then("the hello handler should be run 1 times")
            .Because("it should receive letters one at a time");
            
        Given("it has a mailbox")
            .And("it looks for a hello letter")
            .When("it receives the letter 'hello'")
            .Then("the hello handler should be run 0 times")
            .Because("it shouldn't receive letters before they arrive");
            
        Given("it has a mailbox")
            .And("it receives the letter 'hello and goodbye'")
            .When("it looks for a hello letter")
            .Then("the hello handler should be run 0 times")
            .Because("it shouldn't be able to receive partial letters");
            
        Given("it has a mailbox")
            .And("it receives the letter 'here is 5.35 dollars'")
            .When(@"it looks for some money")
            .Then("the money handler should receive 5.35 dollars")
            .Because("it should be able to receive floats");
            
        Given("it has a mailbox")
            .And("it receives the letter 'here is 5 dollars'")
            .When(@"it looks for some money")
            .Then("the money handler should receive 5 dollars")
            .Because("it should be able to receive ints");
            
        Given("it has a mailbox")
            .And("it receives the letter 'hello'")
            .When("it looks for a hello letter")
            .Then("it should return 'True'")
            .Because("Read should return true if the letter existed");
            
        Given("it has a mailbox")
            .And("it receives the letter 'goodbye'")
            .When("it looks for a hello letter")
            .Then("it should return 'False'")
            .Because("Read should return false if the letter didn't exist");
            
        Given("it has a mailbox")
            .And("it receives the letter 'have    some extra    spaces'")
            .When("it looks for a letter without extra spaces")
            .Then("it should return 'True'")
            .Because("white space should be ignored");
            
        Given("it has a mailbox")
            .And("it receives the letter 'hello'")
            .When("it looks for a hello letter")
            .And("it looks for a hello letter")
            .Then("the hello handler should be run 1 times")
            .Because("it should only receive each letter once");
    }

    public void ItHasAMailbox()
    {
        it = transform.Require<HasAMailbox>();
    }

    public void ItReceivesTheLetter__(string letter)
    {
        it.Send(letter);
    }

    public void ItLooksForAHelloLetter()
    {
        returnValue = it.Read("hello", _ => helloHandled++);
    }

    public void ItLooksForSomeMoney()
    {
        returnValue = it.Read(@"here is __ dollars", _ => moneyReceived = _[0]);
    }

    public void ItLooksForALetterWithoutExtraSpaces()
    {
        returnValue = it.Read(@"have some extra spaces", _ => { });
    }

    public void TheHelloHandlerShouldBeRun__Times(int times)
    {
        helloHandled.ShouldBe(times);
    }

    public void TheMoneyHandlerShouldReceive__Dollars(float dollars)
    {
        moneyReceived.ShouldBe(dollars);
    }

    public void ItShouldReturn__(string ret)
    {
        returnValue.ToString().ShouldBe(ret);
    }
}
