using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;
using System.Web.Http;
using finalServerSide.Models.DAL;


namespace finalServerSide.Models.DAL
{
    public class UserLikeCommDBServices
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
        public int Insert(UserLikesComment ulc)
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

            String cStr = BuildInsertCommand(ulc);      // helper method to build the insert string

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
        private String BuildInsertCommand(UserLikesComment ulc)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat("Values({0}, {1}, {2},{3},{4})", ulc.CommentId, ulc.UserId, ulc.SeriesId, ulc.Like, ulc.Dislike);
            String prefix = "INSERT INTO UserLikeComments_2021 " + "([commentId], [userId],[seriesId], [like], [dislike]) ";
            command = prefix + sb.ToString();
            return command;
        }


        public int Update(UserLikesComment ulc)
        {

            SqlConnection con;
            SqlCommand cmd;
            {
                try
                {
                    con = connect("DBConnectionString"); // create the connection
                }
                catch (Exception ex)
                {
                    // write to log
                    throw (ex);
                }

                //String cStr = BuildUpdateCommand(ulc);
                String cStr = "";
                if (ulc.Dislike) //use this way to relise what to update, value in Like
                {
                    cStr = BuildUpdateLikesCommand(ulc);      // helper method to build the insert string
                } 
                else 
                {
                    cStr = BuildUpdateDislikesCommand(ulc);
                }

                cmd = CreateCommand(cStr, con);             // create the command

                try
                {
                    int rowEffected = cmd.ExecuteNonQuery(); // execute the command
                    return rowEffected;
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

        }

        //--------------------------------------------------------------------
        // Build the Update command String
        //--------------------------------------------------------------------

        private String BuildUpdateLikesCommand(UserLikesComment ulc)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" SET [CommentId]={0}, [UserId]={1}, [SeriesId]={2}, [Like]='{3}', [Dislike]=[Dislike]", ulc.CommentId, ulc.UserId, ulc.SeriesId, ulc.Like);
            String prefix = "UPDATE userLikeComments_2021" + " ";
            String end = " WHERE CommentId= " + ulc.CommentId + " and UserId =" + ulc.UserId + " and SeriesId= " + ulc.SeriesId;
            command = prefix + sb.ToString() + end;
            return command;
        }

        private String BuildUpdateDislikesCommand(UserLikesComment ulc)
        {
            String command;

            StringBuilder sb = new StringBuilder();
            // use a string builder to create the dynamic string
            sb.AppendFormat(" SET [CommentId]={0}, [UserId]={1}, [SeriesId]={2}, [Like]=[Like], [Dislike]='{3}'", ulc.CommentId, ulc.UserId, ulc.SeriesId, ulc.Like);
            String prefix = "UPDATE userLikeComments_2021" + " ";
            String end = " WHERE CommentId= " + ulc.CommentId + " and UserId =" + ulc.UserId + " and SeriesId= " + ulc.SeriesId;
            command = prefix + sb.ToString() + end;
            return command;
        }

        public List<UserLikesComment> Get()
        {
            SqlConnection con = null;
            List<UserLikesComment> userCommentsList = new List<UserLikesComment>();

            try
            {
                con = connect("DBConnectionString"); // create a connection to the database using the connection String defined in the web config file

                String selectSTR = "SELECT * FROM UserLikeComments_2021";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    UserLikesComment c = new UserLikesComment();
                    c.CommentId = Convert.ToInt32(dr["commentId"]);
                    c.UserId = Convert.ToInt32(dr["userId"]);
                    c.SeriesId = Convert.ToInt32(dr["seriesId"]);
                    c.Like = Convert.ToBoolean(dr["like"]);
                    c.Dislike = Convert.ToBoolean(dr["dislike"]);
                    userCommentsList.Add(c);
                }

                return userCommentsList;
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