<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="Submissions.aspx.vb" Inherits="NoticeOfIntent.Submissions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <div class="panel-body">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h1 class="panel-title font-bold">Search Criteria</h1>
            </div>
            <div class="panel-body">
                <asp:HiddenField ID="hffiltercheat" runat="server" Value="1" />
                <table class="table table-bordered table-hover table-condensed">
                    <thead>
                        <tr>
                            <th>Reference No</th>
                            <th>Project Name</th>
                            <th>Permit Number</th>
                            <th>Owner</th>
                            <th>Submission Type</th>
                            <th>Submission Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtReferenceNo" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectName" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPermitNumber" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOwner" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubmissionType1" runat="server" CssClass="form-control" ItemType="NoticeOfIntent.NOIProgSubmissionType" SelectMethod="GetSubmissionTypeList" DataTextField="SubmissionTypeDesc" DataValueField="ProgSubmissionTypeID" AppendDataBoundItems="true">
                                    <asp:ListItem Text="--Select a Submission Type--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubmissionStatusCode" runat="server" CssClass="form-control" ItemType="NoticeOfIntent.NOISubmissionStatusCode" SelectMethod="GetSubmissionStatusCodeList" DataTextField="SubmissionStatus" DataValueField="SubmissionStatusCode" AppendDataBoundItems="true">
                                    <asp:ListItem Text="All" Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="6">
                                <asp:Button ID="btnSearch" runat="server" Text="Search / Reset" CssClass="btn btn-default" />
                            </td>
                        </tr>
                    
                    </tfoot>
                </table>
            </div>
        </div>
        <asp:ListView ID="lvSubmissions" runat="server" DataKeyNames="ReferenceNo,SubmissionTypeID,ProgSubmissionTypeID" ItemType="NoticeOfIntent.NOISubmissionSearchlst" SelectMethod="lvSubmissions_GetData" DeleteMethod="lvSubmissions_DeleteItem">
            <LayoutTemplate>
                <table class=" table table-bordered table-hover table-condensed">
                    <thead>
                        <tr>
                            <th style="vertical-align:top;">
                                <asp:LinkButton ID="SortByReferenceNo" runat="server" CommandName="Sort" CommandArgument="ReferenceNo" ForeColor="White" >Reference No</asp:LinkButton></th>
                            <th style="vertical-align:top;">
                                <asp:LinkButton ID="SortByProjectName" runat="server" CommandName="Sort" CommandArgument="ProjectName"  ForeColor="White">Project Name</asp:LinkButton></th>
                            <th style="vertical-align:top;">Received Date</th>
                            <th style="vertical-align:top;">Permit Number</th>
                            <th style="vertical-align:top;">Owner</th>
                            <th style="vertical-align:top;">Submission Status</th>
                            <th style="vertical-align:top;">IsSigned</th>
                            <th style="vertical-align:top;">Submission Type</th>
                            <th style="vertical-align:top;" class="col-md-2">Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder" />
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="9">
                                <asp:DataPager ID="dpSubmissionByUser" runat="server" PageSize="10" PagedControlID="lvSubmissions">
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
                                        <h1 class="panel-title">Add a new submission</h1>
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
                            <asp:LinkButton ID="lbtnView" runat="server" CssClass="form-control" ToolTip="View Submission" CommandName="select" CommandArgument="<%#: Item.ReferenceNo%>" >V</asp:LinkButton>
                            <asp:LinkButton ID="lbtnSubmissionDetails" CssClass="form-control" runat="server" CommandName="select1" ToolTip="View Submission Overview" CommandArgument="<%#: Item.ReferenceNo%>">D</asp:LinkButton>
                            <asp:LinkButton ID="lbtnDelete" CssClass="form-control" runat="server" OnClientClick="javascript:return confirm('Are you sure to delete?')" CommandName="Delete" ToolTip="Delete Submission" CommandArgument="<%#: Item.ReferenceNo%>">X</asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table class=" table table-bordered table-hover table-condensed">
                    <thead>
                        <tr>
                            <th >
                                <asp:LinkButton ID="SortByReferenceNo" runat="server" CommandName="Sort" CommandArgument="ReferenceNo" ForeColor="White" >Reference No</asp:LinkButton></th>
                            <th >
                                <asp:LinkButton ID="SortByProjectName" runat="server" CommandName="Sort" CommandArgument="ProjectName"  ForeColor="White">Project Name</asp:LinkButton></th>
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
                                        <h1 class="panel-title">Add a new submission</h1>
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

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
