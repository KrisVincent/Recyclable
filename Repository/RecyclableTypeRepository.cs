using Microsoft.Data.SqlClient;
using Recyclable.Models;
using Recyclable.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Recyclable.Repository
{
    public class RecyclableTypeRepository : IRecyclableTypeRepository
    {
        private readonly string connectionString;
        public RecyclableTypeRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public void UpdateRecycleType(RecyclableType recyclableType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "EXEC UpdateRecyclableType @Id, @Type, @Rate, @MinKg, @MaxKg;";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", recyclableType.Id);
                    command.Parameters.AddWithValue("@Type", recyclableType.Type);
                    command.Parameters.AddWithValue("@Rate", recyclableType.Rate);
                    command.Parameters.AddWithValue("@MinKg", recyclableType.MinKg);
                    command.Parameters.AddWithValue("@MaxKg", recyclableType.MaxKg);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddRecyclableType(RecyclableType recyclableType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = "EXEC InsertRecyclableType @Type, @Rate, @MinKg, @MaxKg;";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Type", recyclableType.Type);
                    command.Parameters.AddWithValue("@Rate", recyclableType.Rate);
                    command.Parameters.AddWithValue("@MinKg", recyclableType.MinKg);
                    command.Parameters.AddWithValue("@MaxKg", recyclableType.MaxKg);

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<RecyclableType> GetRecyclableTypes()
        {
            List<RecyclableType> recyclableTypes = new List<RecyclableType>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "EXEC GetRecyclableTypes";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RecyclableType recyclableType = new RecyclableType
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Type = reader["Type"].ToString(),
                                Rate = Convert.ToDecimal(reader["Rate"]),
                                MinKg = Convert.ToDecimal(reader["MinKg"]),
                                MaxKg = Convert.ToDecimal(reader["MaxKg"])
                            };

                            recyclableTypes.Add(recyclableType);
                        }
                    }
                }
            }

            return recyclableTypes;
        }

        public RecyclableType GetRecyclableType(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "EXEC GetRecyclableTypeById @Id;";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id); 


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                             
                            return new RecyclableType
                            {
                                Id = (int)reader["Id"],
                                Type = (string)reader["Type"],
                                Rate = (decimal)reader["Rate"],
                                MinKg = (decimal)reader["MinKg"],
                                MaxKg = (decimal)reader["MaxKg"]
                           
                            };
                        }
                    }
                }
            }
            // Return just in case nothing found
            return null;
        }

        public void EditRecycleType(RecyclableType recyclableType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "EXEC UpdateRecyclableType @Id, @Type, @Rate, @MinKg, @MaxKg;";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", recyclableType.Id);
                    command.Parameters.AddWithValue("@Type", recyclableType.Type);
                    command.Parameters.AddWithValue("@Rate", recyclableType.Rate);
                    command.Parameters.AddWithValue("@MinKg", recyclableType.MinKg);
                    command.Parameters.AddWithValue("@MaxKg", recyclableType.MaxKg);

                    command.ExecuteNonQuery();
                }
            }

        }

        public void DeleteRecycleType(int recycleTypeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "EXEC DeleteRecyclableType @Id;";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", recycleTypeId);  

                    command.ExecuteNonQuery();
                }
            }
        }

         
        public int GetRecyclableTypeIdByType(string RecyclableType)
        {
            int typeId = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "EXEC GetRecyclableTypeIdByType @RecyclableType;";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
         
                    command.Parameters.AddWithValue("@RecyclableType", RecyclableType);

                    object result = command.ExecuteScalar();
                    
                    typeId = Convert.ToInt32(result);
                }
            }

            return typeId;
        }

        public RecyclableType GetRecyclableTypeByType(string type)
        {
            string query = "EXEC GetRecyclableByType @Type;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Type", type);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Map the data to a RecyclableType object
                            RecyclableType recyclableType = new RecyclableType
                            {
                                Id = (int)reader["Id"],
                                Type = (string)reader["Type"],
                                Rate = (decimal)reader["Rate"],
                                MinKg = (decimal)reader["MinKg"],
                                MaxKg = (decimal)reader["MaxKg"]
                            };

                            return recyclableType;
                        }
                    }
                }
            }

            return null; 

        }

        public IEnumerable<RecyclableItemListViewModel> GetRecyclableItems(int recyclableTypeId)
        {
            List<RecyclableItemListViewModel> recyclableItems = new List<RecyclableItemListViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "EXEC GetRecyclableItemsByTypeId @RecyclableTypeId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RecyclableTypeId", recyclableTypeId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RecyclableItemListViewModel recyclableItem = new RecyclableItemListViewModel
                            {
                                Type = reader["Type"].ToString(),
                                Weight = Convert.ToDecimal(reader["Weight"]),
                                ComputedRate = Convert.ToDecimal(reader["ComputedRate"]),
                                ItemDescription = reader["ItemDescription"].ToString()
                          
                            };

                            recyclableItems.Add(recyclableItem);
                        }
                    }
                }
            }

            return recyclableItems;
        }
    }
}
