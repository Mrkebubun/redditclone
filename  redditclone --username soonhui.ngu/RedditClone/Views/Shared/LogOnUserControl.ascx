<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Welcome <b><%=Html.ActionLink(Html.Encode(Page.User.Identity.Name), "UserInformation", "Account",
                                                                     new  { username = Page.User.Identity.Name }, null)%> </b>!
        [ <%= Html.ActionLink("Log Off", "Logout", "Account")%> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Log On", "Login", "Account") %> ]
<%
    }
%>
