<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="RedditClone.Views.UserInfo.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
   
  <h2>Register</h2>

  

   <% if( ViewData["ErrorMessage"] != null ){ %>

   <p><% =ViewData["ErrorMessage"] %></p>
 <% } %>


  <% using(Html.Form("UserInfo","CreateUser" )){ %>

    <fieldset>

       <legend>Register</legend>

    <div><label for="userName">User Name:</label> <% =Html.TextBox( "userName" ) %></div>
      <div><label for="emailAddress">Email Address:</label> <% =Html.TextBox( "emailAddress" ) %></div>

     <div><label for="password">Password:</label> <% =Html.Password( "password" ) %></div>

       <div><% =Html.SubmitButton() %></div>

      <% =Html.Hidden( "returnUrl", "/" ) %>

     </fieldset>
    <% } %>
</asp:Content>
