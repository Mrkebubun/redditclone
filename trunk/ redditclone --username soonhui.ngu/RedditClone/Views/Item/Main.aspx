<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="RedditClone.Views.Item.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<form action="/Item/SubmitView" method="post">
<input type="submit" value="Add New Article" />
</form>

<%=Html.ActionLink("Login","LoginPage", "UserInfoController") %>


<ol>
    <% foreach (var item in ViewData.Model)  {  %>
    <li>
     <%Html.RenderPartial("~/Views/UserControls/ArticleUserControl.ascx", item);%>  

     </li>
    <% } %>
</ol>


</asp:Content>
