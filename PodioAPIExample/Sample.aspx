<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Sample.aspx.cs" Inherits="PodioAPIExample.Sample" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="width: 999px;">
        <label class="date-label">Date From</label>
        <asp:TextBox runat="server" ID="txtDateFrom" CssClass="datepic"></asp:TextBox>
        <label class="date-label">Date To</label>
        <asp:TextBox runat="server" ID="txtDateTo" CssClass="datepic"></asp:TextBox>
        <asp:Button ID="btnButtom" runat="server" CssClass="date-buttom" Text="Filter" OnClick="btnButtom_Click" />
        <div class="clearfix"></div>
    </div>
<hr />
    <asp:Label ID="lblError" runat="server" Font-Size="Large"></asp:Label>
    <asp:Repeater ID="rpItems" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-bordered table-condensed table_margin">
                <tr>
                    <b>
                        <td>Created On</td>
                        <td>Title</td>
                        <td>Location</td>
                        <td>Link</td>
                        <td>Money</td>
                    </b>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("CreatedOn")%></td>
                <td><%# Eval("Title")%></td>
                <td><%# Eval("Location")%></td>
                <td><%# Eval("Link")%></td>
                <td><%# Eval("Money")%></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
