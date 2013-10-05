using UnityEngine;
using System.Collections;
using Shouldly;
using Require;

public class HasAMailboxTest : TestBehaviour
{
    string moneyReceived;
    int helloHandled;
    bool helloReturned;
    HasAMailbox it;

    public override void Spec()
    {
        Given("it has a mailbox").And("it receives the letter 'hello'").When("it looks for a hello letter").Then("the hello handler should be run 1 times");
        Given("it has a mailbox").And("it receives the letter 'hello'").And("it receives the letter 'hello'").When("it looks for a hello letter").Then("the hello handler should be run 1 times");
        Given("it has a mailbox").And("it looks for a hello letter").When("it receives the letter 'hello'").Then("the hello handler should be run 0 times");
        Given("it has a mailbox").And("it receives the letter 'hello and goodbye'").When("it looks for a hello letter").Then("the hello handler should be run 0 times");
        Given("it has a mailbox").And("it receives the letter 'here is 5 dollars'").When(@"it looks for some money").Then("the money handler should receive '5' dollars");
        Given("it has a mailbox").And("it receives the letter 'hello'").When("it looks for a hello letter").Then("it should return 'True'");
        Given("it has a mailbox").And("it receives the letter 'goodbye'").When("it looks for a hello letter").Then("it should return 'False'");
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
        helloReturned = it.On("hello", _ => helloHandled++);
    }

    public void ItLooksForSomeMoney()
    {
        it.On(@"here is (\d+) dollars", _ => moneyReceived = _[0]);
    }

    public void TheHelloHandlerShouldBeRun__Times(int times)
    {
        helloHandled.ShouldBe(times);
    }

    public void TheMoneyHandlerShouldReceive__Dollars(string dollars)
    {
        moneyReceived.ShouldBe(dollars);
    }

    public void ItShouldReturn__(string ret)
    {
        helloReturned.ToString().ShouldBe(ret);
    }
}
