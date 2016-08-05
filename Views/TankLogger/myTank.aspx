<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/tanklogger.Master" CodeBehind="myTank.aspx.vb" MaintainScrollPositionOnPostback="true" Inherits="tanklogger.myTank" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <asp:PlaceHolder id="MetaPlaceHolder" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="formstyle">
            <tr>
                <td  class="HeaderBars">
                    <asp:Label ID="lblTankID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblTankName" runat="server" Text="My Tank"></asp:Label>&nbsp Dashboard
                </td>
            </tr>
            <tr>
                <td>
                    <table class="formstyle">
                        <tr >
                            <td style="border:2px solid #01619d; width:50%; text-align:center; vertical-align:middle;">
                                <table style="width:100%;">
                                    <tr>
                                        <td colspan="2" style="text-align:center; " >
                                            <asp:Image ID="imgTank" runat="server" Width="400px" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style=" text-align:left;">Percent Full: <asp:Label ID="lblPctfull" runat="server"></asp:Label></td>
                                        <td style=" text-align:right;">Remaining Gallons: <asp:Label ID="lblRemvol" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style=" text-align:left;">Total Volume: <asp:Label ID="lblTotvol" runat="server"></asp:Label></td>
                                        <td style=" text-align:right;">Sample Time: <asp:Label ID="lblAge" runat="server"></asp:Label></td>
                                    </tr>
                                    <%If Not oSite.OnSite Then%>
                                    <tr id="rowSpot">
                                            <td style=" text-align:left;">SPOT GPS: <asp:Label ID="lblSPOT_NAME" runat="server"></asp:Label></td>
                                            <td style=" text-align:right;">Tank Coordinates: <asp:linkButton ID="btnMap" runat="server"/>&nbsp;&nbsp; </td>
                                        
                                    </tr>
                                    <%End If%>
                                    <tr>
                                            <td style=" text-align:left;">&nbsp;</td>
                                            <td style=" text-align:right;"><asp:linkButton ID="btnWX" runat="server" text="Weather" Visible="false" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="border:2px solid #01619d; width:50%; text-align:center; vertical-align:middle;">
                                <table style="width:100%;" >
                                    <tr>
                                        <td style="text-align:left; vertical-align:middle;">
                                            <u>Alarms</u></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left; vertical-align:middle;">
                                            <asp:GridView ID="grdAlarms" runat="server" AllowPaging="true" pagesize="10" GridLines="None" 
                                            AutoGenerateColumns="False"  BorderWidth="0px"  Width="100%" 
                                            DataKeyNames="slID"  CssClass="TableDetails">
                                            <HeaderStyle CssClass="TableHeader" HorizontalAlign="Left" ></HeaderStyle>
                                            <Columns>
                                                <asp:ImageField DataImageUrlField="warnimage" HeaderText="" ItemStyle-Width="20"></asp:ImageField>
                                                <asp:ButtonField CommandName="cmdSelected" HeaderText="Description" DataTextField="slDescription" HeaderStyle-HorizontalAlign="left"><ItemStyle  ForeColor="#003366" HorizontalAlign="left"  /></asp:ButtonField>
                                                <asp:BoundField DataField="slSensType" HeaderText="Type" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                                                <asp:BoundField DataField="slSensValue" HeaderText="Limit" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                                                <asp:BoundField DataField="slSensEmail" HeaderText="eMail" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                                                <asp:BoundField DataField="slActive" HeaderStyle-CssClass="hidden" ItemStyle-CssClass=""></asp:BoundField>
                                                <asp:BoundField DataField="slGUID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"></asp:BoundField>
                                                <asp:ButtonField CommandName="cmdDelete" HeaderText="Delete" CausesValidation="true" ButtonType="Image" ImageUrl="images/delrec_tiny.png" />
                                            </Columns>
                                                <PagerStyle CssClass="PagerLink" HorizontalAlign="left" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left; vertical-align:middle;">
                                            <br />
                                            Add/Modify an Alarm:<br />
                                            <asp:Label ID="lblAlID" runat="server" Visible="false"></asp:Label>
                                            Desc:<asp:TextBox ID="tbAlDesc" runat="server" Width="90"></asp:TextBox>
                                            Type:<asp:DropDownList ID="ddAlType" runat="server">
                                                    <asp:ListItem Value="Low">Low</asp:ListItem>
                                                    <asp:ListItem Value="High">High</asp:ListItem>
                                                    </asp:DropDownList>
                                            Limit:<asp:TextBox ID="tbAlLimit" runat="server" Width="40"></asp:TextBox>
                                            Email:<asp:TextBox ID="tbAlEmail" runat="server" Width="90"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:left; vertical-align:middle;">
                                            <asp:Button ID="btnAdd" runat="server" text="Save" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align:center;">
                                <table class="formstyle" style="margin:auto;" >
                                    <tr>
                                        <td style="text-align:center; border:2px solid #01619d">
                                            <asp:RadioButtonList ID="rbChartMode" runat="server"
                                                AutoPostBack="true" style="margin:auto;"
                                                RepeatDirection="Horizontal" Width="50%">
                                                <asp:ListItem Value="R" Selected="True">Real-Time</asp:ListItem>
                                                <asp:ListItem Value="H">Hourly (7d)</asp:ListItem>
                                                <asp:ListItem Value="D">Daily (45d)</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  style="text-align:center;border:2px solid #01619d">
                                            <asp:Label ID="lblnoVdata" runat="server" Visible="false">No Volume Data for this time range.</asp:Label>
                                            <div id="vol_chart_div" >
                                                <asp:Chart ID="RangeChart" runat="server" Height="200px" Width="950px">
                                                    <Series>
                                                        <asp:Series Name="RangeSeries" ChartType="Line" ></asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="RangeArea1"></asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>    
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  style="text-align:center;border:2px solid #01619d">
                                            <asp:Label ID="lblnoTdata" runat="server" Visible="false">No Temperature Data for this time range.</asp:Label>
                                            <div id="temp_chart_div" >
                                                <asp:Chart ID="TempChart" runat="server" Height="200px" Width="950px">
                                                    <Series>
                                                        <asp:Series Name="TempSeries1" ChartType="Line"  ></asp:Series>
                                                    </Series>
                                                    <Series>
                                                        <asp:Series Name="TempSeries2" ChartType="Line" ></asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="TemptArea1"></asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  style="text-align:center;border:2px solid #01619d">
                                            <asp:Label ID="lblnoPdata" runat="server" Visible="false">No Voltage Data for this time range.</asp:Label>
                                            <div id="volts_chart_div" >
                                                <asp:Chart ID="VoltChart" runat="server" Height="200px" Width="950px">
                                                    <Series>
                                                        <asp:Series Name="VoltSeries" ChartType="Line" ></asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="VoltArea1"></asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>    
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td  colspan="2" style="text-align:center;">
                                <asp:Button ID="btnSettings" runat="server" Text="Settings" />
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
