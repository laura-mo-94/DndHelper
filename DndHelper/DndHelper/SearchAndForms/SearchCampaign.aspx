<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchCampaign.aspx.cs" Inherits="DndHelper.SearchAndForms.SearchCampaign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>
        Find Campaigns!
    </h3>

    <div class="row">
        <div class="col-md-2">
            <asp:DropDownList ID="Mode" runat="server" width="100%">
                <asp:ListItem>By Name</asp:ListItem>
                <asp:ListItem>By Dungeon Master</asp:ListItem>
                <asp:ListItem>By Participant</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-8">
                <asp:TextBox ID="SearchBox" runat="server" BorderColor="#3290DA" BorderStyle="Solid" BorderWidth="2px" Wrap="False" width="100%" type="Search"></asp:TextBox>
        </div>
        <div class="col-md-2">
            <asp:Button ID="Search" class="btn btn-warning-outline" runat="server" Text="Search" onclick="getSearchResults"/>
        </div>
    </div>

    <br/>

    <div class="row">
            <asp:UpdatePanel ID="CampaignSearchResults" runat="server">
                <ContentTemplate>
                    <asp:Label  ID="searchSummary" runat="server" Text="<%# resultSummaryString %>"/>
                    <table class="table table-condensed table-borderless">
                        <asp:ListView ID="SearchResults" runat="server" DataKeyNames="CampaignID" ItemType="DndHelper.Models.Campaign" SelectMethod="getResults"> 
                            <GroupTemplate>
                                <tr id="CampaignPlaceHolderContainer" runat="server">
                                    <td id="itemPlaceholder" runat="server"></td>
                                </tr>
                            </GroupTemplate>     
                            <ItemTemplate>
                               <tr>
                                  <td>
                                    <a href=<%#: getURL("../Account/CampaignPage.aspx",Item.CampaignId, getPagePlayer().PlayerName)%>><%#:Item.CampaignName %></a>
                                  </td>
                               </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <tr>
                                    <td>
                                         <p style="text-align:center"> No results </p>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                           
                            </asp:ListView>
                     </table>
                    
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Search" />
                </Triggers>
            </asp:UpdatePanel>
    </div>
    
</asp:Content>
