<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="ManageProjects.aspx.vb" Inherits="NoticeOfIntent.ManageProjects" %>
<%@ Register src="../UserControls/ucNameAddressInfoReadOnly.ascx" tagname="ucNameAddressInfoReadOnly" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <div class="jumbotron">
        <h1 class="center">Manage Projects</h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h1 class="panel-title">List of projects in eNOI Database</h1>
            </div>
            <div class="panel-body input-group-sm">
                <asp:HiddenField ID="hfReportID" runat="server" ClientIDMode="Static" />
                <asp:ListView ID="lvProjects" runat="server" DataKeyNames="ProjectID,PermitNumber" ItemType="NoticeOfIntent.NOIProject" SelectMethod="lvProjects_GetData">
                    <LayoutTemplate>
                        <table class=" table table-bordered table-hover table-condensed">
                            <thead>
                                <tr>
                                    <th >Project Number</th>
                                    <th >Project Name</th>
                                    <th >Permit Number</th>
                                    <th >Address</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr runat="server" id="itemPlaceholder" />
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="8">
                                        <asp:DataPager ID="dpProjects" runat="server" PageSize="5" PagedControlID="lvProjects">
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
                            <%--<td><%#: Item.ProjectAddress%></td>--%>
                        </tr>
                    </ItemTemplate>

                </asp:ListView>
                <div class="form-group input-group-sm">
                    <label class="form-control-static">
                        *Note: Once the project is imported to eNOI the admin has to attach the project for the requested registered users in CROMERR or Send the Project Number for the User for registering in CROMERR.
                    </label>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title font-bold">Add Projects which are not existing in eNOI Database from DEN</h2>
            </div>
            <div class="panel-body input-group-sm">
                <div class="form-group input-group-sm">
                    <asp:Label ID="lblSelectProject" runat="server" class="control-label" AssociatedControlID="txtIntProject" Text="Enter Project Name which needs to be added"></asp:Label>
                    <asp:TextBox ID="txtIntProject" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                </div><br/>
                <div class="row">
                    <div class="col-md-12 input-group-sm">
                        <label class="control-label">Project ID:</label>
                        <asp:Label ID="lblProjectID" runat="server" Text="" CssClass="form-control-static well-sm" ClientIDMode="Static"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:HiddenField ID="hfProgID" runat="server" Value="" ClientIDMode="Static" />
                        <asp:HiddenField ID="hfProjectName" runat="server" Value="" ClientIDMode="Static" />
                        <uc2:ucNameAddressInfoReadOnly ID="ucnaProjectDetails" runat="server" CompanyNameLabel="Project Name:" Address1Label="Project Location/Address:" personnamevisible="false" companytypevisible="false" address2visible="false" phonevisible="false" emailvisible="false" countymunicipalityvisible="false" ClientIDMode="Static"  />
                    </div>
                </div><br />
                <div class="row">
                    <div class="col-md-6 input-group-sm">
                        <label class="control-label">Latitude:</label>
                        <asp:Label ID="lblLatitude" runat="server" Text="" CssClass="form-control-static  well-sm" ClientIDMode="Static"></asp:Label>
                    </div>
                    <div class="col-md-6 input-group-sm">
                        <label class="control-label">Longitude:</label>
                        <asp:Label ID="lblLongitude" runat="server" Text="" CssClass="form-control-static  well-sm" ClientIDMode="Static"></asp:Label>
                    </div>
                </div>
                <div class="well clearfix">
                    <div class="clearfix pull-right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnImportProject" runat="server" Text="Import Project" CssClass="btn btn-default btn-block" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
        <script>
            $(document).ready(function () {

                $('#txtIntProject').autocomplete({
                    source: function (request, response) {
                        //debugger;
                        $.ajax({
                            url: "../api/ExistingProjects",
                            data: { projectname: request.term, reportid: $('#hfReportID').val() },       //encodeURIComponent(arguments.Value)},
                            datatype: "json",
                            type: "GET",
                            contentType: "application/json; charset=utf-8",
                            success: function (msg) {
                                //var st = $.parseJSON(msg);
                                //response(st);
                                response(msg);
                            }

                        });



                    },
                    change: function (event, ui) {
                        //debugger;
                        if (ui.item == null || ui.item == undefined) {
                            this.value = "";
                            $('#hfProgID')[0].value = "";
                            $('#hfProjectName')[0].value = "";
                            $('#lblProjectID')[0].value = "";
                            $('#lblCompanyNameDisp')[0].value = "";
                            $('#lblAddress1Disp')[0].value = "";
                            $('#lblZipDisp')[0].value = "";
                            $('#lblCityDisp')[0].value = "";
                            $('#lblStateDisp')[0].value = "";
                            //$('#lblCountyDisp')[0].value = "";
                            //$('#lblMunicipalityDisp')[0].value = "";
                            $('#lblLatitude')[0].value = "";
                            $('#lblLongitude')[0].value = "";

                            $('#lblProjectID').text("");
                            $('#lblCompanyNameDisp').text("");
                            $('#lblAddress1Disp').text("");
                            $('#lblZipDisp').text("");
                            $('#lblCityDisp').text("");
                            $('#lblStateDisp').text("");
                            //$('#lblCountyDisp').text("");
                            //$('#lblMunicipalityDisp').text("");
                            $('#lblLatitude').text("");
                            $('#lblLongitude').text("");
                        }

                    },
                    autoFocus: true,
                    minLength: 1,
                    select: function (event, ui) {
                        //debugger;
                        selectedvaln = ui.item;

                        $('#lblProjectID')[0].value = ui.item.PermitNumber;
                        $('#hfProgID')[0].value = ui.item.PermitNumber;
                        $('#lblCompanyNameDisp')[0].value = ui.item.ProjectName;
                        $('#hfProjectName')[0].value = ui.item.ProjectName;
                        $('#lblAddress1Disp')[0].value = ui.item.ProjectAddress;
                        $('#lblZipDisp')[0].value = ui.item.ProjectPostalCode;
                        $('#lblCityDisp')[0].value = ui.item.ProjectCity;
                        $('#lblStateDisp')[0].value = ui.item.ProjectStateAbv;
                       // $('#lblCountyDisp')[0].value = ui.item.ProjectCounty;
                        //$('#lblMunicipalityDisp')[0].value = ui.item.ProjectMunicipality;
                        $('#lblLatitude')[0].value = ui.item.Latitude;
                        $('#lblLongitude')[0].value = ui.item.Longitude;


                        $('#lblProjectID').text(ui.item.PermitNumber)
                        $('#lblCompanyNameDisp').text(ui.item.ProjectName);
                        $('#lblAddress1Disp').text(ui.item.ProjectAddress);
                        $('#lblZipDisp').text(ui.item.ProjectPostalCode);
                        $('#lblCityDisp').text(ui.item.ProjectCity);
                        $('#lblStateDisp').text(ui.item.ProjectStateAbv);
                        //$('#lblCountyDisp').text(ui.item.ProjectCounty);
                       // $('#lblMunicipalityDisp').text(ui.item.ProjectMunicipality);
                        $('#lblLatitude').text(ui.item.Latitude);
                        $('#lblLongitude').text(ui.item.Longitude);
                    }
                }).focus(function () {
                    $(this).autocomplete("search");
                }).blur(function () {
                    if (this.value == "") {

                        
                        $('#hfProgID')[0].value = "";
                        $('#lblProjectID')[0].value = "";
                        $('#hfProjectName')[0].value = "";
                        $('#lblCompanyNameDisp')[0].value = "";
                        $('#lblAddress1Disp')[0].value = "";
                        $('#lblZipDisp')[0].value = "";
                        $('#lblCityDisp')[0].value = "";
                        $('#lblStateDisp')[0].value = "";
                       // $('#lblCountyDisp')[0].value = "";
                        //$('#lblMunicipalityDisp')[0].value = "";
                        $('#lblLatitude')[0].value = "";
                        $('#lblLongitude')[0].value = "";

                        $('#lblProjectID').text("");
                        $('#lblCompanyNameDisp').text("");
                        $('#lblAddress1Disp').text("");
                        $('#lblZipDisp').text("");
                        $('#lblCityDisp').text("");
                        $('#lblStateDisp').text("");
                       // $('#lblCountyDisp').text("");
                       // $('#lblMunicipalityDisp').text("");
                        $('#lblLatitude').text("");
                        $('#lblLongitude').text("");
                    }
                });
                

            })
    </script>
</asp:Content>
