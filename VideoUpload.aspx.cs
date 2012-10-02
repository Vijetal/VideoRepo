using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class VideoUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         string uname;
        uname = User.Identity.Name;
        uname = uname.ToLower();
        uname = uname.Remove(0, 10);
        Session["UserName"] = uname;
        if (Request.QueryString["VideoID"] == null)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        else
        {
            //btnsave.Visible = false;
            //fuVideo.Visible = false;
            //gvVideos.Visible = false;
            PlayVideo();
        }
    }

    public void PlayVideo()
    {
        string ID = Request.QueryString["VideoID"].ToString();
        RegisterVideo rv = new RegisterVideo();
        rv.PKVideoID = Convert.ToInt32(ID);
        rv.GetVideoFromID();
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", ("inline;filename=" + rv.VideoName));
        Response.ContentType = rv.Extension.ToString();
        Response.BinaryWrite(rv.Image);
        Response.Flush();
        Response.End();
    }

    public void BindGrid()
    {
        string connectionstring = ConfigurationManager.ConnectionStrings["VideoConnectionString"].ToString();
        string query = "Select * from Videos order by DateUploaded DESC";
        SqlConnection con = new SqlConnection(connectionstring);
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataSet ds = new DataSet();
        da.Fill(ds);
        gvVideos.DataSource = ds;
        gvVideos.DataBind();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["VideoConnectionString"].ToString());
        try
        {
            System.Web.HttpPostedFile InputFile = fuVideo.PostedFile;
            int FileLength = InputFile.ContentLength;
            System.IO.Stream StreamObject = InputFile.InputStream;
            byte[] FileByteArray = new byte[FileLength];
            StreamObject.Read(FileByteArray, 0, FileLength);
            string VideoName = fuVideo.FileName.ToString();
               string ext;
                ext = VideoName.Substring((VideoName.Length - (VideoName.Length - (VideoName.LastIndexOf(".") + 1))));
                ext = ext.Trim();
                ext = ext.ToUpper();
                if (ext == "XLS" || ext == "MOV" || ext == "MP4" || ext == "WMV" || ext == "M2V" || ext == "M4V" || ext == "OGG" || ext == "WEBM" || ext == "AVI" || ext == "MPG" || ext == "MPEG")
                {
                    RegisterVideo rv = new RegisterVideo();
                    rv.VideoName = fuVideo.FileName.ToString();


                    rv.Image = FileByteArray;
                    rv.DateUploaded = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    rv.UserName = Session["UserName"].ToString();
                    // rv.InsertIntoVideos();

                    string FileSize = GetFileSize(FileLength);
                    rv.FileSize = FileSize;
                    int cnt = rv.CheckVideo();

                    if (cnt == 0)
                    {
                        SqlCommand com = new SqlCommand();
                        com.CommandText = "spInsertIntoVideos";
                        com.CommandType = CommandType.StoredProcedure;
                        com.Connection = con;
                        con.Open();

                        com.Parameters.Add(new SqlParameter("@VideoName", rv.VideoName));
                        com.Parameters.Add(new SqlParameter("@Image", rv.Image));
                        com.Parameters.Add(new SqlParameter("@DateUploaded", rv.DateUploaded));
                        com.Parameters.Add(new SqlParameter("@UserName", rv.UserName));
                        com.Parameters.Add(new SqlParameter("@FileSize", FileSize));
                        com.ExecuteNonQuery();

                        lblError.Text = "Video Uploaded Successfully";
                    }
                    else
                    {
                        lblError.Text = "File already exists";
                    }
                    BindGrid();
                }
                else
                {
                    lblError.Text = "File format not supported";
                }
        }
        catch (System.Exception ex)
        {
            lblError.Text = ex.Message;
        }
        finally
        {
            con.Close();
        }
    }

    public string GetFileSize(int filelength)
    {
        const int byteConversion = 1024;
        double bytes = Convert.ToDouble(filelength);

        if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
        {
            return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " GB");
        }
        else if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
        {
            return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " MB");
        }
        else if (bytes >= byteConversion) //KB Range
        {
            return string.Concat(Math.Round(bytes / byteConversion, 2), " KB");
        }
        else //Bytes
        {
            return string.Concat(bytes, " Bytes");
        }
    }



    protected void gvVideos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVideos.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void gvVideos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int id = Int32.Parse(gvVideos.DataKeys[e.RowIndex].Value.ToString());
            RegisterVideo rv = new RegisterVideo();
            rv.PKVideoID = id;
            rv.DeleteVideo();

            BindGrid();
        }
        catch (System.Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}