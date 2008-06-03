<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Submit.aspx.cs" Inherits="RedditClone.Views.Item.Submit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h2>Submit</h2>

<form action="/Item/SubmitNew" method="post">
<table>
    <tr>
        <td>Url</td>
        <td><input id="URL" type="text" name="URL" /></td>
    </tr>
    
     <tr>
        <td>Title</td>
        <td><input id="Title" type="text" name="Title" /></td>
    </tr>
    
       <tr>
        <td>Name</td>
        <td><input id="Diggers" type="text" name="Diggers" /></td>
    </tr>
</table>

<p></p>

<input type="submit" value="Save" />

</form>

</asp:Content>
