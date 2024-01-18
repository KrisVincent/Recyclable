using Microsoft.Data.SqlClient;
using Recyclable.Interface;
using Recyclable.Models;
using System;

namespace Recyclable.Repository
{
    public class RecyclableItemRepository : IRecyclableItemRepository
    {

        private readonly string connectionString;
        public RecyclableItemRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public void AddRecyclableItem(RecyclableItem RecyclableItem)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string insertQuery = @"EXEC InsertRecyclableItem
                                            @TypeId,
                                            @Weight,
                                            @ComputedRate,
                                            @ItemDescription;";

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@TypeId", RecyclableItem.TypeId);
                    command.Parameters.AddWithValue("@Weight", RecyclableItem.Weight);
                    command.Parameters.AddWithValue("@ComputedRate", RecyclableItem.ComputedRate);
                    command.Parameters.AddWithValue("@ItemDescription", RecyclableItem.ItemDescription);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRecyclableItem(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL command to delete a recyclable item by id
                string selectQuery = "EXEC DeleteRecyclableItem @Id;";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    // Add parameters to the SQL command
                    command.Parameters.AddWithValue("@Id", id);

                    // Execute the SQL command
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditRecyclableItem(RecyclableItem RecyclableItem)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct the UPDATE query
                string updateQuery = @" EXEC UpdateRecyclableItem
                                            @Id,
                                            @TypeId,
                                            @Weight,
                                            @ComputedRate,
                                            @ItemDescription;";

                using (SqlCommand cmd = new SqlCommand(updateQuery, connection))
                {
                    // Set parameters
                    cmd.Parameters.AddWithValue("@TypeId", RecyclableItem.TypeId);
                    cmd.Parameters.AddWithValue("@Weight", RecyclableItem.Weight);
                    cmd.Parameters.AddWithValue("@ComputedRate", RecyclableItem.ComputedRate);
                    cmd.Parameters.AddWithValue("@ItemDescription", RecyclableItem.ItemDescription);
                    cmd.Parameters.AddWithValue("@Id", RecyclableItem.Id);

                    // Execute the query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<RecyclableItem> GetAllRecyclableItem()
        {
            List<RecyclableItem> recyclableItems = new List<RecyclableItem>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "EXEC GetAllRecyclableItems;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RecyclableItem item = new RecyclableItem
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                TypeId = reader["Type_Id"] != DBNull.Value ? (int?)Convert.ToInt32(reader["Type_Id"]) : null,
                                Weight = reader["Weight"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["Weight"]) : null,
                                ComputedRate = reader["ComputedRate"] != DBNull.Value ? (decimal?)Convert.ToDecimal(reader["ComputedRate"]) : null,
                                ItemDescription = reader["ItemDescription"] != DBNull.Value ? reader["ItemDescription"].ToString() : null
                            };

                            recyclableItems.Add(item);
                        }
                    }
                }
            }

            return recyclableItems;
        }

        public RecyclableItem GetRecyclableItem(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "EXEC GetRecyclableItemById @Id;";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            RecyclableItem recyclableItem = new RecyclableItem
                            {
                                Id = (int)reader["Id"],
                                TypeId = reader["Type_Id"] as int?,
                                Weight = reader["Weight"] as decimal?,
                                ComputedRate = reader["ComputedRate"] as decimal?,
                                ItemDescription = reader["ItemDescription"] as string
                            };

                            return recyclableItem;
                        }
                    }
                }
            }

            return null; // If no matching item is found
        }
    }
}
