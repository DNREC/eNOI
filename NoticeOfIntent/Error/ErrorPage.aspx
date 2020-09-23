<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralNested.Master" CodeBehind="ErrorPage.aspx.vb" Inherits="NoticeOfIntent.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
        <div class="jumbotron">
        <h2>Oops! Error occured</h2>
        <!--<p>Agree and then click on the Submit button to send the application for approval. You will not be able to make any changes to the application after submit.</p>-->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
     <p>
        Sorry for the inconvenience occured. 
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </p>
    <div>
        <asp:Button ID="btnHome" runat="server" Text="Home" ToolTip ="Go to home page." CssClass="btn btn-default btn-lg" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
