<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="RedditClone.Views.UserInfo.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   
  <h2>Register</h2>

  

   <% if( ViewData["ErrorMessage"] != null ){ %>

   <p><% =ViewData["ErrorMessage"] %></p>
 <% } %>

<form method="post" action= "<%=Html.AttributeEncode(Url.Action("Register")) %>">
        <div>
            <table>
                <tr>
                    <td>Username:</td>
                    <td><%= Html.TextBox("userName")%></td>
                </tr>
                <tr>
                    <td>Password:</td>
                    <td><%= Html.Password("password")%></td>
                </tr>
                 <tr>
                    <td>email:</td>
                    <td><%= Html.TextBox("email")%></td>
                </tr>
                <tr>
                    <td></td>
                    <td><input type="submit" value="Login" /></td>
                </tr>
            </table>
        </div>
</form>

</asp:Content>
