<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="SessionFailure.aspx.vb" Inherits="NoticeOfIntent.SessionFailure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <table style="width:700px;">
        <tr>
            <td class="greyBarNew" style="font-family: Arial;font-size: 12pt;font-weight: bold;text-align:center">
                Payment Trasaction Failed
            </td>
        </tr>
        <tr>
            <td style="font-weight:bold;">
                Reason: <asp:Label ID="lblTranFailMsg" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align:center">
                <asp:Button ID="btnReturn" runat="server" Text="Continue to Submission Details" CssClass="btn btn-default" />
            </td>
        </tr>
    
    </table>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
