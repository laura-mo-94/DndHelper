<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RunCampaign.aspx.cs" Inherits="DndHelper.SearchAndForms.RunCampaign" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">   
    <h1 class="site-title">
        <%: getCurrentCampaign().CampaignName %>
    </h1>   

    <div class="row">
        <asp:Button ID="Save"  class="btn btn-warning-outline" runat="server" OnClick ="SaveProgress" Text="Save"/>
    </div>
    <br />

    <div class="row">
            <asp:UpdatePanel ID="CharacterStatsPanel" runat="server">
                <ContentTemplate>
                        <asp:ListView ID="CharacterStats" runat="server" DataKeyNames="CharacterID" ItemType="DndHelper.Models.Character">      
                            <ItemTemplate>
                                <table class="table table-condensed table-borderless">
                                    <tr class="success">
                                        <th>
                                            <p>
                                                <h5>
                                                    <b> <%#: Item.CharacterName %> </b>
                                                </h5>
                                            </p>
                                            <p>
                                                <small>
                                                    <%#: Item.Class %>
                                                </small>
                                                &nbsp &nbsp &nbsp &nbsp &nbsp

                                                <small>
                                                    <%#: Item.Specie %>
                                                </small>
                                            </p>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                             <b>Level</b>:
                                             &nbsp &nbsp
                                            <asp:Button ID="LevelDown" class="btn btn-warning-outline" value=-1 field="Level" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                            &nbsp &nbsp 
                                            <%#:Item.Level %>
                                            &nbsp &nbsp
                                            <asp:Button ID="LevelUp" class="btn btn-warning-outline" value=1 field="Level" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <b>Health</b>:
                                                 &nbsp &nbsp &nbsp
                                                <asp:Button ID="HealthDown" class="btn btn-warning-outline" value=-1 field="Health" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                 &nbsp &nbsp 
                                                <%#: Item.CurrentHealth %>
                                                &nbsp &nbsp
                                                <asp:Button ID="HealthUp" class="btn btn-warning-outline" value=1 field="Health" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/>

                                                &nbsp &nbsp &nbsp / &nbsp &nbsp &nbsp 
                                                <asp:Button ID="MaxHealthDown" class="btn btn-warning-outline" value=-1 field="MaxHealth" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                 &nbsp &nbsp 
                                                <%#: Item.MaxHealth %>
                                                &nbsp &nbsp
                                                <asp:Button ID="MaxHealthUp" class="btn btn-warning-outline" value=1 field="MaxHealth" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <b>Status</b>:
                                                <%#: Item.Status %>
                                                &nbsp &nbsp &nbsp &nbsp
                                                 <asp:TextBox ID="NewStatus" owner=<%#:Item.CharacterID %> runat="server" />
                                                <asp:Button ID="ChangeStatus" class="btn btn-warning-outline" owner=<%#:Item.CharacterID %> field="Status" runat="server" Text="Change" OnClick="ChangeStats" />
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp 
                                            Base &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp Modifiers
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                 <b> Strength </b>
                                                 &nbsp &nbsp &nbsp
                                                    <asp:Button ID="BSDown" class="btn btn-warning-outline" value=-1 field="BaseStrength" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                 &nbsp &nbsp 
                                                 <%#:Item.BaseStrength %>
                                                &nbsp &nbsp
                                                <asp:Button ID="BSUp" class="btn btn-warning-outline" value=1 field="BaseStrength" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 

                                                &nbsp &nbsp + &nbsp  &nbsp
                                                    <asp:Button ID="MSDown" class="btn btn-warning-outline" value=-1 field="StrengthModifier" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                &nbsp &nbsp 
                                                 <%#:Item.StrengthModifier %>
                                                &nbsp &nbsp
                                                <asp:Button ID="MSUp" class="btn btn-warning-outline" value=1 field="StrengthModifier" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 
                                            
                                                &nbsp &nbsp = <%#: Item.BaseStrength + Item.StrengthModifier %>
                                            </p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <p>
                                                 <b> Dexterity </b>
                                                 &nbsp &nbsp &nbsp
                                                    <asp:Button ID="BDDown" class="btn btn-warning-outline" value=-1 field="BaseDexterity" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                 &nbsp &nbsp 
                                                 <%#:Item.BaseDexterity %>
                                                &nbsp &nbsp
                                                <asp:Button ID="BDUp" class="btn btn-warning-outline" value=1 field="BaseDexterity" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 

                                                &nbsp &nbsp + &nbsp  &nbsp
                                                    <asp:Button ID="MDDown" class="btn btn-warning-outline" value=-1 field="DexterityModifier" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                &nbsp &nbsp 
                                                 <%#:Item.DexterityModifier %>
                                                &nbsp &nbsp
                                                <asp:Button ID="MDUp" class="btn btn-warning-outline" value=1 field="DexterityModifier" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 
                                            
                                                &nbsp &nbsp = <%#: Item.BaseDexterity + Item.DexterityModifier %>
                                            </p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <p>
                                                 <b> Constitution </b>
                                                 &nbsp &nbsp &nbsp
                                                    <asp:Button ID="BCDown" class="btn btn-warning-outline" value=-1 field="BaseConstitution" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                 &nbsp &nbsp 
                                                 <%#:Item.BaseConstitution %>
                                                &nbsp &nbsp
                                                <asp:Button ID="BCUp" class="btn btn-warning-outline" value=1 field="BaseConstitution" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 

                                                &nbsp &nbsp + &nbsp  &nbsp
                                                    <asp:Button ID="MCDown" class="btn btn-warning-outline" value=-1 field="ConstitutionModifier" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                &nbsp &nbsp 
                                                 <%#:Item.ConstitutionModifier %>
                                                &nbsp &nbsp
                                                <asp:Button ID="MCUp" class="btn btn-warning-outline" value=1 field="ConstitutionModifier" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 
                                            
                                                &nbsp &nbsp = <%#: Item.BaseConstitution + Item.ConstitutionModifier %>
                                            </p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <p>
                                                 <b> Intelligence </b>
                                                 &nbsp &nbsp &nbsp
                                                    <asp:Button ID="BIDown" class="btn btn-warning-outline" value=-1 field="BaseIntelligence" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                 &nbsp &nbsp 
                                                 <%#:Item.BaseIntelligence %>
                                                &nbsp &nbsp
                                                <asp:Button ID="BIUp" class="btn btn-warning-outline" value=1 field="BaseIntelligence" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 

                                                &nbsp &nbsp + &nbsp  &nbsp
                                                    <asp:Button ID="MIDown" class="btn btn-warning-outline" value=-1 field="IntelligenceModifier" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                &nbsp &nbsp 
                                                 <%#:Item.IntelligenceModifier %>
                                                &nbsp &nbsp
                                                <asp:Button ID="MIUp" class="btn btn-warning-outline" value=1 field="IntelligenceModifier" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 
                                            
                                                &nbsp &nbsp = <%#:Item.BaseIntelligence + Item.IntelligenceModifier %>
                                            </p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <p>
                                                 <b> Wisdom </b>
                                                 &nbsp &nbsp &nbsp
                                                    <asp:Button ID="BWDown" class="btn btn-warning-outline" value=-1 field="BaseWisdom" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                 &nbsp &nbsp 
                                                 <%#:Item.BaseWisdom %>
                                                &nbsp &nbsp
                                                <asp:Button ID="BWUp" class="btn btn-warning-outline" value=1 field="BaseWisdom" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 

                                                &nbsp &nbsp + &nbsp  &nbsp
                                                    <asp:Button ID="MWDown" class="btn btn-warning-outline" value=-1 field="WisdomModifier" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                &nbsp &nbsp 
                                                 <%#:Item.WisdomModifier %>
                                                &nbsp &nbsp
                                                <asp:Button ID="MWUp" class="btn btn-warning-outline" value=1 field="WisdomModifier" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 
                                            
                                                &nbsp &nbsp = <%#: Item.BaseWisdom + Item.WisdomModifier %>
                                            </p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <p>
                                                 <b> Charisma </b>
                                                 &nbsp &nbsp &nbsp
                                                    <asp:Button ID="BChDown" class="btn btn-warning-outline" value=-1 field="BaseCharisma" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                 &nbsp &nbsp 
                                                 <%#:Item.BaseCharisma %>
                                                &nbsp &nbsp
                                                <asp:Button ID="BChUp" class="btn btn-warning-outline" value=1 field="BaseCharisma" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 

                                                &nbsp &nbsp + &nbsp  &nbsp
                                                    <asp:Button ID="MChDown" class="btn btn-warning-outline" value=-1 field="CharismaModifier" owner=<%#:Item.CharacterID %> runat="server" Text="-" onclick="ChangeStats"/>
                                                &nbsp &nbsp 
                                                 <%#:Item.StrengthModifier %>
                                                &nbsp &nbsp
                                                <asp:Button ID="MChUp" class="btn btn-warning-outline" value=1 field="CharismaModifier" owner=<%#:Item.CharacterID %>  runat="server" Text="+" onclick="ChangeStats"/> 
                                            
                                                &nbsp &nbsp = <%#: Item.BaseCharisma + Item.CharismaModifier %>
                                            </p>
                                        </td>
                                    </tr>
                               </table>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <tr>
                                    <td>
                                         <p style="text-align:center"> No Characters in this campaign </p>
                                    </td>
                                </tr>
                            </EmptyDataTemplate>
                        </asp:ListView>
                </ContentTemplate>
               
            </asp:UpdatePanel>
    </div>
</asp:Content>
