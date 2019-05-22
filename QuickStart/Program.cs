using Cassandra;
using System.Linq;
using System;

namespace QuickStart
{
    class Program
    {
        static void Main(string[] args)
        { 
            // Connect to the "demo" keyspace on our cluster running at 127.0.0.1
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("demo");

            SetUser(session, "Jones", 35, "Austin", "bob@example.com", "Bob");

            GetUser(session, "Jones");

            UpdateUser(session, 36, "Jones");

            GetUser(session, "Jones");

            DeleteUser(session, "Jones");
        }

        private static void SetUser(ISession session, String lastname, int age, String city, String email, String firstname)
        {
            var statement = new SimpleStatement("INSERT INTO users (lastname, age, city, email, firstname) VALUES (?,?,?,?,?)", lastname, age, city, email, firstname);

            session.Execute(statement);

        }

        private static void GetUser(ISession session, String lastname)
        {
            var statement = new SimpleStatement("SELECT * FROM users WHERE lastname = ?", lastname);
            var result = session.Execute(statement).First();
            Console.WriteLine("{0} {1}", result["firstname"], result["age"]);

        }
       
        public static void UpdateUser(ISession session, int age, String lastname)
        {

            var statement = new SimpleStatement("UPDATE users SET age =? WHERE lastname = ?", age, lastname);

            session.Execute(statement);
        }

        public static void DeleteUser(ISession session, String lastname)
        {

            var statement = new SimpleStatement("DELETE FROM users WHERE lastname = ?", lastname);

            session.Execute(statement);
        }
       
    }
}