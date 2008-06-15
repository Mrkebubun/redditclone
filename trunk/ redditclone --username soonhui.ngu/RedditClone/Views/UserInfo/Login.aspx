<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RedditClone.Views.UserInfo.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<form action="/UserInfo/AddUser" method="post">
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

</form>
</asp:Content>
