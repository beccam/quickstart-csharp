# DataStax C# Driver for Apache Cassandra Quickstart

A basic C#/.NET CRUD application using the DataStax C# Driver for Apache Cassandra. 
The intent is to help users get up and running quickly with the driver. 
If you are having trouble, the complete code solution for `Program.cs` can be found [here](https://gist.github.com/beccam/d8491990895fe659e0584a4bc31d1df3).

## Prerequisites
  * A running instance of [Apache Cassandra®](http://cassandra.apache.org/download/) 1.2+
  * [MircoSoft Visual Studio](https://visualstudio.microsoft.com/vs/) with .NET Core 1+
  
  
## Create the keyspace and table
The `users.cql` file provides the schema used for this project:

```sql
CREATE KEYSPACE demo
    WITH replication = {'class': 'SimpleStrategy', 'replication_factor': '1'};

CREATE TABLE demo.users (
    lastname text PRIMARY KEY,
    age int,
    city text,
    email text,
    firstname text);
```

## Connect to your cluster

All of our code is contained in the `GettingStarted` class. 
Note how the main method creates a session to connect to our cluster and runs the CRUD operations against it. 
Replace the default parameters in `CqlSession.builder()` with your own hostname, port and datacenter.

```csharp
// TO DO: Fill in your own host, port, and data center
Cluster cluster = Cluster.Builder()
                         .AddContactPoint("127.0.0.1")
                         .Build();
ISession session = cluster.Connect("demo");
```

## CRUD Operations
Fill the code in the methods that will add a user, get a user, update a user and delete a user from the table with the driver.

### INSERT a user
```csharp
private static void SetUser(ISession session, String lastname, int age, String city, String email, String firstname) {
    
    //TO DO: execute SimpleStatement that inserts one user into the table
    var statement = new SimpleStatement("INSERT INTO users (lastname, age, city, email, firstname) VALUES (?,?,?,?,?)", lastname, age, city, email, firstname);

    session.Execute(statement);
}
```

```csharp
### SELECT a user
 private static void GetUser(ISession session, String lastname){

      //TO DO: execute SimpleStatement that retrieves one user from the table
      //TO DO: print firstname and age of user
      var statement = new SimpleStatement("SELECT * FROM users WHERE lastname = ?", lastname);
      
      var result = session.Execute(statement).First();
      Console.WriteLine("{0} {1}", result["firstname"], result["age"]);

}
```

### UPDATE a user's age
```csharp
private static void UpdateUser(ISession session, int age, String lastname) {

    //TO DO: execute SimpleStatement that updates the age of one user
    var statement = new SimpleStatement("UPDATE users SET age =? WHERE lastname = ?", age, lastname);

    session.Execute(statement);
}
```   

### DELETE a user
```csharp
private static void DeleteUser(ISession session, String lastname) {

    //TO DO: execute SimpleStatement that deletes one user from the table
    var statement = new SimpleStatement("DELETE FROM users WHERE lastname = ?", lastname);

    session.Execute(statement);
}
```
    



