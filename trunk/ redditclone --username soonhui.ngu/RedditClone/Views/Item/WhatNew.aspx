<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="WhatNew.aspx.cs" Inherits="RedditClone.Views.Item.WhatNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
<ol>
    <% foreach (var item in ViewData)  {  %>
    <li>
        <%= Html.RenderUserControl("~/Views/UserControls/ArticleUserControl.ascx", item) %>  
     </li>
    <% } %>
</ol>
</asp:Content>
