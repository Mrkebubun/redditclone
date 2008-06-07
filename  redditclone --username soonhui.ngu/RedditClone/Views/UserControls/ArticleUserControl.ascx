<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleUserControl.ascx.cs" Inherits="RedditClone.Views.Item.ArticleUserControl" %>


<a href="<%= ViewData.URL %>"><%= ViewData.Title%> </a>
<%=Html.Button<RedditClone.Controllers.ItemController>
    (s=>s.CastUpVote(ViewData.id, ViewData.Diggers), "upVotes",
    ViewData.UpVotes+" votes") %>
<%=Html.Button<RedditClone.Controllers.ItemController>
(s=>s.CastDownVote(ViewData.id, ViewData.Diggers), "upVotes",
ViewData.DownVotes+" votes") %>
