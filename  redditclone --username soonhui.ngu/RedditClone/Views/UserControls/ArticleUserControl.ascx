    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleUserControl.ascx.cs" Inherits="RedditClone.Views.Item.ArticleUserControl" %>


<a href="<%= ViewData.Model.URL %>"><%= ViewData.Model.Title%> </a>


<form method="post" action="<%= Html.AttributeEncode(Url.Action("CastUpVote")) %>">
<input type="submit" value="<%=ViewData.Model.UpVotes%> up votes" />
</form>

<form method="post" action="<%= Html.AttributeEncode(Url.Action("CastDownVote")) %>">
<input type="submit" value="<%=ViewData.Model.DownVotes%> down votes" />
</form>
<%--<%= Html.Button<RedditClone.Controllers.ItemController>
    (s=>s.CastUpVote(ViewData.Model.id, ViewData.Model.Diggers), "upVotes",
    ViewData.Model.UpVotes+" votes") %>--%>
<%--<%=Html.Button<RedditClone.Controllers.ItemController>
(s=>s.CastDownVote(ViewData.Model.id, ViewData.Model.Diggers), "upVotes",
ViewData.Model.DownVotes+" votes") %>--%>
