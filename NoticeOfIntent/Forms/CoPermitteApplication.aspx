<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="CoPermitteApplication.aspx.vb" Inherits="NoticeOfIntent.CoPermitteApplication" %>
<%@ Register src="../UserControls/ucNameAddressInfo.ascx" tagname="ucNameAddressInfo" tagprefix="uc1" %>
<%@ Register src="../UserControls/ucNameAddressInfoReadOnly.ascx" tagname="ucNameAddressInfoReadOnly" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <div class="page-header well text-center font-bold">
        <h1>Co-Permittee Application<br /><small>for Shared Operational Control of<br />Storm Water Discharges Associated With<br /> CONSTRUCTION ACTIVITY Under a NPDES General Permit</small></h1>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">

<%--    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="alert alert-danger" ShowModelStateErrors="true" EnableClientScript="true" DisplayMode="BulletList" HeaderText="<div class='validationheader'>&nbsp;Please correct the following error:</div>" />
    </div>--%>
    <div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title font-bold">Permit Information</h2>
            </div>
            <div class="panel-body input-group-sm">
                <div class="row">
                    <div class="col-md-6 input-group-sm">
                        <asp:Label ID="lblNOIID" AssociatedControlID="lblNOIIDDisplay" runat="server" Text="NOI ID#:" CssClass="control-label "></asp:Label>
                        <asp:Label ID="lblNOIIDDisplay" runat="server" Text="" CssClass="form-control-static  well-sm"></asp:Label>
                        <asp:HiddenField ID="hfNOISubmissionID" Value="" runat="server" />
                    </div>
                    <div class="col-md-6 input-group-sm">
                        <asp:Label ID="lblNOIDateReceived" AssociatedControlID="lblNOIDateReceivedDisplay" runat="server" Text="Date Received:" CssClass="control-label"></asp:Label>
                        <asp:Label ID="lblNOIDateReceivedDisplay" runat="server" Text="" CssClass="form-control-static well-sm"></asp:Label>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="row">
                    <div class="col-md-12">
                        <uc2:ucNameAddressInfoReadOnly ID="ucnaProjectDetails" runat="server" CompanyNameLabel="Project Name:" companytypevisible="false" personnamevisible="false" address1visible="false" address2visible="false" CityZipStateVisible="false" phonevisible="false" emailvisible="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title font-bold">Original Permittee Information</h2>
            </div>
            <div class="panel-body input-group-sm">
                <uc2:ucNameAddressInfoReadOnly ID="ucnaOriPermitteeInfo" runat="server" companytypevisible="false" countymunicipalityvisible="false" />
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title font-bold">Co-Permittee Information</h2>
            </div>
            <div class="panel-body input-group-sm">
                <uc1:ucNameAddressInfo ID="ucnaCoPermitteeInfo" runat="server" countymunicipalityvisible="false" ValidationGroup="ValidateNOI" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblComments" AssociatedControlID="txtComments" runat="server" Text="Comments:" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" MaxLength="2000" CssClass="form-control input-group-sm"></asp:TextBox>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h2 class="panel-title font-bold">Shared Operational Control Agreement</h2>
            </div>
            <div class="panel-body input-group-sm">
                <p class="">
                    The above parties agree to share operational control under the above referenced permit effective <span class="form-inline"><asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox></span><asp:RequiredFieldValidator ID="rfvEffectiveDate" ControlToValidate="txtEffectiveDate" runat="server" Display="Dynamic" EnableClientScript="true" CssClass="alert-text" ToolTip="Effective Date is required." ErrorMessage="Effective Date is required." ValidationGroup="ValidateNOI">*</asp:RequiredFieldValidator>.</p>
                <p> Co-Permittee hereby assumes joint and severable responsibility, coverage, and liability under the permit for any obligations, duties, responsibilities and violations under said permit. Original Permittee shall remain liable under the permit for violations of the permit conditions up to and including the above referenced date and until a Notice of Termination is filed and acknowledged by the DNREC, Watershed Stewardship. Co-permittee shall remain liable under the permit for violations of the permit conditions up to and including the above referenced date and until a Notice of Termination is filed and acknowledged by the DNREC, Watershed Stewardship or until co-permittee no longer maintains operational control.
                </p>
            </div>
        </div>
        <div class="well clearfix">
            <div class="col-md-4 clearfix pull-right">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-block" ValidationGroup="ValidateNOI" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-default btn-block"  />           
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

<%--                            <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Save & Submit" CssClass="btn btn-default btn-lg btn-block" />
                        </td>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
    <script>
        $(document).ready(function () {

            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_txtZip').on("change", function (e) {
                e.preventDefault();
                $.getJSON('../api/Zipcodes/' + $(this).val())
                .done(function (data) {
                    if (data != null) {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_txtCity').val(data.PO_Name)
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_ddlStateAbv').val(data.StateAbv)
                    }
                    else {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_txtCity').val("")
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_ddlStateAbv').val("--")
                        alert("no matching records available.");
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_txtZip').focus()
                    }
                })
                .fail(function (jqxhr, textStatus, error) {
                    alert(error);
                })

            });


            $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_ddlCompanyType').each(function (e) {
                //debugger;
                if ($(this).val() == 'P') {
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_divcompanyname').hide();
                    ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_txtCompanyName")[0], false);
                    //$('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divpersonname').show();
                    //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName")[0], true);
                    //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName")[0], true);
                }
                else {
                    $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_divcompanyname').show();
                    ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_txtCompanyName")[0], true);
                    //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName")[0], false);
                    //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName")[0], false);
                    //$('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divpersonname').hide();
                }

                $(this).on("change", function (e) {
                    //debugger;

                    if ($(this).val() == 'P') {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_divcompanyname').hide();
                        ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_txtCompanyName")[0], false);
                        //$('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divpersonname').show();
                        //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName")[0], true);
                        //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName")[0], true);
                    }
                    else {
                        $('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_divcompanyname').show();
                        ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_ucnaCoPermitteeInfo_txtCompanyName")[0], true);
                        //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtLastName")[0], false);
                        //ConfigureValidators($("#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_txtFirstName")[0], false);
                        //$('#ContentPlaceHolderBody_NestedContentPlaceHolderBody_wzNOI_ucnaOwnerInfo_divpersonname').hide();
                    }
                });

            });


            //useful function to disable the .net validators client side.
            function ConfigureValidators(control, enabled) {
                //debugger;
                //enabled = enabled || false;
                //control.style.visibility=!enabled
                if (typeof Page_Validators != 'undefined') {
                    for (i = 0; i <= Page_Validators.length; i++) {
                        if (Page_Validators[i] != null) {
                            //var visible = $('#' + Page_Validators[i].controltovalidate).parent().is(':visible');
                            if (Page_Validators[i].controltovalidate == control.id) {
                                //validator.enabled = enabled;
                                //Page_Validators[i].enabled = enabled;
                                ValidatorEnable(Page_Validators[i], enabled);

                            }

                        }
                    }
                };

            }



        })

    </script>
</asp:Content>
