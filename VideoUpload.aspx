<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoUpload.aspx.cs" Inherits="VideoUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="fuVideo" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" onclick="btnsave_Click" 
                        Text="Save File" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvVideos" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="PKVideoID" AllowPaging="True" 
                        onpageindexchanging="gvVideos_PageIndexChanging" 
                        onrowdeleting="gvVideos_RowDeleting" >
                        <Columns>
                            <asp:BoundField DataField="PKVideoID" HeaderText="PKVideoID" Visible="False" />
                            <asp:BoundField DataField="VideoName" HeaderText="VideoName" />
                            <asp:TemplateField HeaderText= "Name">
                                <ItemTemplate>
                                   <%-- <asp:HyperLink ID="hlnkVideo" runat = "server" NavigateUrl='<%# Eval("PKVideoID", "~/ShowVideo.aspx?VideoID={0}") %>' Text = '<%# Eval("VideoName") %>' Target = "_blank" >
                                    </asp:HyperLink>--%>
                                   <%--  <asp:HyperLink ID="hlnkVideo" runat = "server" NavigateUrl='<%# Eval("PKVideoID", "~/VideoUpload.aspx?VideoID={0}") %>' Text = '<%# Eval("VideoName") %>' Target = "_blank" >--%>
                                     <asp:HyperLink ID="hlnkVideo" runat = "server" NavigateUrl='<%# Eval("PKVideoID", "?VideoID={0}") %>' Text = '<%# Eval("VideoName") %>' Target = "_blank" >
                                    </asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="FileSize" HeaderText="File Size" />
                            <asp:BoundField DataField="DateUploaded" HeaderText="Date Uploaded" />
                            <asp:BoundField DataField="UserName" HeaderText="User Name" />
                           <asp:CommandField ShowDeleteButton="True" ItemStyle-HorizontalAlign="Center" CausesValidation ="false" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
