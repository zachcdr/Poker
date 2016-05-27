<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Poker.aspx.cs" Inherits="Poker.Poker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>iTrellis Poker</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Welcome To iTrellis Poker Validator</h1>
            <div>
                <h4>INFO</h4>
                <h5>iTrellis Poker Validator is here to determine which hand, between two players, is better.<br />
                    Enter the Players' names below (alphanumeric and up to 25 characters) along with selecting each Player's repective hand from the lists.</h5>
            </div>

            <div id="divErrorMessage" style="color:red">
                <b><asp:Literal ID="litErrorMessage" runat="server" Visible="false" /></b>
            </div>

            <asp:Panel ID="pnlPlayers" runat="server"> 
                <table>
                    <tr>
                        <td><b>Enter Player One Name:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbP1Name" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>Enter Player One Cards:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer1Card1Num" runat="server">
                                <asp:ListItem Text="Card #1 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer1Card1Suit" runat="server">
                                <asp:ListItem Text="Card #1 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer1Card2Num" runat="server">
                                <asp:ListItem Text="Card #2 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer1Card2Suit" runat="server">
                                <asp:ListItem Text="Card #2 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer1Card3Num" runat="server">
                                <asp:ListItem Text="Card #3 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer1Card3Suit" runat="server">
                                <asp:ListItem Text="Card #3 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer1Card4Num" runat="server">
                                <asp:ListItem Text="Card #4 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer1Card4Suit" runat="server">
                                <asp:ListItem Text="Card #4 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer1Card5Num" runat="server">
                                <asp:ListItem Text="Card #5 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer1Card5Suit" runat="server">
                                <asp:ListItem Text="Card #5 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Enter Player Two Name:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="tbP2Name" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td><b>Enter Player Two Cards:</b>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer2Card1Num" runat="server">
                                <asp:ListItem Text="Card #1 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer2Card1Suit" runat="server">
                                <asp:ListItem Text="Card #1 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer2Card2Num" runat="server">
                                <asp:ListItem Text="Card #2 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer2Card2Suit" runat="server">
                                <asp:ListItem Text="Card #2 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer2Card3Num" runat="server">
                                <asp:ListItem Text="Card #3 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer2Card3Suit" runat="server">
                                <asp:ListItem Text="Card #3 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer2Card4Num" runat="server">
                                <asp:ListItem Text="Card #4 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer2Card4Suit" runat="server">
                                <asp:ListItem Text="Card #4 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPlayer2Card5Num" runat="server">
                                <asp:ListItem Text="Card #5 Face Value" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlPlayer2Card5Suit" runat="server">
                                <asp:ListItem Text="Card #5 Suit" Value="-1" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnWhoWon" runat="server" OnClick="btnWhoWonClick" Text="Determine Winner" />
            </asp:Panel>
            <asp:Panel ID="pnlWinner" runat="server" Visible="false">
                <b>The Winner Is:
                    <asp:Label ID="lblWinner" runat="server" />
                </b>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
