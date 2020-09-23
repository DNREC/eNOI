<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="ManageCoupons.aspx.vb" Inherits="NoticeOfIntent.ManageCoupons" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <div class="jumbotron">
        <h1 class="center">Manage Fee Exemption</h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title">List of exemption codes in eNOI Database</h1>
        </div>
        <div class="panel-body input-group-sm">
            <asp:ListView ID="lvExemptions" runat="server" DataKeyNames="ExemptionID" ItemType="NoticeOfIntent.NOIFeeExemption" SelectMethod="lvExemptions_GetData" InsertItemPosition="LastItem" InsertMethod="lvExemptions_InsertItem" DeleteMethod="lvExemptions_DeleteItem">
                    <LayoutTemplate>
                        <table class=" table table-bordered table-hover table-condensed">
                            <thead>
                                <tr>
                                    <th >Exemption Code</th>
                                    <th >Active From</th>
                                    <th >Expires On</th>
                                    <th >Used for Reference No</th>
                                    <th >Created By</th>
                                    <th  class="col-md-2">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr runat="server" id="itemPlaceholder" />
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="6">
                                        <asp:DataPager ID="dpExemptions" runat="server" PageSize="5" PagedControlID="lvExemptions">
                                            <Fields>
                                                <asp:NumericPagerField ButtonType="Button" ButtonCount="5"   />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#: Item.ExemptionCode%></td>
                            <td><%#: Item.ActiveFrom.ToString("MM/dd/yyyy")%></td>
                            <td><%#: Item.ExpiresOn.ToString("MM/dd/yyyy")%></td>
                            <td><%#: Item.SubmissionID%></td>
                            <td><%#: Item.CreatedBy%></td>
                            <td>
                                <div class="form-inline">
                                    <asp:LinkButton ID="lbtnDelete" CssClass="form-control" runat="server" OnClientClick="javascript:return confirm('Are you sure to delete?')" CommandName="Delete" CommandArgument="<%#: Item.ExemptionID%>">Delete</asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <InsertItemTemplate>
                         <tr>
                             <td colspan="6">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h1 class="panel-title">Generate a Exemption Code</h1>
                                    </div>
                                    <div class="panel-body input-group-sm">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:Label ID="Label1" AssociatedControlID="txtActiveFrom" runat="server" Text="Start Date:" CssClass="control-label col-md-2"></asp:Label>
                                                <div class="col-md-10">
                                                    <asp:TextBox ID="txtActiveFrom" runat="server" CssClass="form-control datepicker" Text="<%# BindItem.ActiveFrom%>"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label2" AssociatedControlID="txtExpiresOn" runat="server" Text="End Date:" CssClass="control-label col-md-2"></asp:Label>
                                                <div class="col-md-10">
                                                    <asp:TextBox ID="txtExpiresOn" runat="server" CssClass="form-control datepicker" Text="<%# BindItem.ExpiresOn%>"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-offset-2 col-md-10">
                                                    <asp:Button ID="btnGECode" runat="server" CommandName="Insert" Text="Generate Exemption Code" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                             </td>
                         </tr>
                    </InsertItemTemplate>
                    <EmptyDataTemplate>
                        <table class=" table table-bordered table-hover table-condensed">
                            <thead>
                                <tr>
                                    <th >Exemption Code</th>
                                    <th >Active From</th>
                                    <th >Expires On</th>
                                    <th >Used for Reference No</th>
                                    <th >Created By</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td colspan="5">No ExemptionCode available.</td>
                                </tr>
                            </tbody>
                    </EmptyDataTemplate>
                </asp:ListView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
