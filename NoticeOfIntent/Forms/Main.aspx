<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="Main.aspx.vb" Inherits="NoticeOfIntent.Main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">

    <div class="row">
        <div class="col-md-12">
            <label class="control-label">User Name:</label>
            <asp:Label ID="lblUser" runat="server" Text="" CssClass="form-control-static"></asp:Label>
        </div>
    </div>
    <div class="row">
        <p class="col-md-12">
            Welcome to the main page of Notice Of Intent application. Here you could fill out application for new NOI, CoPermittee and Termination, view previous submitted NOIs and even delete incomplete submission.<br /><br />
        </p>
    </div>
    <div class="row">
        <div class="col-md-12">      
            <p>NOI Submissions for <asp:Label ID="lblheadertext" runat="server" Text="Storm Water Discharges Associated with CONSTRUCTION ACTIVITY under a NPDES General Permit"></asp:Label></p>
            <asp:ListView ID="lvSubmissionByUser" runat="server" DataKeyNames="ReferenceNo,SubmissionTypeID,ProgSubmissionTypeID" ItemType="NoticeOfIntent.NOISubmissionSearchlst" SelectMethod="GetAllSubmissionByUserSearch" DeleteMethod="lvSubmissionByUser_DeleteItem">
                <LayoutTemplate>
                    <table class="table table-bordered table-hover table-condensed">
                        <thead>
                            <tr>
                                <th >Reference No</th>
                                <th >Project Name</th>
                                <th >Received Date</th>
                                <th >Permit Number</th>
                                <th >Owner</th>
                                <th >Submission Status</th>
                                <th >IsSigned</th>
                                <th >Submission Type</th>
                                <th  class="col-md-2">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr runat="server" id="itemPlaceholder" />
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="9">
                                    <asp:DataPager ID="dpSubmissionByUser" runat="server" PageSize="5" PagedControlID="lvSubmissionByUser">
                                        <Fields>
                                            <asp:NumericPagerField ButtonType="Button" ButtonCount="5"   />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="panel-title font-bold">Add a new submission</div>
                                        </div>
                                        <div class="panel-body text-center">
                                            <div class="form-inline">
                                                <div class="form-group">
                                                    <label class="control-label">Select the Submission Type:</label>
                                                    <asp:DropDownList ID="ddlSubmissionType" CssClass="form-control" runat="server" ItemType="NoticeOfIntent.NOIProgSubmissionType" SelectMethod="GetSubmissionTypeList" DataTextField="SubmissionTypeDesc" DataValueField="ProgSubmissionTypeID">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnAddNewSubmission" CssClass="form-control" runat="server" CommandName="add" Text="Add New Submission" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#: Item.ReferenceNo%></td>
                        <td><%#: Item.ProjectName%></td>
                        <td><%#: Item.ReceivedDate.ToShortDateString()%></td>
                        <td><%#: Item.PermitNumber%></td>
                        <td><%#: Item.Owners%></td>
                        <td><%#: Item.SubmissionStatus%></td>
                        <td><asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked='<%#: Item.IsSigned%>' /></td>
                        <td><%#: Item.SubmissionType%></td>
                        <td>
                            <div class="form-inline">
                                <asp:LinkButton ID="lbtnView" runat="server" CssClass="form-control" CommandName="select" ToolTip="View Submission" CommandArgument="<%#: Item.ReferenceNo%>" >V</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSubmissionDetails" CssClass="form-control" runat="server" CommandName="select1" ToolTip="View Submission Overview" CommandArgument="<%#: Item.ReferenceNo%>">D</asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" CssClass="form-control" runat="server" OnClientClick="javascript:return confirm('Are you sure to delete?')" CommandName="Delete" ToolTip="Delete Submission" CommandArgument="<%#: Item.ReferenceNo%>">X</asp:LinkButton>
                            </div>
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table class="table table-bordered table-hover table-condensed">
                        <thead>
                            <tr>
                                <th >Reference No</th>
                                <th >Project Name</th>
                                <th >Received Date</th>
                                <th >Permit Number</th>
                                <th >Owner</th>
                                <th >Submission Status</th>
                                <th >IsSigned</th>
                                <th >Submission Type</th>
                                <th  class="col-md-2">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr runat="server" id="itemPlaceholder" />
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="9">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="panel-title font-bold">Add a new submission</div>
                                        </div>
                                        <div class="panel-body text-center">
                                            <div class="form-inline">
                                                <div class="form-group">
                                                    <label class="control-label">Select the Submission Type:</label>
                                                    <asp:DropDownList ID="ddlSubmissionType" CssClass="form-control" runat="server" ItemType="NoticeOfIntent.NOIProgSubmissionType" SelectMethod="GetSubmissionTypeList" DataTextField="SubmissionTypeDesc" DataValueField="ProgSubmissionTypeID">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnAddNewSubmission" CssClass="form-control" runat="server" CommandName="add1" Text="Add New Submission" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </EmptyDataTemplate>

            </asp:ListView>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
