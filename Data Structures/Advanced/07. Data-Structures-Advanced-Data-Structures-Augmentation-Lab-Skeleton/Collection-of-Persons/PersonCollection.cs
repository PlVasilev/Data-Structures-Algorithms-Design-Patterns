using System.Linq;
using Wintellect.PowerCollections;

namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;

    public class PersonCollection : IPersonCollection
    {
        // TODO: define the underlying data structures here ...
        private Dictionary<string, Person> byEmail = new Dictionary<string, Person>();
        private Dictionary<string, SortedSet<Person>> byEDomain = 
            new Dictionary<string, SortedSet<Person>>();
        private Dictionary<string, SortedSet<Person>> byNameTown = 
            new Dictionary<string, SortedSet<Person>>();
        //  private OrderedDictionary<string, Person> byAgeTown2 = new OrderedDictionary<string, Person>();
        private SortedDictionary<int, Dictionary<string, SortedSet<Person>>> byAgeTown =
            new SortedDictionary<int, Dictionary<string, SortedSet<Person>>>();



        public bool AddPerson(string email, string name, int age, string town)
        {
            var person = FindPerson(email);
            if (person != null)
            {
                return false;
            }

            person = new Person()
            {
                Email = email,
                Age = age,
                Name = name,
                Town = town
            };
            
            byEmail[email] = person;

            var emailDomain = email.Split('@')[1];
            byEDomain.AppendValueToKey(emailDomain, person);

            var nameTown = GetNameTown(person);
            byNameTown.AppendValueToKey(nameTown, person);

            byAgeTown.EnsureKeyExists(age);
            byAgeTown[age].AppendValueToKey(town, person);

            return true;
        }

        private string GetNameTown(Person person)
        {
            return $"{person.Name}_{person.Town}";
        }

        public int Count
        {
            get => byEmail.Count;
        }
        public Person FindPerson(string email)
        {
            if (byEmail.ContainsKey(email))
            {
                return byEmail[email];
            }

            return null;
        }

        public bool DeletePerson(string email)
        {
            var person = FindPerson(email);
            if (person == null)
                return false;

            var age = person.Age;
            var town = person.Town;

            byEmail.Remove(email);
            var emailDomain = email.Split('@')[1];
            byEDomain[emailDomain].Remove(person);
            byEDomain.CleanupValueForKey(emailDomain);

            var nameTown = GetNameTown(person);
            byNameTown[nameTown].Remove(person);
            byNameTown.CleanupValueForKey(nameTown);

            byAgeTown[age][town].Remove(person);
            byAgeTown[age].CleanupValueForKey(town);
            byAgeTown.CleanupValueForKey(age);

            return true;


        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
           return byEDomain.GetValuesForKey(emailDomain);

        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            return byNameTown.GetValuesForKey(name + "_" + town);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            SortedSet<int> ages = new SortedSet<int>(byAgeTown.Keys);
            var resultKeys = ages.GetViewBetween(startAge, endAge);
            return resultKeys.SelectMany(k => byAgeTown[k].Values.SelectMany(p => p).OrderBy(p => p.Email));
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            SortedSet<int> ages = new SortedSet<int>(byAgeTown.Keys);
            var resultKeys = ages.GetViewBetween(startAge, endAge);
            return resultKeys.SelectMany(k => byAgeTown[k].GetValuesForKey(town).OrderBy(p => p.Email));
        }
    }

    internal class PersonComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            var cmp = x.Age.CompareTo(y.Age);
            return cmp == 0 ? x.Email.CompareTo(y.Email) : cmp;
        }
    }
}
