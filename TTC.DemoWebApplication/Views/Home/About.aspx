<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TTC.Tools.ITypeInstanceDictionary>" %>

<asp:Content ID="siteMasterInitializationContent" ContentPlaceHolderID="siteMasterInitializationContentPlaceHolder" runat="server">
   <% Model.Get<IMasterPageViewData>().BodyId = "about"; %>
</asp:Content>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <p>
        Put content here.
    </p>
</asp:Content>
