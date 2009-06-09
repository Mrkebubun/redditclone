<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<RedditClone.Models.UserInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>UserInformation</h2>

    <fieldset>
        <legend>Fields</legend>
        <p>
            Reputation:
            <%= Html.Encode(Model.Reputation) %>
        </p>
        <p>
            Diggers:
            <%= Html.Encode(Model.Diggers) %>
        </p>
        <p>
            email:
            <%= Html.Encode(Model.email) %>
        </p>
    </fieldset>
    <p>

        <%=Html.ActionLink("Edit", "Edit", new { id=Model.Diggers }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

