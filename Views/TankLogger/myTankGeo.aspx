<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="myTankGeo.aspx.vb" Inherits="tanklogger.tankgeo" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <style type="text/css">
      html { height: 100% }
      body { height: 100%; margin: 0; padding: 0 }
      #map-canvas { height: 100% }
    </style>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAtqqdYG0XxjlNyEhO1hLM6Lk93qW3HQiY&sensor=false"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        function InitializeMap() {
            var latlng = new google.maps.LatLng('<%= lat%>', '<%= lon%>');
            var myOptions = {
                zoom: 10,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map-canvas"), myOptions);

            var marker = new google.maps.Marker({
                position: latlng,
                map: map,
                title: '<%= tankname%>'
            });
        }
        window.onload = InitializeMap;
    </script>
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:PlaceHolder id="MetaPlaceHolder" runat="server" />
    <div>
        <table class="formstyle" style="width:100%; text-align:center">
            <tr>
                <td  class="HeaderBars">
                    <asp:Label ID="lblTankID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblTankName" runat="server" Text="My Tank"></asp:Label>&nbsp Location
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    <div id="map-canvas" style="width:100%;height:800px;"></div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
