<%@ Page Title="D&D Helper" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CampaignPage.aspx.cs" Inherits="DndHelper.Account.CampaignPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1 class="site-title">
         <a href=<%: getURL("/Account/UserPage.aspx", getPageOwner().PlayerId) %> style="text-decoration:none">
            <%: getCurrentCampaign().CampaignName%>    
         </a>
     </h1>  
    <h3>
        <%: getPageOwner().PlayerName %>
    </h3>
      
     <div class="row">
        <div class="col-md-4">
            <table class="table table-borderless table-condensed">
                <tr class="success">
                    <th>
                        <h5>
                            General
                        </h5>
                    </th>
                </tr>
                <tr>
                    <td>
                        <b>Dungeon Master</b>: <a href=<%: getURL("/Account/UserPage.aspx", getDM().PlayerId, getDM().PlayerName)%>><%: getDM().PlayerName %></a>
                    </td>
                </tr>
                <tr>
                    <asp:ListView ID="PlayersInCampaign" runat="server" GroupItemCount="20" DataKeyNames="PlayerID" ItemType="DndHelper.Models.Player" SelectMethod ="getParticipants">
                    <GroupTemplate>
                        <td>
                            <div style="height: 20px; overflow:auto;" id="itemPlaceholder" runat="server">
                            </div>
                        </td>
                    </GroupTemplate>            
                    <ItemTemplate>
                        <a href=<%#: getURL("/Account/UserPage.aspx", Item.PlayerId, Item.PlayerName)%>><%#:Item.PlayerName %></a>
                    </ItemTemplate>
                    <ItemSeparatorTemplate> &nbsp | &nbsp </ItemSeparatorTemplate>
                    </asp:ListView>
                </tr>
                <tr>
                    <td runat="server" id="activeChar">
                        <b> Active Character</b>: <a href=<%: (getActiveCharacter() == null) ? "" : getURL("../Account/CharacterPage.aspx", getActiveCharacter().CharacterID) %>> 
                            <%: (getActiveCharacter() == null) ? "Dungeon Master" : getActiveCharacter().CharacterName %></a>
                    </td>
                </tr>
            </table>
        </div>

         <div class="col-md-8">
             <h3 class="description"> 
                 <%:getCurrentCampaign().Description %>
             </h3>
         </div>
    </div>

    <div class="row">
         <div class="col-md-3">
             <asp:Button ID="Join" class="btn btn-warning-outline" runat="server" Text="Join" onclick="joinCampaign"/>
             <asp:Button ID="Edit" class="btn btn-success-outline" runat="server" Text="Edit" onclick="editCampaign"/>
             <asp:Button ID="Delete" class="btn btn-dangeroutline" runat="server" Text="Delete" onclick="deleteCampaign"/>
             <asp:Button ID="Leave" class="btn btn-info-outline" runat="server" Text="Leave" onclick="leaveCampaign"/>
            <asp:Button ID="Start" class="btn btn-secondary-outline" runat="server" Text="Start" onclick="startCampaign"/>
        </div>

    </div>
</asp:Content>
