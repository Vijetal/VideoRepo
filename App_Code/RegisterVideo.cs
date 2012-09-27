using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for RegisterVideo
/// </summary>
public class RegisterVideo : Video
{

    static public object ExecuteCmd(string Querystring)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoConnectionString"].ToString());
        SqlCommand com = new SqlCommand(Querystring, con);
        try
        {
            con.Open();
            return com.ExecuteScalar();
        }
        finally
        {
            con.Close();
        }
    }

    static public SqlDataReader ExecuteReader(string Querystring)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoConnectionString"].ToString());
        SqlCommand com = new SqlCommand(Querystring, con);
        try
        {
            con.Open();
            return com.ExecuteReader();
        }
        finally
        {

        }
    }

    public void InsertIntoVideos()
    {

        string query = string.Format("Insert into Videos(VideoName, Image, DateUploaded, UserName) values ('{0}','{1}','{2}','{3}');", VideoName.Replace("'", "''"), Image, DateUploaded, UserName);
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoConnectionString"].ToString());
        SqlCommand com = new SqlCommand(query, con);
        try
        {
            con.Open();
            Convert.ToInt32(com.ExecuteScalar());
        }
        finally
        {
            con.Close();
        }
    }

    public void DeleteVideo()
    {
        string query = string.Format("Delete from Videos where PKVideoID ='{0}'", PKVideoID);
        ExecuteCmd(query);
    }

    public int CheckVideo()
    {
        int PID = 0;
        SqlDataReader reader;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoConnectionString"].ToString());
        string query = string.Format("Select count(VideoName) as VideoName from Videos where VideoName = '{0}' and FileSize = '{1}'", VideoName, FileSize);
        try
        {
            SqlCommand com = new SqlCommand(query, con);
            con.Open();
            reader = com.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                PID = Convert.ToInt32(reader["VideoName"]);
            }
        }
        finally
        {
            con.Close();
        }
        return PID;
    }

    public void GetVideoFromID()
    {
        string query = string.Format("Select VideoName,Image from Videos where PKVideoID = '{0}'", PKVideoID);
        SqlDataReader reader = ExecuteReader(query);
        reader.Read();
        VideoName = reader["VideoName"].ToString();
        Image = (byte[])(reader["Image"]);
    }

    public object Extension
    {
        get
        {
            string ext;
            ext = VideoName.Substring((VideoName.Length - (VideoName.Length - (VideoName.LastIndexOf(".") + 1))));
            ext = ext.Trim();
            switch (ext.ToUpper())
            {
                case "XLS" :
                    return "application/msexcel";
                    break;
                case "MOV" :
                    return "video/quicktime";
                    break;
                case "MP4" :
                    return "video/mp4";
                    break;
                case "WMV":
                    return "video/wmv";
                    break;
                case "PNG" :
                    return "image/png";
                    break;
                case  "M2V":
                    return "video/m2v";
                    break;
                default :
                    return "video/mp4";
                    break;
            }
        }
    }
}