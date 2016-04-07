<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateCampaign.aspx.cs" Inherits="DndHelper.SearchAndForms.CreateCampaign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Campaign </h2>

    <div>
        <asp:Label ID="Message" runat="server" Wrap="false" Visible="false"></asp:Label>
    </div>

    <table class="table table-condensed table-borderless">
        <tr>
            <td>
               <b> Campaign Name</b>:
            </td>
            <td>
                <asp:TextBox ID="CampaignName" runat="server" Wrap="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <b> Campaign Description</b>:
            </td>
            <td>
                <asp:TextBox ID="CampaignDescription" runat="server" TextMode="MultiLine" Width=100%></asp:TextBox>
            </td>
        </tr>

    </table>

    <div>
         <asp:Button ID="Execute"  class="btn btn-warning-outline" runat="server" Text="Create" onclick="executeCampaignAction"/>
    </div>
</asp:Content>
