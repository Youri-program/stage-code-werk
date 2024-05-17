using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text.Json;
using System.Xml.Linq;

// Variables for JSON data
string kingsJsonData = File.ReadAllText("koning.json");
string castleJsonData = File.ReadAllText("kasteel.json");
// Deserialize the JsonData into a object-list with 'JsonSerializer' method.
var kings = JsonSerializer.Deserialize<List<King>>(kingsJsonData);
var castles = JsonSerializer.Deserialize<List<Castle>>(castleJsonData);

// Making 'selection menu' with a switch function within a do while function.
do
{
    // Here you can just select a number of your choice. View the kings, castles or just quit the application.
    Console.WriteLine("\nWhich JSON file do you want to use?: ");
    Console.WriteLine("1: 'koning.json'.");
    Console.WriteLine("2: 'kasteel.json'.");
    Console.WriteLine("3: Quit.");
    // Begin switch statement, every case has its own output of course.
    switch (Console.ReadLine())
    {
        case "1":
            // 'kings' object needs to be not null, otherwise I get a warning that 'kings' may be null
            //if (kings != null)
            //{
            //    // foreach statement loops through each king
            //    foreach (var king in kings)
            //    {
            //        // Prints every variable from the class king
            //        Console.WriteLine($"{king.Id} {king.CastleId} {king.Name} {king.Location} {king.Year}");
            //    }
            //}
            //var db = new KingsandCastlesDB();
            //db.Kings?.AddRange(kings);
            //db.SaveChanges();
            //Console.WriteLine("Data in DB!");

            // Select what CRUD function you want to do with 'koning.json' file
            Console.WriteLine("\nWhat do you want to do with 'koning.json'?");
            Console.WriteLine("1: CREATE/INSERT new data");
            Console.WriteLine("2: READ excisting data");
            Console.WriteLine("3: UPDATE excisting data");
            Console.WriteLine("4: DELETE excisting data");
            Console.WriteLine("5: Go back <--\n");

            // Calling CRUD functions method for the 'koning.json' file
            kingCRUDFunctions();

            break;
        case "2":
            //if (castles != null)
            //{
            //    foreach (var castle in castles)
            //    {
            //        Console.WriteLine($"{castle.Id} {castle.Name}");
            //    }
            //}
            //var db2 = new KingsandCastlesDB();
            //db2.Castles?.AddRange(castles);
            //db2.SaveChanges();
            //Console.WriteLine("Data in DB!");

            // Select what CRUD function you want to do with 'kasteel.json' file
            Console.WriteLine("\nWhat do you want to do with 'kasteel.json'?");
            Console.WriteLine("1: CREATE/INSERT new data");
            Console.WriteLine("2: READ excisting data");
            Console.WriteLine("3: UPDATE excisting data");
            Console.WriteLine("4: DELETE excisting data");
            Console.WriteLine("5: Go back <--\n");

            // Calling CRUD functions method for the 'kasteel.json' file
            castleCRUDFunctions();

            break;
        case "3":
            Console.WriteLine("See you soon!");
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Please choose one of the options above.");
            break;
    }
} while (true);

