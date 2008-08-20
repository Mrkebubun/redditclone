    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleUserControl.ascx.cs" Inherits="RedditClone.Views.Item.ArticleUserControl" %>


<a href="<%= ViewData.Model.URL %>"><%= ViewData.Model.Title%> </a>
<%=Html.Button<RedditClone.Controllers.ItemController>
    (s=>s.CastUpVote(ViewData.Model.id, ViewData.Model.Diggers), "upVotes",
    ViewData.Model.UpVotes+" votes") %>
<%=Html.Button<RedditClone.Controllers.ItemController>
(s=>s.CastDownVote(ViewData.Model.id, ViewData.Model.Diggers), "upVotes",
ViewData.Model.DownVotes+" votes") %>
