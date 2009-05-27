<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
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
