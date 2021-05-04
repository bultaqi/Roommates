using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Roommates.Models;


// Classes that are responsible for interacting with databases are called repositories


namespace Roommates.Repositories
{
    //Inheriting the BaseRepository
    class RoommateRepository : BaseRepository
    {
        //New instantiaton, we have to pass the connection to the BaseRepository
        public RoommateRepository(string connectionString) : base(connectionString) { }

        // Methods of the class Roommate"Repository"
        // This first one will get all the roomates from the database
        public List<Roommate> GetAll()
        {
            // "using' the database connection 
            // we properly close and disconnect from a resource even if there is an error.
            using (SqlConnection conn = Connection)
            {
                conn.Open();


                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // Setting up the SQL command before we execute it
                    cmd.CommandText = @"select FirstName, LastName, RentPortion, MoveInDate, RoomId, Name, MaxOccupancy
                                        from Roommate
                                        left join Room on Room.Id = Roommate.RoomId";
                    
                    // 
                    SqlDataReader reader = cmd.ExecuteReader();

                    // This list holds the data we get back
                    List<Room> roomies = new List<Room>();

                    // The reader function will continue to run as long as data is continuing to be returned
                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idvalue = reader.GetInt32(idColumnPosition);

                        int firstNameColumnPosition = reader.GetOrdinal("FirstName");
                        string firstNameValue = reader.GetString(firstNameColumnPosition);

                        int lastNameColumnPosition = reader.GetOrdinal("LastName");
                        string lastNameValue = reader.GetString(lastNameColumnPosition);

                        int rentPortionColumnPosition = reader.GetOrdinal("RentPortion");
                        string rentPortionValue = reader.GetString(rentPortionColumnPosition);

                        int roomIdColumnPosition = reader.GetOrdinal("RoomId");
                        string roomIdValue = reader.GetString(roomIdColumnPosition);
                    }





                }
            }



        }




    }
}
