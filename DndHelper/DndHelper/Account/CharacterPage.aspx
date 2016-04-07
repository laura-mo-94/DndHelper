<%@ Page Title="D&D Helper" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CharacterPage.aspx.cs" Inherits="DndHelper.Account.CharacterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" style="vertical-align:bottom">
        <div class="col-md-5">
             <h1 class="site-title" >
                 <a href=<%: getURL("/Account/UserPage.aspx", getPageOwner().PlayerId) %> style="text-decoration:none">
                     <%: getCurrentCharacter().CharacterName %>
                 </a>
             </h1>  
        </div>
    </div>

    <div class="row">
        <div class="col-md-1">
             <asp:Button class="btn btn-warning-outline" ID="Edit" runat="server" Text="Edit" onclick="editCharacter"/>
        </div>
        <div class="col-md-1">
            <asp:Button  class="btn btn-warning-outline" ID="Delete" runat="server" Text="Delete" onclick="deleteCharacter"/>
        </div>
    </div>
      
    <br/>

     <div class="row">
        <div class="col-md-5">
            <table class="table table-borderless table-condensed">
                <tr class="success">
                    <th>
                        <h5>
                            Basic Information
                        </h5>
                    </th>
                </tr>
                <tr>
                    <td>
                        <p class="table-element">
                            <b>Specie</b>: <%: getCurrentCharacter().Specie %>
                            <br />
                            <b>Class</b>: <%: getCurrentCharacter().Class %>
                             <br />
                            <b>Level</b>: <%: getCurrentCharacter().Level %>
                            <br />
                            <b>HP</b>: <%: getCurrentCharacter().CurrentHealth %> / <%: getCurrentCharacter().MaxHealth %>
                            <br />
                            <b>Status</b>: <%: getCurrentCharacter().Status %>
                        </p>
                    </td>
                </tr>
            </table>
        </div>

         <div class="col-md-1">
         </div>

         <div class="col-md-5">
            <table class="table table-borderless table-condensed">
                <tr class="warning">
                    <th>
                        <h5>
                            Active Campaigns
                        </h5>
                    </th>
                </tr>
                <asp:ListView ID="ActiveCampaigns" runat="server" GroupItemCount="20" DataKeyNames="CampaignID" ItemType="DndHelper.Models.Campaign" SelectMethod ="getActiveCampaigns">
                    <GroupTemplate>
                        <tr>
                            <td>
                                <div style="height: 20px; overflow:auto;" id="itemPlaceholder" runat="server">
                                </div>
                            </td>
                        </tr>
                        
                    </GroupTemplate>            
                    <ItemTemplate>
                        <a href=<%#: getURL("../Account/CampaignPage.aspx", Item.CampaignId)%>><%#:Item.CampaignName %></a>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <tr>
                            <td>
                                No campaigns yet!
                            </td>
                        </tr>
                    </EmptyDataTemplate>
               </asp:ListView>
            </table>
        </div>
    </div>

    <div id="CharacterStats" style="width:100%; height:400px;">
        <script type="text/javascript">
            function loadChart() {
               
                var test = 10;
                window.chart = new Highcharts.Chart({
                    chart: {
                        renderTo: 'CharacterStats',
                        polar: true,
                        type: 'line'
                    },

                    title: {
                        text: 'Stats',
                    },

                    pane: {
                        size: '80%'
                    },

                    xAxis: {
                        categories: ['Strength', 'Dexterity', 'Constitution', 'Intelligence', 'Wisdom', 'Charisma'],
                        tickmarkPlacement: 'on',
                        lineWidth: 0
                    },

                    yAxis: {
                        gridLineInterpolation: 'polygon',
                        lineWidth: 0,
                        min: 0,
                    },

                    tooltip: {
                        shared: true
                    },

                    series: [{
                        name: 'With Modifiers',
                        data: modifiedStats,
                        type: 'area',
                        pointPlacement: 'on'
                    },
                    {
                        name: 'Base Stats',
                        data: baseStats,
                        type: 'area',
                        pointPlacement: 'on'
                    }]
                 });
            };
        </script>
    </div>
</asp:Content>
