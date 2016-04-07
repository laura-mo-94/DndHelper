<%@ Page Title="D&D Helper" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="DndHelper.Account.UserPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <h1 class="site-title"><%: Title %></h1>
        <h2>
              <%: ownsPage() ? "Welcome!" : "" %> <%: getPagePlayer().PlayerName %>
        </h2>
        
        <div class="row">
            <div class="col-md-4">
                <table class="table table-condensed table-borderless">
                    <tr class="success">
                        <th>
                            <h5> Campaigns</h5>
                        </th> 
                   </tr>
                        <asp:ListView ID="CampaignList" runat="server"  DataKeyNames="CampaignID" GroupItemCount="1" ItemType="DndHelper.Models.Campaign" SelectMethod ="LoadCampaigns">
                        <GroupTemplate>
                            <tr id="CampaignPlaceHolderContainer" runat="server">
                                <td id="itemPlaceholder" runat="server"></td>
                            </tr>
                        </GroupTemplate>           
                        <ItemTemplate>
                            <tr class="active">  
                                <td style="vertical-align: top; text-align:left;">
                                    <b>
                                        <h6>
                                            <b>
                                                 <a href=<%#: getURL("../Account/CampaignPage.aspx",Item.CampaignId, getPagePlayer().PlayerName)%>><%#:Item.CampaignName %></a>
                                            </b>
                                        </h6>
                                    </b>
                                    <asp:HiddenField runat="server" ID="CampaignID" Value="<%# Item.CampaignId %>"/>
                                    <tr>
                                        <asp:ListView ID="PlayersInCampaign" runat="server" GroupItemCount="20" DataKeyNames="PlayerID" ItemType="DndHelper.Models.Player" SelectMethod ="LoadPlayers">
                                            <GroupTemplate>
                                                <td>
                                                    &nbsp &nbsp &nbsp
                                                    <div style="height: 20px; overflow:auto;" id="itemPlaceholder" runat="server">
                                                    </div>
                                                </td>
                                            </GroupTemplate>            
                                            <ItemTemplate>
                                                <small><a href=<%#: getURL("../Account/UserPage.aspx", Item.PlayerId, Item.PlayerName) %>><%#:Item.PlayerName %></a></small>
                                            </ItemTemplate>
                                            <ItemSeparatorTemplate> &nbsp | &nbsp </ItemSeparatorTemplate>
                                        </asp:ListView>
                                    </tr>
                                </td>
                            </tr>
                        </ItemTemplate>

                        <EmptyDataTemplate>
                            <td>
                                 No campaigns yet.
                            </td>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <tr>
                        <td>
                            <p>
                                <a href=<%: (!ownsPage()) ? "#" : getURL("../SearchAndForms/SearchCampaign.aspx", getPagePlayer().PlayerId, getPagePlayer().PlayerName) %>>
                                     <%: (!ownsPage()) ? "" : "+ Find a campaign" %>
                                </a>
                                <br/>
                                <a href=<%: (!ownsPage()) ? "#" : getURL("../SearchAndForms/CreateCampaign.aspx", getPagePlayer().PlayerId, getPagePlayer().PlayerName) %>>
                                     <%: (!ownsPage()) ? "" : "+ Create a campaign" %>
                                </a>
                            </p>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="col-md-1">

            </div>

             <div class="col-md-4">
                <table class="table table-condensed table-borderless">
                    <tr class="info">
                        <th>
                            <h5>Characters</h5>
                        </th> 
                   </tr>
                        <asp:ListView ID="ListView1" runat="server"  DataKeyNames="CharacterID" GroupItemCount="1" ItemType="DndHelper.Models.Character" SelectMethod ="LoadCharacters">
                        <GroupTemplate>
                            <tr id="CharacterPlaceHolderContainer" runat="server">
                                <td id="itemPlaceholder" runat="server"></td>
                            </tr>
                        </GroupTemplate>           
                        <ItemTemplate>
                            <tr class="active">  
                                <td style="vertical-align: top; text-align:left;">
                                    <h6>
                                        <a  href=<%#: getURL("../Account/CharacterPage.aspx", Item.CharacterID, getCharacterOwner(Item.PlayerID)) %>><%#:Item.CharacterName %></a>
                                    </h6>
                                    <p>
                                        <%#:Item.Class %>
                                        &nbsp &nbsp
                                        <%#:Item.Specie %>
                                        &nbsp &nbsp
                                        <b>
                                            <%#:Item.Status %>
                                        </b>
                                    </p>
                                </td>
                            </tr>
                        </ItemTemplate>

                        <EmptyDataTemplate>
                             <td>
                                 No characters yet!
                            </td>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <tr>
                        <td>
                            <p>
                                 <a href=<%: (!ownsPage()) ? "#" : getURL("../SearchAndForms/CreateCharacter.aspx", getPagePlayer().PlayerId, getPagePlayer().PlayerName) %>>
                                     <%: (!ownsPage()) ? "" : "+ Create a character" %>
                                </a>
                            </p>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        
</asp:Content>
