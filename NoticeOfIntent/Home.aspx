<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralNested.Master" CodeBehind="Home.aspx.vb" Inherits="NoticeOfIntent.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">   
    <h2></h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <asp:Panel ID="pnlExternal" runat="server">
        <div class="well">
            <p class="font-bold">Welcome to Online Submission of Notice of Intent</p>
            <p>This web site allows you to submit the <asp:Label ID="lblHeaderText" runat="server" Text=""></asp:Label> electronically. </p>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-default" /><asp:Button ID="btnNewUser" runat="server" Text="NewUser" CssClass="btn btn-default" />
            </div>
            <div id="divuserguide" runat="server">
                <p>Please <asp:HyperLink ID="hylUserGuide" runat="server" Target="_blank">click here</asp:HyperLink> to download the user guide</p>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlInternal" runat="server">
        <div class="well">
            <p class="font-bold">Welcome to Online Submission of Notice of Intent</p>
            <p>This web site allows you to submit the <asp:Label ID="lblHeaderTextInternal" runat="server" Text=""></asp:Label> electronically. </p>
            <div>
                <asp:Button ID="btnSubmissions" runat="server" Text="Submissions" CssClass="btn btn-default"/>
                <asp:Button ID="btnManageUser" runat="server" Text="Manage User(s)" CssClass="btn btn-default" />
                <asp:Button ID="btnSubmitToEPA" runat="server" Text="Send Data To EPA" CssClass="btn btn-default" Enabled="false"/>
            </div>  
        </div>
    </asp:Panel>
    <h2>
        <asp:Label ID="lblNotification" runat="server" Text="" Visible="false"></asp:Label>
    </h2>
    <div id="divExistingPermit" runat="server" class="panel panel-default">
        <div class="panel-heading">
            <h2 class="panel-title font-bold">List of NOI's for <asp:Label ID="lblListHeaderText" runat="server" Text=""></asp:Label></h2>
        </div>
        <div class="panel-body input-group-sm">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div data-toggle="collapse" data-target="#SearchPanel" class="panel-title font-bold cursorpointer">Search Criteria <span class="pull-right glyphicon glyphicon-chevron-up"></span></div>
                </div>
                <div id="SearchPanel" class="panel-collapse collapse in">
                    <div class="panel-body input-group-sm">
                        <div id="divCSSearch" runat="server" class="table-responsive">
                            <table class="table table-bordered table-hover table-condensed">
                                <thead>
                                    <tr>
                                        <th >Delegated Agency</th>
                                        <th >Received Date From</th>
                                        <th >Received Date To</th>
                                        <th >Permit Number</th>
                                        <th >Project Name Begins With</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlPlanApprovalAgency" runat="server" CssClass="form-control" DataTextField="DisplayName" DataValueField="PersonOrgID" ItemType="NoticeOfIntent.PlanApprovalAgency" SelectMethod="GetPlanApprovalAgency">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRecdDateFrom" runat="server" Text="" CssClass="form-control datepicker"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRecdDateTo" runat="server" Text="" CssClass="form-control datepicker"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPermitNumber" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjectName" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>

                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td colspan="5">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-default" />
                                        </td>
                                    </tr>

                                </tfoot>
                            </table>
                        </div>
                        <div id="divISSearch" runat="server" class="table-responsive" >
                            <table class="table table-bordered table-hover table-condensed">
                                <thead>
                                    <tr>
                                        <th>Project Name Begins With</th>
                                        <th>Permit Number</th>
                                        <th>Received Date From</th>
                                        <th>Received Date To</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>

                                            <asp:TextBox ID="txtProjectName1" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPermitNumber1" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRecdDateFrom1" runat="server" Text="" CssClass="form-control datepicker"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRecdDateTo1" runat="server" Text="" CssClass="form-control datepicker"></asp:TextBox>
                                        </td>
                                    </tr>

                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="btnSearch1" runat="server" Text="Search" CssClass="btn btn-default" />
                                        </td>
                                    </tr>

                                </tfoot>
                            </table>
                        </div>
                        
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3 pull-right alert-text">
                    <label>*Closed NOIs are in Red</label>
                </div>
            </div>
            <div id="divCSNOIList" runat="server" class="table-responsive">
                <asp:ListView ID="lvNOISearch" runat="server" ItemType="NoticeOfIntent.NOIPublicView"  DataKeyNames="PiID,PermitNumber" SelectMethod="lvNOISearch_GetData">
                    <LayoutTemplate>
                        <table class="table table-bordered table-hover table-condensed">
                            <thead>
                                <tr>
                                    <th >Permit Number</th>
                                    <th >Project Name</th>
                                    <th >Received Date</th>
                                    <th >Project Type</th>
                                    <th >Delegated Agency</th>
                                    <th >County</th>
                                    <th >Owner(s)</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr runat="server" id="itemPlaceHolder" />
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="8">
                                        <asp:DataPager ID="dpNOISearch" runat="server" PageSize="15" PagedControlID="lvNOISearch">
                                            <Fields>
                                                <asp:NumericPagerField ButtonType="Button" ButtonCount="5" />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr runat="server">
                            <td><%#: Item.PermitNumber%>
                                <asp:HiddenField ID="hfPiID" runat="server" Value="<%#: Item.PiID%>" />
                                <asp:HiddenField ID="hfPermitStatus" runat="server" Value="<%#: Item.PermitStatusCode%>" />
                            </td>
                            <td><%#: Item.ProjectName%></td>
                            <td><%#: Item.DateReceived.ToShortDateString()%></td>
                            <td><%#: Item.ProjectType%></td>
                            <td><%#: Item.DelegateAgency%></td>
                            <td><%#: Item.ConstructCounty%></td>
                            <td>
                                <asp:ListView ID="lvProjectOwners" runat="server" DataKeyNames="PiID,PermitNumber,AfflID,AfflTypeCode" ItemType="NoticeOfIntent.NOIOwner"
                                    SelectMethod="lvProjectOwners_GetData" OnItemCommand="lvProjectOwners_ItemCommand">
                                    <LayoutTemplate>
                                        <table class="table table-bordered table-hover table-condensed">
                                            <tr runat="server" id="itemPlaceHolder" />
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#: Item.OrgName%>
                                                <asp:HiddenField ID="hfAfflActive" runat="server" Value="<%#: Item.AfflActive %>" />
                                                <asp:HiddenField ID="hfAfflEndDate" runat="server" Value ="<%#: Item.AfflEndDate %>" />
                                            </td>
                                            <td class="col-md-2">
                                                <asp:LinkButton ID="lbtnView" runat="server" CssClass="form-control" CommandName="view" CommandArgument='<%# Item.PiID.ToString() + "," + Item.PermitNumber + "," + Item.AfflID.ToString() + "," + Item.AfflTypeCode.ToString()%>'>View</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <table class="table table-bordered table-hover table-condensed">
                                            <tr>
                                                <td> </td>
                                                <td><asp:LinkButton ID="lbtnViewEmpty" runat="server" CssClass="form-control" CommandName="viewempty">View</asp:LinkButton></td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div id="divISNOIList" runat="server" class="table-responsive" >
                <asp:ListView ID="lvISNOISearch" runat="server" ItemType="NoticeOfIntent.NOIPublicViewForIS" DataKeyNames="PiID,PermitNumber" SelectMethod="lvISNOISearch_GetData"
                                               OnItemCommand="lvISNOISearch_ItemCommand"  >
                    <LayoutTemplate>
                        <table class="table table-bordered table-hover table-condensed">
                            <thead>
                                <tr>
                                    <th >Permit Number</th>
                                    <th class="col-md-4" ><asp:Label ID="lblISSearchLabel2" runat="server" Text="Facility Name"></asp:Label></th>
                                    <th >Received Date</th>
                                    <th >City</th>
                                    <th class="col-md-4">Owner</th>
                                    <th >Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr runat="server" id="itemPlaceHolder" />
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="6">
                                        <asp:DataPager ID="dpISNOISearch" runat="server" PageSize="15" PagedControlID="lvISNOISearch">
                                            <Fields>
                                                <asp:NumericPagerField ButtonType="Button" ButtonCount="5" />
                                            </Fields>
                                        </asp:DataPager>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr runat="server">
                            <td><%#: Item.PermitNumber%>
                                <asp:HiddenField ID="hfPiID" runat="server" Value="<%#: Item.PiID%>" />
                                <asp:HiddenField ID="hfPermitStatus" runat="server" Value="<%#: Item.PermitStatusCode%>" />
                            </td>
                            <td><%#: Item.ProjectName%></td>
                            <td><%#: Item.DateReceived.ToShortDateString()%></td>
                            <td><%#: Item.City%></td>
                            <td><%#: Item.OrgName%>
                                <asp:HiddenField ID="hfAfflActive" runat="server" Value="<%#: Item.AfflActive %>" />
                            </td>
                            <td>
                                <asp:LinkButton ID="lbtnView" runat="server" CssClass="form-control;" CommandName="view" CommandArgument='<%# Item.PiID.ToString() + "," + Item.PermitNumber + "," + Item.AfflID.ToString + "," + Item.AfflTypeCode.ToString()%>'>View</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>
    

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
    <script>
        $(document).ready(function () {

            $('.panel-collapse').on('hide.bs.collapse', function (e) {
                //debugger;
                if ($(this).is(e.target)) {
                    var $ctl = $(this).parent().find('.panel-title>span').first()
                    if ($ctl.hasClass('glyphicon glyphicon-chevron-up')) {
                        $ctl.removeClass('glyphicon glyphicon-chevron-up');
                        $ctl.addClass('glyphicon glyphicon-chevron-down');
                    }
                }

            });

            $('.panel-collapse').on('show.bs.collapse', function (e) {
                //debugger;
                if ($(this).is(e.target)) {
                    var $ctl = $(this).parent().find('.panel-title>span').first()
                    if ($ctl.hasClass('glyphicon glyphicon-chevron-down')) {
                        $ctl.removeClass('glyphicon glyphicon-chevron-down');
                        $ctl.addClass('glyphicon glyphicon-chevron-up');
                    }
                }
            });

            $("input[name*='hfPermitStatus']").each(function () {

                if ($(this).val() == 'C') {
                    $(this).parent().parent().addClass("alert-text");
                }

            })

            //$("input[name*='hfAfflActive']").each(function () {

            //    if ($(this).val() == 'N') {
            //        $(this).parent().parent().addClass("alert-text");
            //    }
            //    else
            //    {
            //        $(this).parent().parent().addClass("normal-text");
            //    }

            //})

             $("input[name*='hfAfflEndDate']").each(function () {

                if ($(this).val() != '') {
                    $(this).parent().parent().addClass("alert-text");
                }
                else
                {
                    $(this).parent().parent().addClass("normal-text");
                }

            })


        })
    </script>
</asp:Content>