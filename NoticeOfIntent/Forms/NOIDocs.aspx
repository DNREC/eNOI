<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CLFNested.master" CodeBehind="NOIDocs.aspx.vb" Inherits="NoticeOfIntent.NOIDocs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NestedContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NestedContentPlaceHolderPageTitle" runat="server">
    <div class="well">
        <h2>Upload Documents</h2>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="NestedContentPlaceHolderBody" runat="server">
    <div class="panel panel-default">
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group ">
                    <asp:Label ID="lblFileDesc" AssociatedControlID="txtFileDesc" runat="server" Text="File Description:" CssClass="control-label col-md-2"></asp:Label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtFileDesc" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rvtxtFileDesc" ControlToValidate="txtFileDesc" CssClass="alert-text" Display="Dynamic" EnableClientScript="true" runat="server" ErrorMessage="File Description is required" ToolTip="File Description is required" ValidationGroup="ValidateDoc"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <asp:FileUpload ID="fileupload"  CssClass="form-control" runat="server" ClientIDMode="Static" ValidationGroup="ValidateDoc" />
                    <asp:Label ID="lblFileSelect" AssociatedControlID="txtFile" runat="server" Text="Select a file:" CssClass="control-label col-md-2"></asp:Label>
                    <asp:HiddenField ID="hfMaxlength" runat="server" ClientIDMode="Static" />
                    <div class="col-md-10">                    
                        <div class="input-group">
                            <asp:TextBox ID="txtFile" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="No file selected"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btnUploadFiles" runat="server" Text="UploadFiles" ClientIDMode="Static" CssClass="btn btn-default" ValidationGroup="ValidateDoc" />
                                <asp:HyperLink ID="hlUploadFiles" runat="server" ClientIDMode="Static" CssClass="btn btn-default" ValidationGroup="ValidateDoc">Upload Files</asp:HyperLink>
                            </div>
                        </div>
                        <asp:CustomValidator ID="cvFileUpload" runat="server" Display="dynamic" ValidationGroup="ValidateDoc" ControlToValidate="fileupload" ClientValidationFunction="cvFileUpload_ClientValidation" EnableClientScript="true" ErrorMessage="File does not exists!" ToolTip="File does not exists!" CssClass="alert-text"></asp:CustomValidator>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title font-bold">List of uploaded documents</h1>
        </div>
        <div class="panel-body">
            <asp:ListView ID="lvUploadedFiles" runat="server" ItemType="NoticeOfIntent.NOISubmissionDocs" DataKeyNames="NOIDocID,SubmissionID" SelectMethod="lvUploadedFiles_GetData" DeleteMethod="lvUploadedFiles_DeleteItem" >
                <LayoutTemplate>
                    <table class="table table-bordered table-hover table-condensed">
                        <thead>
                            <tr>
                                <th>Document Name</th>
                                <th>Document Desc</th>
                                <th>Document Type</th>
                                <th>Action</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr runat="server" id="itemPlaceholder" />
                        </tbody>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton ID="lbtnDocumentName" runat="server" CommandName="download" CommandArgument='<%#: Item.NOIDocID %>' ToolTip="Click to Download"><%#: Item.DocumentName %></asp:LinkButton></td>
                        <td><%#: Item.DocumentDesc %></td>
                        <td><%#: Item.DocumentType %></td>
                        <td>
                            <asp:ImageButton ID="ibtnDelete" runat="server" AlternateText="Delete" CssClass="btn-xs" CommandName="Delete" ImageUrl="~/Content/images/Delete.png" ToolTip="Delete this document" OnClientClick="javascript:return confirm('Are you sure to delete?')" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="panel-footer">
            <asp:CustomValidator ID="cvCheckDocument" runat="server" CssClass="alert-text" Display="Dynamic" ValidationGroup="DocRequired" ErrorMessage="At least one document has to be uploaded." ToolTip="At least one document has to be uploaded."></asp:CustomValidator>
        </div>
    </div>
    <div class="well clearfix">
        <div class="col-md-2 clearfix pull-right">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="btn btn-default btn-block" ValidationGroup="DocRequired" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NestedContentPlaceHolderJS" runat="server">
    <script>
        $(document).ready(function () {

             $('#txtFile').prop('disabled', true)
             $('#fileupload').hide();
            $('#btnUploadFiles').hide();

            $('#hlUploadFiles').click(function () {
                $('#fileupload').click();
            })

            $('#txtFileDesc').keyup(function () {
                $('#hfFileDesc').val($(this).val());
            })

            function splitpath(paths) {
                    var st = paths.split("\\");
                    return st[st.length - 1];
            }


            $('#fileupload').change(function () {
                var varfile = $(this);
                document.getElementById("txtFile").value = splitpath(varfile.val()); 
                uploadfile();
            })

            function uploadfile() {
                $('#btnUploadFiles').click();
            }

           
        
        })

 function cvFileUpload_ClientValidation(source, arguments) {
     var varfile = $('#fileupload');
     var maxfilesize = $('#hfMaxlength').val();
     if (varfile[0].files[0].size > (maxfilesize * 1024)) {
         source.innerHTML = "You can only upload a document of size " + (maxfilesize / 1024) + " mb."
        arguments.IsValid = false
    }
    else {
        arguments.IsValid = true
    }
}
            
    </script>
</asp:Content>
