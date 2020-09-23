<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="UserAuthenticationFailed.aspx.vb" Inherits="NoticeOfIntent.UserAuthenticationFailed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <p>
        User Authentication Failed.
    </p>
    <p>
        The user did not authenticate. Please go to the home page and try to login again.
    </p>
    <div>
        <asp:Button ID="btnHome" runat="server" Text="Home" ToolTip ="Go to home page." CssClass="btn btn-default btn-lg btn-block" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
