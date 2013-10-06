PostOffice
==========

I needed to wait a while before receiving a message.

Examples
========

Open a mailbox

    HasAMailbox mailbox = transform.Require<HasAMailbox>();
    
send it a letter

    mailbox.Send("I hate you and am moving to Argentina.");
    
read it and cry quietly to yourself

    mailbox.Read("I hate you and am moving to Argentina.", args => {
      Cry();
    });
    
send some money

    mailbox.Send("Here's 400 dollars for the trip.");

pocket the money and stew on it

    mailbox.Read("Here's __ dollars for the trip.", args => {
      pocketMoney += args[0];
      Stew();
    });

Details
=======

 * Messages are case insensitive, whitespace insensitive and ignore characters that aren't `[a-zA-Z0-9.-_]`.
 * Numbers can be captured with __ (two underscores) and are passed into the args as float[].
 * Read() is a polling operation, checking the mail before it's arrived does nothing.
 * If two identical messages are in the mailbox, you have to call Read() twice to get them both out.
   Read() returns a boolean for success or fail. If you want to read all of them at once, just wrap it in a while loop.

What Its For
============

I'm mostly using this to send messages to state machines to be read next time
the state machine enters a state capable of handling the message. For instance
hitting a doorway collider fires off a `"teleport to (__, __, __)"` message. If you
happened to be in the middle of a sick-ass whirlwind kick while going through the
door, the state machine will hold off on checking its mail and transitioning to the
teleport state until it returns to the resting state.
