<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="RedditClone.Views.Item.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<form action="/Item/SubmitView" method="post">
<input type="submit" value="Add New" />
</form>

<ol>
    <% foreach (var item in ViewData)  {  %>
    <li>
    <a href="<%= item.URL %>"><%= item.Title %></a>
     </li>
    <% } %>
</ol>
</asp:Content>
