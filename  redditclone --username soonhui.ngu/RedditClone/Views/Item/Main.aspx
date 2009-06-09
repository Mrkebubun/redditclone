<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<List<Article>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<form action="<%=Html.AttributeEncode(Url.Action("SubmitNew"))%>" method="get">
<input type="submit" value="Add New Article" />
</form>


<ol>
    <% foreach (var item in ViewData.Model)  {  %>
    <li>
     <%Html.RenderPartial("~/Views/UserControls/ArticleUserControl.ascx", item);%>  

     </li>
    <% } %>
</ol>


</asp:Content>
