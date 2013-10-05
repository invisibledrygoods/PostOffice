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
        Given("it has a mailbox").And("it receives the letter 'hello'").When("it looks for a hello letter").Then("the hello handler should be run 1 times");
        Given("it has a mailbox").And("it receives the letter 'hello'").And("it receives the letter 'hello'").When("it looks for a hello letter").Then("the hello handler should be run 1 times");
        Given("it has a mailbox").And("it looks for a hello letter").When("it receives the letter 'hello'").Then("the hello handler should be run 0 times");
        Given("it has a mailbox").And("it receives the letter 'hello and goodbye'").When("it looks for a hello letter").Then("the hello handler should be run 0 times");
        Given("it has a mailbox").And("it receives the letter 'here is 5.35 dollars'").When(@"it looks for some money").Then("the money handler should receive 5.35 dollars");
        Given("it has a mailbox").And("it receives the letter 'here is 5 dollars'").When(@"it looks for some money").Then("the money handler should receive 5 dollars");
        Given("it has a mailbox").And("it receives the letter 'hello'").When("it looks for a hello letter").Then("it should return 'True'");
        Given("it has a mailbox").And("it receives the letter 'goodbye'").When("it looks for a hello letter").Then("it should return 'False'");
        Given("it has a mailbox").And("it receives the letter 'have    some extra    spaces'").When("it looks for a letter without extra spaces").Then("it should return 'True'");
        Given("it has a mailbox").And("it receives the letter 'hello'").When("it looks for a hello letter").And("it looks for a hello letter").Then("the hello handler should be run 1 times");
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
        returnValue = it.On("hello", _ => helloHandled++);
    }

    public void ItLooksForSomeMoney()
    {
        returnValue = it.On(@"here is __ dollars", _ => moneyReceived = _[0]);
    }

    public void ItLooksForALetterWithoutExtraSpaces()
    {
        returnValue = it.On(@"have some extra spaces", _ => { });
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
