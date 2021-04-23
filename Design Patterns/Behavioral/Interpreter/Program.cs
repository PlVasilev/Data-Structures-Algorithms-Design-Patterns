using System;
using System.Collections;

namespace Interpreter
{
    // A way to include language elements in a program
    //  Given a language, define a representation for its grammar along with an interpreter that uses the representation to interpret sentences in the language.

    // This structural code demonstrates the Interpreter patterns, which using a defined grammer,
    // provides the interpreter that processes parsed statements.
    class MainApp
    {
        static void Main()
        {
            Context context = new Context();

            // Usually a tree 
            ArrayList list = new ArrayList();

            // Populate 'abstract syntax tree' 
            list.Add(new TerminalExpression());
            list.Add(new NonterminalExpression());
            list.Add(new TerminalExpression());
            list.Add(new TerminalExpression());

            // Interpret
            foreach (AbstractExpression exp in list)
            {
                exp.Interpret(context);
            }

            // Wait for user
            Console.ReadKey();
        }
    }

    class Context { }

    abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }

    class TerminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("Called Terminal.Interpret()");
        }
    }

    class NonterminalExpression : AbstractExpression
    {
        public override void Interpret(Context context)
        {
            Console.WriteLine("Called Nonterminal.Interpret()");
        }
    }
}
