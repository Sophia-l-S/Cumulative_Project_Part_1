﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogProject.Models;
using MySql.Data.MySqlClient;

namespace BlogProject.Controllers
{
    public class AuthorDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private BlogDbContext Blog = new BlogDbContext();

        //This Controller Will access the authors table of our blog database.
        /// <summary>
        /// Returns a list of Authors in the system
        /// </summary>
        /// <example>GET api/AuthorData/ListAuthors</example>
        /// <returns>
        /// A list of Authors (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Author> ListAuthors()
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Authors";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Author Names
            List<Author> Authors = new List<Author> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int AuthorId = (int)ResultSet["AuthorId"];
                string AuthorFname = (string)ResultSet["AuthorFname"];
                string AuthorLname = (string)ResultSet["AuthorLname"];
                string AuthorBio = (string)ResultSet["AuthorBio"];
               

                Author NewAuthor = new Author();
                NewAuthor.AuthorId = AuthorId;
                NewAuthor.AuthorFname = AuthorFname;
                NewAuthor.AuthorLname = AuthorLname;
                NewAuthor.AuthorBio = AuthorBio;


                //Add the Author Name to the List
                Authors.Add(NewAuthor);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Author names
            return Authors;
        }

        public Author FindAuthor(int id)
        {
            Author NewAuthor = new Author();

            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Authors where AuthorId = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int AuthorId = (int)ResultSet["AuthorId"];
                string AuthorFname = (string)ResultSet["AuthorFname"];
                string AuthorLName = (string)ResultSet["AuthorLname"];
                string AuthorBio = (string)ResultSet["AuthorBio"];


                NewAuthor.AuthorId = AuthorId;
                NewAuthor.AuthorFname = AuthorFname;
                NewAuthor.AuthorLname = AuthorLName;
                NewAuthor.AuthorBio = AuthorBio;

            }
            return NewAuthor;
        }
  
    }
}
