<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="DisplayProjects.aspx.vb" Inherits="NoticeOfIntent.DisplayProjects" %>
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
                <table class="table table-bordered table-hover table-condensed">
                    <thead>
                        <tr>
                            <th>Project Number</th>
                            <th>Project Name</th>
                            <th>Permit Number</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtProjectNumber" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProjectName" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPermitNumber" runat="server" Text="" CssClass="form-control"></asp:TextBox>
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
        <asp:MultiView ID="mvExistingPermits" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwCoPermittee" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h1 class="panel-title font-bold">List of approved projects of current user</h1>
                        <p>Please contact <asp:Label ID="lblAdminDeptName" runat="server" ></asp:Label> if you don't find the project you are looking for.</p>
                    </div>
                    <div class="panel-body input-group-sm">
                        <asp:ListView ID="lvSubmissionByUser" runat="server" DataKeyNames="ProjectID,PermitNumber" ItemType="NoticeOfIntent.NOIProject" SelectMethod="lvSubmissionByUser_GetData">
                            <LayoutTemplate>
                                <table class=" table table-bordered table-hover table-condensed">
                                    <thead>
                                        <tr>
                                            <th >Project Number</th>
                                            <th >Project Name</th>
                                            <th >Permit Number</th>
                                            <th  class="col-md-2">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr runat="server" id="itemPlaceholder" />
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="9">
                                                <asp:DataPager ID="dpSubmissionByUser" runat="server" PageSize="10" PagedControlID="lvSubmissionByUser">
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
                                    <td><%#: Item.ProjectNumber%></td>
                                    <td><%#: Item.ProjectName%></td>
                                    <td><%#: Item.PermitNumber%></td>
                                    <td>
                                        <div class="form-inline">
                                            <asp:LinkButton ID="lbtnAddCoPermittee" runat="server" CssClass="form-control" CommandName="Add" CommandArgument='<%# Item.ProjectID.ToString() + "," + Item.PermitNumber %>'>Add CoPermittee</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </asp:View>
            <asp:View ID="vwCoPermiteeTerminate" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h1 class="panel-title">List of approved CoPermittee Submission of current user</h1>
                    </div>
                    <div class="panel-body input-group-sm">
                        <asp:ListView ID="lvCoPermitSubmissionByUser" runat="server" DataKeyNames="PermitNumber,AfflID" ItemType="NoticeOfIntent.ProjectOwnerView" SelectMethod="lvCoPermitSubmissionByUser_GetData">
                            <LayoutTemplate>
                                <table class=" table table-bordered table-hover table-condensed">
                                    <thead>
                                        <tr>
                                            <th >ProjectNumber</th>
                                            <th >Project Name</th>
                                            <th >Permit Number</th>
                                            <th >Org Name</th>
                                            <th >Last Name</th>
                                            <th >First Name</th>
                                            <th  class="col-md-2">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr runat="server" id="itemPlaceholder" />
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="9">
                                                <asp:DataPager ID="dpCoPermitSubmissionByUser" runat="server" PageSize="10" PagedControlID="lvCoPermitSubmissionByUser">
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
                                    <td><%#: Item.ProjectNumber%></td>
                                    <td><%#: Item.ProjectName%></td>
                                    <td><%#: Item.PermitNumber%></td>
                                    <td><%#: Item.OrgName%></td>
                                    <td><%#: Item.LName%></td>
                                    <td><%#: Item.FName%></td>
                                    <td>
                                        <div class="form-inline">
                                            <asp:LinkButton ID="lbtnAddCopermitteeTerminate" runat="server" CssClass="form-control" CommandName="Add" CommandArgument='<%# Item.PermitNumber + "," + Item.AfflID.ToString%>' >Add Termination</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class=" table table-bordered table-hover table-condensed">
                                    <thead>
                                        <tr>
                                            <th >ProjectNumber</th>
                                            <th >Project Name</th>
                                            <th >Permit Number</th>
                                            <th >Org Name</th>
                                            <th >Last Name</th>
                                            <th >First Name</th>
                                            <th  class="col-md-2">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td colspan="7">
                                                <p>No approved CoPermittee Submissions exists.</p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </EmptyDataTemplate>

                        </asp:ListView>
                        <%--<asp:LinqDataSource ID="LinqDataSource1" OnSelecting="LinqDataSource1_Selecting" runat="server"></asp:LinqDataSource>--%>
                    </div>
                </div>
            </asp:View>
            <%--<asp:View ID="vwGenralTerminate" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h1 class="panel-title">List of approved General Permit Submission of current user</h1>
                    </div>
                    <div class="panel-body input-group-sm">
                        <asp:ListView ID="lvGeneralPermitSubmissionByUser" runat="server" DataKeyNames="ProjectID,PermitNumber" ItemType="NoticeOfIntent.NOIProject" SelectMethod="lvSubmissionByUser_GetData">
                            <LayoutTemplate>
                                <table class=" table table-bordered table-hover table-condensed">
                                    <thead>
                                        <tr>
                                            <th >Project Number</th>
                                            <th >Project Name</th>
                                            <th >Permit Number</th>
                                            <th  class="col-md-2">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr runat="server" id="itemPlaceholder" />
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="9">
                                                <asp:DataPager ID="dpGeneralPermitSubmissionByUser" runat="server" PageSize="10" PagedControlID="lvGeneralPermitSubmissionByUser">
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
                                    <td><%#: Item.ProjectNumber%></td>
                                    <td><%#: Item.ProjectName%></td>
                                    <td><%#: Item.PermitNumber%></td>
                                    <td>
                                        <div class="form-inline">
                                            <asp:LinkButton ID="lbtnAddGeneralTerminate" runat="server" CssClass="form-control" CommandName="Add" CommandArgument='<%# Item.ProjectID.ToString() + "," + Item.PermitNumber %>'>Add Termination</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table class=" table table-bordered table-hover table-condensed">
                                    <thead>
                                        <tr>
                                            <th >Project Number</th>
                                            <th >Project Name</th>
                                            <th >Permit Number</th>
                                            <th  class="col-md-2">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td colspan="6">
                                                <p>No approved General Permit Submission exists.</p>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </EmptyDataTemplate>

                        </asp:ListView>
                    </div>
                </div>
            </asp:View>--%>
        </asp:MultiView>
    </div>
    <div class="col-md-2 pull-right">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnHome" runat="server" Text="Cancel"/>
                </td>
            </tr>
        </table>
    </div>
    <div class="clearfix"></div>
    <br />
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
</asp:Content>
