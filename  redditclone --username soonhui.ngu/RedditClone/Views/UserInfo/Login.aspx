<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RedditClone.Views.UserInfo.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h2>Login</h2>
<% if( ViewData["ErrorMessage"] != null ){ %>
    <p><% =ViewData["ErrorMessage"] %></p>
<% } %>

   

      <% using (Html.Form("UserInfo", "Login"))
         { %>

     <fieldset>

         <legend>Login</legend>

        <div><label for="userName">User Name:</label> <% =Html.TextBox("userName")%></div>

        <div><label for="password">Password:</label> <% =Html.Password("password")%></div>

          <div><label for="rememberMe">Remember Me:</label>
           <input type="checkbox" id="rememberMe" name="rememberMe" checked="checked" value="Remember Me" /></div>

       <div><% =Html.SubmitButton()%></div>

        <% =Html.Hidden("returnUrl", "/")%>

  </fieldset>
  <%} %>

<%--<form action="/UserInfo/AddUser" method="post">
<table>
    <tr>
        <td>User Name:</td>
        <td><input id="username" type="text" name="username" /></td>
    </tr>
    
     <tr>
        <td>Password:</td>
        <td><input id="password" type="password" name="password"/></td>

    </tr>
    

</table>

<p></p>

<input type="submit" value="add" />

</form>--%>
</asp:Content>
