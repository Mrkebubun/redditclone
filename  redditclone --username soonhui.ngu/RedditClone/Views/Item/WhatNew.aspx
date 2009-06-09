<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<ol>
    <% foreach (var item in ViewData)  {  %>
    <li>
        <%Html.RenderPartial("~/Views/UserControls/ArticleUserControl.ascx", item); %>  
     </li>
    <% } %>
</ol>
</asp:Content>
