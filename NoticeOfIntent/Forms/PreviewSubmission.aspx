<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="PreviewSubmission.aspx.vb" Inherits="NoticeOfIntent.PreviewSubmission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title font-bold">Review Submission</h1>
        </div>
        <div class="panel-body input-group-sm">
            <iframe id="iframeDoc" runat="server" style="height:600px; width: 100%" >
            </iframe>
        </div>
        <div class="panel-footer">
            <div class="pull-right">
                <asp:Button ID="btnBack" runat="server"  CssClass="btn btn-default"  Text="Back" />
                <asp:Button ID="btnContinue" runat="server"  CssClass="btn btn-default" Text="Continue" />
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
