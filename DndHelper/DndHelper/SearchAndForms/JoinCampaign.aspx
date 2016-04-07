<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JoinCampaign.aspx.cs" Inherits="DndHelper.SearchAndForms.JoinCampaign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Join <%: cname %>
    </h2>
    <div class="row">
        <div class="col-md-2">
            <b> Character to use: </b>
        </div>
        
        <div class="col-md-3">
            <asp:DropDownList ID="CharacterList" runat="server" width="100%">
            </asp:DropDownList>
        </div>
      
        <div class="col-md-2">
            <asp:Button ID="Join" class="btn btn-warning-outline" runat="server" Text="Join" onclick="joinGame"/>
        </div>

        <asp:Label runat="server" id="Message" Text=""/>

    </div>
</asp:Content>
