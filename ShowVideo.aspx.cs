using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShowVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
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

        //tryint git.....
    }
}