// Start CRUD method for kings
void kingCRUDFunctions()
{
    SqlConnection sqlConnection;
    string connectionString = @"Server=localhost\SQLEXPRESS;Database=KingsandCastlesDB;Trusted_Connection=True;";
    sqlConnection = new SqlConnection(connectionString);
    sqlConnection.Open();
    Console.WriteLine("\nConnection successfully established!");
    do
    {
        switch (Console.ReadLine())
        {
            case "1":
                try
                {
                    // Creating data
                    Console.WriteLine("\n>CREATE DATA<");
                    Console.WriteLine("\nEnter kings name:");
                    string? kingName = Console.ReadLine();
                    Console.WriteLine("Enter location:");
                    string? kingLocation = Console.ReadLine();
                    Console.WriteLine("Enter year:");
                    int kingYear = int.Parse(Console.ReadLine());
                    //Console.WriteLine("Please also fill in a CastleID:");
                    //int castleId = int.Parse(Console.ReadLine());
                    string insertQuery = "INSER INTO Kings(Name, Location, Year) VALUES('" + kingName + "','" + kingLocation + "', " + kingYear + ")"; // T is gone.
                    SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                    insertCommand.ExecuteNonQuery();
                    Console.WriteLine("\nSuccesfully inserted into table!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            case "2":
                try
                {
                    // Reading data
                    Console.WriteLine("\n>READ DATA<");
                    string readingQuery = "SELECT * FROM Koningen"; // Correct table name is Kings.
                    SqlCommand readingCommand = new SqlCommand(readingQuery, sqlConnection);
                    SqlDataReader dataReader = readingCommand.ExecuteReader();
                    Console.WriteLine("{0, -6}{1, -15}{2, -21}{3, 25}{4, 20}", "Id:", "CastleId:", "Name:", "Location:", "Year:");
                    while (dataReader.Read())
                    {
                        Console.WriteLine("{0, -6}{1, -15}{2, -21}{3, 25}{4, 20}", dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString(), dataReader.GetValue(4).ToString());
                    }
                    dataReader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            case "3":
                try
                {
                    // Updating data, FIRST you can read the data so you can look which ID you want to update.
                    string readingQuery = "SELECT * FROM Kings";
                    SqlCommand readingCommand = new SqlCommand(readingQuery, sqlConnection);
                    SqlDataReader dataReader = readingCommand.ExecuteReader();
                    Console.WriteLine("{0, -6}{1, -15}{2, -21}{3, 25}{4, 20}", "Id:", "CastleId:", "Name:", "Location:", "Year:");
                    while (dataReader.Read())
                    {
                        Console.WriteLine("{0, -6}{1, -15}{2, -21}{3, 25}{4, 20}", dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString(), dataReader.GetValue(4).ToString());
                    }
                    dataReader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                try
                {
                    // Actually updating data
                    Console.WriteLine("\n>UPDATE DATA<");
                    int userId;
                    int updatedYear;
                    Console.WriteLine("Enter ID you would like to update:");
                    userId = int.Parse(Console.ReadLine());

                    Console.WriteLine("What would you like to update?");
                    Console.WriteLine("1: Name");
                    Console.WriteLine("2: Location");
                    Console.WriteLine("3: Year");
                    Console.WriteLine("4: Go back\n");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("Enter new name:");
                            string? updatedName = Console.ReadLine();
                            string updateQueryName = "UPDATE Kings SET Name = " + updatedName + "' WHERE Id = " + userId; // ' Before updatedName
                            SqlCommand updateCommandName = new SqlCommand(updateQueryName, sqlConnection);
                            updateCommandName.ExecuteNonQuery();
                            Console.WriteLine("Name is successfully updated!");
                            return;
                        case "2":
                            Console.WriteLine("Enter new location:");
                            string? updatedLocation = Console.ReadLine();
                            string updateQueryLocation = "UPDATE Kings SET Name = '" + updatedLocation + "' WHERE Id = " + userId;
                            SqlCommand updateCommandLocation = new SqlCommand(updateQueryLocation, sqlConnection);
                            updateCommandLocation.ExecuteNonQuery();
                            Console.WriteLine("Location is successfully updated!");
                            return;
                        case "3":
                            Console.WriteLine("Enter new year:");
                            updatedYear = int.Parse(Console.ReadLine());
                            string updateQueryYear = "UPDATE Kings SET Name = '" + updatedYear + "' WHERE Id = " + userId;
                            SqlCommand updateCommandYear = new SqlCommand(updateQueryYear, sqlConnection);
                            updateCommandYear.ExecuteNonQuery();
                            Console.WriteLine("Year is successfully updated!");
                            return;
                        case "4":
                            Console.WriteLine("Going back <--");
                            return;
                        default:
                            Console.WriteLine("Please choose one of the options above.");
                            break;
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            case "4":
                try
                {
                    // Deleting data, FIRST you can read the data so you can look which ID you want to delete.
                    string readingQuery = "SELECT FROM Kings"; // * is gone
                    SqlCommand readingCommand = new SqlCommand(readingQuery, sqlConnection);
                    SqlDataReader dataReader = readingCommand.ExecuteReader();
                    Console.WriteLine("{0, -6}{1, -15}{2, -21}{3, 25}{4, 20}", "Id:", "CastleId:", "Name:", "Location:", "Year:");
                    while (dataReader.Read())
                    {
                        Console.WriteLine("{0, -6}{1, -15}{2, -21}{3, 25}{4, 20}", dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString(), dataReader.GetValue(2).ToString(), dataReader.GetValue(3).ToString(), dataReader.GetValue(4).ToString());
                    }
                    dataReader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                try
                {
                    // Actually DELETING data
                    Console.WriteLine("\n>DELETE DATA<");
                    int userId;
                    Console.WriteLine("Enter ID you would like to delete:");
                    userId =int.Parse(Console.ReadLine());
                    string deleteQuery = "DELETE FROM Kings WHERE Id  " + userId; // Missing a =   WHEN enterng a id that doesnt exist it also outputs a error.
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                    deleteCommand.ExecuteNonQuery();
                    Console.WriteLine("Id successfully deleted!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            case "5":
                Console.WriteLine("Going back <--");
                return;
            default:
                Console.WriteLine("Please choose one of the options above.");
                break;
        }
    } while (true);
    sqlConnection.Close();
}

// Start CRUD method for castles
void castleCRUDFunctions()
{
    SqlConnection sqlConnection;
    string connectionString = @"Server=localhost\SQLEXPRESS;Database=KingsandCastlesDB;Trusted_Connection=True;";
    sqlConnection = new SqlConnection(connectionString);
    sqlConnection.Open();
    Console.WriteLine("\nConnection successfully established!");
    do
    {
        switch (Console.ReadLine())
        {
            case "1":
                try
                {
                    // Creating data
                    Console.WriteLine("\n>CREATE DATA<");
                    Console.WriteLine("\nEnter castle name:");
                    string? castleName = Console.ReadLine();
                    string insertQuery = "INSERT INTO Castles(Name) VALUES('" + castleName + "')";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                    insertCommand.ExecuteNonQuery();
                    Console.WriteLine("\nSuccesfully inserted into table!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            case "2":
                try
                {
                    // Reading data
                    Console.WriteLine("\n>READ DATA<");
                    string readingQuery = "SELECT * FROM Castles";
                    SqlCommand readingCommand = new SqlCommand(readingQuery, sqlConnection);
                    SqlDataReader dataReader = readingCommand.ExecuteReader();
                    Console.WriteLine("{0, -6}{1, -15}", "Id:", "Name:");
                    while (dataReader.Read())
                    {
                        Console.WriteLine("{0, -6}{1, -15}", dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
                    }
                    dataReader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            case "3":
                try
                {
                    // Updating data, FIRST you can read the data so you can look which ID you want to update.
                    string readingQuery = "SELECT * FROM Castles";
                    SqlCommand readingCommand = new SqlCommand(readingQuery, sqlConnection);
                    SqlDataReader dataReader = readingCommand.ExecuteReader();
                    Console.WriteLine("{0, -6}{1, -15}", "Id:", "Name:");
                    while (dataReader.Read())
                    {
                        Console.WriteLine("{0, -6}{1, -15}", dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
                    }
                    dataReader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                try
                {
                    // Actually updating data
                    Console.WriteLine("\n>UPDATE DATA<");
                    int userId;
                    Console.WriteLine("Enter ID you would like to update:");
                    userId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter new name:");
                    string? updatedName = Console.ReadLine();
                    string updateQuery = "UPDATE Castles SET Name = '" + updatedName + "' WHERE Id = " + userId;
                    SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection);
                    updateCommand.ExecuteNonQuery();
                    Console.WriteLine("Data is successfully updated!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            case "4":
                try
                {
                    // Deleting data, FIRST you can read the data so you can look which ID you want to delete.
                    Console.WriteLine("\n>READ DATA<");
                    string readingQuery = "SELECT * FROM Castles";
                    SqlCommand readingCommand = new SqlCommand(readingQuery, sqlConnection);
                    SqlDataReader dataReader = readingCommand.ExecuteReader();
                    Console.WriteLine("{0, -6}{1, -15}", "Id:", "Name:");
                    while (dataReader.Read())
                    {
                        Console.WriteLine("{0, -6}{1, -15}", dataReader.GetValue(0).ToString(), dataReader.GetValue(1).ToString());
                    }
                    dataReader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                try
                {
                    // Actually DELETING data
                    Console.WriteLine("\n>DELETE DATA<");
                    int userId;
                    Console.WriteLine("Enter ID you would like to delete:");
                    userId = int.Parse(Console.ReadLine());
                    string deleteQuery = "DELETE FROM Castles WHERE Id = " + userId;
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                    deleteCommand.ExecuteNonQuery();
                    Console.WriteLine("Id successfully deleted!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return;
            case "5":
                Console.WriteLine("Going back <--");
                return;
            default:
                Console.WriteLine("Please choose one of the options above.");
                break;
        }
    } while (true);
    sqlConnection.Close();
}
