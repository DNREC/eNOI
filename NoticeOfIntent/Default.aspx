<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralNested.master" CodeBehind="Default.aspx.vb" Inherits="NoticeOfIntent._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <h1 class="text-center">Electronic Notice of Intent (eNOI)</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <p>
        DNREC has created the eNOI for construction sites, industrial facilities, pesticides and small MS4 to apply for coverage under DNREC's
    </p>
    <asp:Repeater ID="Repeater1" runat="server" ItemType="NoticeOfIntent.NOIProgram" SelectMethod="Repeater1_GetData">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <%--<a href="Home.aspx?ReportID=<%#: Item.ProgramID %>"  ><%#: Item.ProgramDesc %></a>--%>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("~/Home.aspx?ReportID={0}", Item.ProgramID) %>'><%#: Item.ProgramDesc %></asp:HyperLink>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>

    <p>
        The eNOI system is an online service to submit and manage NOI application. Registration is quick and easy.
    </p>
    <p>
        Please click the respective link to access DNREC eNOI system to begin the process of preparing and submitting eNOIs and other reports.
    </p>
    <label class="control-label" for="txtSCSToken">SCS Token:</label>
    <asp:TextBox ID="txtSCSToken" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5"></asp:TextBox>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
