<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Authorization.aspx.cs" Inherits="PodioAPIExample.Authorization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-header">
        <h1>Authorize/authentication method</h1>
    </div>
    <div class="row">
        <div class="span4">
            <h2>Connect using authorization code</h2>
            <p>
                This server-side flow works by sending you to Podio to authorize. It's a four step process:
            </p>
            <ol>
                <li>redirects you user to Podio for authorization</li>
                <li>Podio redirects you back to this web app with an authorization code</li>
                <li>You use the authorization code to obtain an access token</li>
                <li>You use the access token for all API requests</li>
            </ol>
            <p></p>

        </div>
        <div class="span4">
            <h2>Connect as Podio app</h2>
            <p>
                The app authentication flow is suitable in situations where you only need data from a single app and do not wish authenticate as a specific user. It is similar to the username &amp; password flow, but uses the app ID and a special app token as the login credentials.
            </p>
            <p>
                When you authenticate as an app you can only access that specific app and if you create content it will appear as having been created by the app itself rather than a specific user. 
            </p>
            <p>Uses for the app authentication flow are automated scripts that run without any user interaction.</p>
        </div>
        <div class="span4">
            <h2>Connect with Username &amp; Password</h2>
            <p>
                The username and password flow is suitable for clients capable of asking end-users for their usernames and passwords. The advantage over HTTP Basic, is that the user credentials are used in a single request and are exchanged for an access token and refresh token. This eliminates the need to store the username and password.
            </p>
            <p>
                Unlike the server-side flow there are no redirects to the Podio authorization page because the user provides their username and password directly. The access token is also provided immediately and there's no authorization code which must be exchanged for an access token.
            </p>
        </div>
    </div>
    <div class="row">
        <div class="span4">
            <div class="well">
                <strong class="pull-right">Or</strong>
                <p>No user input is needed in this end</p>
                <asp:Button ID="btnStartAuthorization" runat="server"  Text="Start authorization flow" CssClass="btn btn-primary" OnClick="btnStartAuthorization_Click" />
            </div>
        </div>
        <div class="span4">
            <div class="well">
                <strong class="pull-right">Or</strong>
                <label for="appid">App Id</label>
                <asp:TextBox ID="txtAppId" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAppId" runat="server" ControlToValidate="txtAppId" Display="Dynamic" ErrorMessage="Required" CssClass="text-error" ValidationGroup="AppAouth"></asp:RequiredFieldValidator>
                <label for="apptoken">App token</label>
                <asp:TextBox ID="txtAppToken" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAppToken" runat="server" ControlToValidate="txtAppToken" Display="Dynamic" ErrorMessage="Required" ValidationGroup="AppAouth" CssClass="text-error"></asp:RequiredFieldValidator>
                <asp:Button ID="btnConnetAsApp" runat="server"  Text="Connect as App" CssClass="btn btn-primary" OnClick="btnConnetAsApp_Click" ValidationGroup="AppAouth" />
            </div>
        </div>
        <div class="span4">
            <div class="well">
                <label for="username">Username</label>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Required" ValidationGroup="UserAouth" CssClass="text-error"></asp:RequiredFieldValidator>
                <label for="password">Password</label>
               <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
               <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Required" ValidationGroup="UserAouth" CssClass="text-error"></asp:RequiredFieldValidator>
               <asp:Button ID="btnConnectWithUsername" runat="server"  Text="Connect with Username &amp; Password" CssClass="btn btn-primary" OnClick="btnConnectWithUsername_Click" ValidationGroup="UserAouth"/>
            </div>
        </div>
        <asp:Label ID="lblErrror" runat="server" CssClass="error"></asp:Label>
        <asp:Label ID="lblErrorDescription" runat="server" CssClass ="error"></asp:Label>
    </div>
</asp:Content>
