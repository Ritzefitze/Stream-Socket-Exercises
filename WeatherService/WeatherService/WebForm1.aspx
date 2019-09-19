<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WeatherService.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<script language="C#" runat="server">
    String zip = "11111";
    void Submit_Click(object sender, EventArgs e)
    {
        try
        {
            // Get the zip code from the form
            zip = ZipCode.Text;
        }
        catch (Exception ex) // ignored
        {
        }

        WeatherService.Service1 myService = new WeatherService.Service1(); // Create the weater service that will return the forecast
        Result.Text = "Today's forecast is: " + myService.GetTodaysForecast(zip); // Set the display with the results of GetTodaysForecast
    }
</script>
<body style="font: 10pt verdana">
<h4>Weather Report</h4>
    <form id="form1" runat="server">
    <div style="padding:15,15,15,15; background-color:Gray; width:300; border-color:Black; border-width:1; border-style:solid">
    Zip Code: <br /><asp:TextBox id="ZipCode" Text="11111" runat="server" /><br />
    <input type="submit" id="Add" value="Get Weather Report" onserverclick="Submit_Click" runat="server" />
    <p />
    <asp:Label ID="Result" runat="server" />
    </div>
    </form>
</body>
</html>
