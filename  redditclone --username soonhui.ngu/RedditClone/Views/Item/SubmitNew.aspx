<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h2>Submit</h2>

      <form method="post" action="<%= Html.AttributeEncode(Url.Action("SubmitNew")) %>">
      <table>
    <tr>
        <td>Url</td>
        <td><%=Html.TextBox("url") %></td>
    </tr>
    
     <tr>
        <td>Title</td>
        <td><%=Html.TextBox("Title") %></td>
    </tr>
    
       <tr>
        <td>Name</td>
        <td><%=Html.TextBox("digger")%></td>
    </tr>
    <tr>
     <td><input type="submit" value="Submit" /></td>
    </tr>
</table>
      </form>

</asp:Content>
