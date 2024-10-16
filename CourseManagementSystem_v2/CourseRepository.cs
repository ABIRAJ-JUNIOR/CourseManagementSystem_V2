﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementSystem_v2
{
    internal class CourseRepository
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=CourseManagement;Trusted_Connection=true;TrustServerCertificate=true;";

        public void Insert(string id, string title, string duration, decimal price)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"INSERT INTO Courses(CourseId,Title,Duration,Price)VALUES(@courseId,@title,@duration,@price)";
                    command.Parameters.AddWithValue("@courseId", id);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@duration", duration);
                    command.Parameters.AddWithValue("@price", price);
                    command.ExecuteNonQuery();

                    Console.WriteLine($"\nCourse added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
            }
        }

        public void Update(string id, string title, string duration, decimal price)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Courses SET Title=@title,Duration=@duration,Price=@price WHERE CourseId=@courseId;";
                    command.Parameters.AddWithValue("@courseId", id);
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@duration", duration);
                    command.Parameters.AddWithValue("@price", price);
                     var roweffected = command.ExecuteNonQuery();
                    if(roweffected > 0)
                    {
                        Console.WriteLine($"\nCourse Update successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"\nCourse Not Found.");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
            }
        }

        public void Delete(string id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"DELETE FROM Courses WHERE CourseId=@courseId;";
                    command.Parameters.AddWithValue("@courseId", id);
                    var roweffected = command.ExecuteNonQuery();
                    if (roweffected > 0)
                    {
                        Console.WriteLine($"\nCourse Deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"\nCourse Not Found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
            }
        }

        public void GetAllCourses()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM Courses";
                    using(var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("--- List of Courses ---\n");
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader.GetString(0)}, Title: {reader.GetString(1)}, Duration: {reader.GetString(2)}, Price: {reader.GetDecimal(3)}");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
            }
        }

        public void GetCourseById(string id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"SELECT * FROM Courses WHERE CourseId=@courseId;";
                    command.Parameters.AddWithValue("@courseId", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            Console.WriteLine($"ID: {reader.GetString(0)}, Title: {reader.GetString(1)}, Duration: {reader.GetString(2)}, Price: {reader.GetDecimal(3)}");
                        }
                        else
                        {
                            Console.WriteLine($"\nCourse Not Found..");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR : {ex.Message}");
            }
        }

        public string CapitalizeTitle(string title)
        {
            var words = title.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
            return string.Join(" ",words);
        }
    }
}
