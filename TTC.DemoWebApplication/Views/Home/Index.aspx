<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TTC.Tools.ITypeInstanceDictionary>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <% Model.Verify<IIndexViewData>(); %>
    <h2><%= Model.Get<IIndexViewData>().Message %></h2>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">http://asp.net/mvc</a>.
    </p>
    <p>
      <%= Model.Get<IIndexViewData>().Message2 %>     
    </p>
    <p>
      <%= Model.Get<IIndexViewData>().Message3 %>
    </p>
</asp:Content>
