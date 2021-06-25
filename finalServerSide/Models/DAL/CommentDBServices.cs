﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace finalServerSide.Models.DAL
{
    public class CommentDBServices
    {
        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // This method inserts a comment
        //--------------------------------------------------------------------------------------------------
        public int Insert(Comment comment)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            String cStr = BuildInsertCommand(comment);      // helper method to build the insert string

            cmd = CreateCommand(cStr, con);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //--------------------------------------------------------------------
        // Build the Insert command String
        //--------------------------------------------------------------------
        private String BuildInsertCommand(Comment comment)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values('{0}', {1}, '{2}', {3}, '{4}')", comment.CurrDate, comment.UserId, comment.UserName, comment.SeriesId, comment.Content);
            String prefix = "INSERT INTO Comments_2021 " + "([currDate], [userId],[userName],[seriesId],[content]) ";
            command = prefix + sb.ToString();
            return command;
        }

        //---------------------------------------------------------------------------------
        // Read from the DB into a list - dataReader
        //---------------------------------------------------------------------------------
        public List<Comment> GetComments(int seriesId)
        {
            SqlConnection con = null;
            List<Comment> CommentsList = new List<Comment>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM Comments_2021 WHERE seriesId = " + seriesId;
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Comment c = new Comment();
                    c.CommentId= Convert.ToInt32(dr["commentId"]);
                    c.CurrDate = (string)dr["currDate"];
                    c.UserId = Convert.ToInt32(dr["userId"]);
                    c.UserName = (string)(dr["userName"]);
                    c.SeriesId = Convert.ToInt32(dr["seriesId"]);
                    c.Content = (string)(dr["content"]);
                    CommentsList.Add(c);
                }

                return CommentsList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        public int GetMostActivUser()
        {
            SqlConnection con = null;
            Comment Comments = new Comment();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT userId FROM Comments_2021 GROUP BY userId ORDER BY count(userId) DESC";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                if (dr.Read())
                {   // Read till the end of the data into a row

                    Comments.UserId = Convert.ToInt32(dr["userId"]);
                }

                return Comments.UserId;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

    }
}