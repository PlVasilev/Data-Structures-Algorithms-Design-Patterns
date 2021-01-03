namespace Collection_of_Persons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class PersonCollectionSlow : IPersonCollection
    {
        // TODO: define the underlying data structures here ...
        public List<Person> people { get; set; } = new List<Person>();

        public bool AddPerson(string email, string name, int age, string town)
        {
            var person = people.FirstOrDefault(p => p.Email == email);
            if (person == null)
            {
                people.Add(new Person()
                {
                    Email = email,
                    Age = age,
                    Name = name,
                    Town = town
                });
            }
            return person == null;
        }

        public int Count { get => people.Count; }
        public Person FindPerson(string email)
        {
            return people.FirstOrDefault(p => p.Email == email);
        }

        public bool DeletePerson(string email)
        {
            var person = people.FirstOrDefault(p => p.Email == email);
            if (person != null)
            {
                people.Remove(person);
                return true;
            }
            return false;
        }

        public IEnumerable<Person> FindPersons(string emailDomain)
        {
            Regex matcher = new Regex($"@({emailDomain})(?!\\S)");
            return people.Where(p => 
            p.Email.Split('@')[1] == emailDomain)
            //matcher.IsMatch(p.Email))
                .OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(string name, string town)
        {
            return people.Where(p => p.Name == name && p.Town == town).OrderBy(p => p.Email);
        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge)
        {
            return people.Where(p => p.Age >= startAge && p.Age <= endAge)
                .OrderBy(p => p, new PersonComparerSlow());

        }

        public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
        {
            return people.Where(p => p.Age >= startAge && p.Age <= endAge && p.Town == town)
                 .OrderBy(p => p.Age)
                 .ThenBy(p => p.Email);
        }

        
    }

    internal class PersonComparerSlow : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            var cmp = x.Age.CompareTo(y.Age);
            return cmp == 0 ? x.Email.CompareTo(y.Email) : cmp;
        }
    }
}
