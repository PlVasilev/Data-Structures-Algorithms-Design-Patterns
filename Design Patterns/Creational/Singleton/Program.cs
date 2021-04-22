using System;

namespace Singleton
{
    // A class of which only a single instance can exist

    // Ensure a class has only one instance and provide a global point of access to it.

    // This structural code demonstrates the Singleton pattern which assures only a single instance (the singleton) of the class can be created.
    class MainApp
    {
        static void Main()
        {
            // Constructor is protected -- cannot use new
            Singleton s1 = Singleton.Instance();
            Singleton s2 = Singleton.Instance();

            // Test for same instance
            if (s1 == s2)
            {
                Console.WriteLine("Objects are the same instance");
            }

            // Wait for user
            Console.ReadKey();
        }
    }

    class Singleton
    {
        private static Singleton _instance;

        // Constructor is 'protected'
        protected Singleton() { }

        public static Singleton Instance()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (_instance == null)
            {
                _instance = new Singleton();
            }

            return _instance;
        }
    }
}
