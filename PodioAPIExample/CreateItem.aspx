<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="CreateItem.aspx.cs" Inherits="PodioAPIExample.CreateItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Create Item</h1>
    </div>
    <div class="form-horizontal">
        <fieldset>
            <div class="control-group">
                <label class="control-label" for="input01">Title</label>
                <div class="controls">
                    <asp:TextBox ID="txtTitle" CssClass="input-xlarge"  runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="Required Field" CssClass="text-error" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="input01">Date</label>
                <div class="controls">
                    <asp:TextBox ID="txtDate" CssClass="input-xlarge itemdate"  runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="input01">Location</label>
                <div class="controls">
                    <asp:TextBox ID="txtLocation" CssClass="input-xlarge"  runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="input01">Number</label>
                <div class="controls">
                    <asp:TextBox ID="txtNumber" CssClass="input-xlarge"  runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cvNumber" runat="server" CssClass="text-error" ErrorMessage="Input shoud be number" ControlToValidate="txtNumber" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="input01">Link(with http/https)</label>
                <div class="controls">
                    <asp:TextBox ID="txtLink" CssClass="input-xlarge"  runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="input01">Money</label>
                <div class="controls">
                    <asp:TextBox ID="txtMoney" CssClass="input-xlarge"  runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cvMoney" runat="server" CssClass="text-error" ErrorMessage="Input shoud be number" ControlToValidate="txtMoney" Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="input01">App Reference</label>
                <div class="controls">
                    <asp:DropDownList ID="ddlReference" runat="server">
                        <asp:ListItem Text="Select App" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="input01">Image Upload</label>
                <div class="controls">
                    <asp:FileUpload ID="pnlImage" runat="server" />
                </div>
            </div>
            <div class="control-group">
                <div class="controls">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
                </div>
            </div>
             <div class="control-group">
                <div class="controls">
                   <asp:Label ID="lblSuccess" CssClass="text-success" runat="server"></asp:Label>
                </div>
            </div>
        </fieldset>
    </div>
</asp:Content>
