<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateCharacter.aspx.cs" Inherits="DndHelper.SearchAndForms.CreateCharacter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2> Character </h2>

    <div>
        <asp:Label ID="Message" runat="server" Wrap="false" Visible="false"></asp:Label>
    </div>

    <table id="general" class="table table-condensed table-borderless">
        <tr>
            <td>
               <b> Character Name</b>:
            </td>
            <td>
                <asp:TextBox ID="CharacterName" runat="server" Wrap="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <b>Specie</b>:
            </td>
            <td>
                <asp:TextBox ID="CharacterSpecie" runat="server" Wrap="false"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>
                <b>Class</b>:
            </td>
            <td>
                <asp:TextBox ID="CharacterClass" runat="server" Wrap="false"></asp:TextBox>
            </td>
        </tr>

         <tr>
            <td>
                <b>Max Health</b>:
            </td>
            <td>
                <asp:TextBox ID="CharacterHealth" runat="server" Wrap="false"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table id="stats" class="table table-condensed table-borderless">
        <tr>
            <td>
                <b>
                    Attack
                </b>:
            </td>
            <td>
                Base
            </td>
            <td>
                <asp:TextBox ID="CBaseAttack" runat="server" />
            </td>
            <td>
                Modifier
            </td>
            <td>
                <asp:TextBox ID="CAttackModifier" runat="server" />
            </td>
        </tr>
         
        <tr>
            <td>
                <b>
                    Dexterity
                </b>:
            </td>
            <td>
                Base
            </td>
            <td>
                <asp:TextBox ID="CBaseDexterity" runat="server" />
            </td>
            <td>
                Modifier
            </td>
            <td>
                <asp:TextBox ID="CDexterityModifier" runat="server" />
            </td>
        </tr>
         
        <tr>
            <td>
                <b>
                    Constitution
                </b>:
            </td>
            <td>
                Base
            </td>
            <td>
                <asp:TextBox ID="CBaseConstitution" runat="server" />
            </td>
            <td>
                Modifier
            </td>
            <td>
                <asp:TextBox ID="CConstitutionModifier" runat="server" />
            </td>
        </tr>

          <tr>
            <td>
                <b>
                    Intelligence
                </b>:
            </td>
            <td>
                Base
            </td>
            <td>
                <asp:TextBox ID="CBaseIntelligence" runat="server" />
            </td>
            <td>
                Modifier
            </td>
            <td>
                <asp:TextBox ID="CIntelligenceModifier" runat="server" />
            </td>
        </tr>

          <tr>
            <td>
                <b>
                    Wisdom
                </b>:
            </td>
            <td>
                Base
            </td>
            <td>
                <asp:TextBox ID="CBaseWisdom" runat="server" />
            </td>
            <td>
                Modifier
            </td>
            <td>
                <asp:TextBox ID="CWisdomModifier" runat="server" />
            </td>
        </tr>

          <tr>
            <td>
                <b>
                    Charisma
                </b>:
            </td>
            <td>
                Base
            </td>
            <td>
                <asp:TextBox ID="CBaseCharisma" runat="server" />
            </td>
            <td>
                Modifier
            </td>
            <td>
                <asp:TextBox ID="CCharismaModifier" runat="server" />
            </td>
        </tr>
    </table>

    <div>
         <asp:Button ID="Execute"  class="btn btn-warning-outline" runat="server" Text="Create" onclick="executeCharacter"/>
    </div>
</asp:Content>
