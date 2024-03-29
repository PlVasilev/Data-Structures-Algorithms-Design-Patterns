﻿using System;

namespace Mediator
{
    // Defines simplified communication between classes
    // Define an object that encapsulates how a set of objects interact.
    // Mediator promotes loose coupling by keeping objects from referring to each other explicitly, and it lets you vary their interaction independently.

    // This structural code demonstrates the Mediator pattern facilitating loosely coupled communication between
    // different objects and object types. The mediator is a central hub through which all interaction must take place.
    class MainApp
    {
        static void Main()
        {
            ConcreteMediator m = new ConcreteMediator();

            ConcreteColleague1 c1 = new ConcreteColleague1(m);
            ConcreteColleague2 c2 = new ConcreteColleague2(m);

            m.Colleague1 = c1;
            m.Colleague2 = c2;

            c1.Send("How are you?");
            c2.Send("Fine, thanks");

            // Wait for user
            Console.ReadKey();
        }
    }

    abstract class Mediator
    {
        public abstract void Send(string message,
          Colleague colleague);
    }

    class ConcreteMediator : Mediator
    {
        private ConcreteColleague1 _colleague1;
        private ConcreteColleague2 _colleague2;

        public ConcreteColleague1 Colleague1
        {
            set { _colleague1 = value; }
        }

        public ConcreteColleague2 Colleague2
        {
            set { _colleague2 = value; }
        }

        public override void Send(string message,
          Colleague colleague)
        {
            if (colleague == _colleague1)
            {
                _colleague2.Notify(message);
            }
            else

            {
                _colleague1.Notify(message);
            }
        }
    }

    abstract class Colleague
    {
        protected Mediator mediator;

        // Constructor
        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }

    class ConcreteColleague1 : Colleague
    {
        // Constructor
        public ConcreteColleague1(Mediator mediator) : base(mediator) { }

        public void Send(string message)
        {
            mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine("Colleague1 gets message: "

              + message);
        }
    }

    class ConcreteColleague2 : Colleague
    {
        // Constructor
        public ConcreteColleague2(Mediator mediator) : base(mediator) { }

        public void Send(string message)
        {
            mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine("Colleague2 gets message: "

              + message);
        }
    }
}
