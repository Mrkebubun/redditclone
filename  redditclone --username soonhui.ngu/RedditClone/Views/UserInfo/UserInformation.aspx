<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="UserInformation.aspx.cs" Inherits="RedditClone.Views.UserInfo.AboutTheUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<h2> About the user</h2>
<<table border="0" cellspacing="0" cellpadding="0" width="100%" >
	<tr>
		<td>Name:</td>
		<td> <%=ViewData.Model.Diggers %> </td>
	</tr>
	<tr>
	<td>Email:</td>
	<td><% = ViewData.Model.email %></td>
	</tr>
</table>
</asp:Content>
