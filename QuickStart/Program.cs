using Cassandra;
using System.Linq;
using System;

namespace QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {

            // Connect to the demo keyspace on our cluster running at 127.0.0.1
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("demo");

            SetUser(session, "Jones", 35, "Austin", "bob@example.com", "Bob");

            GetUser(session, "Jones");

            UpdateUser(session, 36, "Jones");

            GetUser(session, "Jones");

            DeleteUser(session, "Jones");
        }

        public static void SetUser(ISession session, String lastname, int age, String city, String email, String firstname)
        {
            // Insert Bob
            session.Execute("INSERT INTO users (lastname, age, city, email, firstname) VALUES " +
                " ('" + lastname + "'," + age + ", '" + city + "', '" + email + "', '" + firstname + "')");
        }

        public static void GetUser(ISession session, String lastname)
        {

            var result = session.Execute("select * from users WHERE lastname = '" + lastname + "'");
            var row = result.First();
            Console.WriteLine("{0} {1}", row["firstname"], row["age"]);
        }


        public static void UpdateUser(ISession session, int age, String lastname)
        {

            session.Execute("UPDATE users SET age =" + age + " WHERE lastname = '" + lastname + "'");
        }

        public static void DeleteUser(ISession session, String lastname)
        {

            session.Execute("DELETE FROM users WHERE lastname = '" + lastname + "'");
        }
    }
}