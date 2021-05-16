<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="VaccineForAll.WebApp.About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <title>Vaccine for All</title>
    <link rel="stylesheet" type="text/css" href="css/style.css">
    <link rel="stylesheet" type="text/css" href="css/mobile.css">
    <script type='text/javascript' src='js/mobile.js'></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <h1><a href="Default.aspx">Vaccine for all! <span>Slot lookup</span></a></h1>
            <ul id="navigation">
                <li>
                    <a href="Default.aspx">Home</a>
                </li>
                <li>
                    <a href="ReportViewer.aspx">Report Viewer</a>
                </li>
                <li class="current">
                    <a href="About.aspx">About</a>
                </li>
            </ul>
        </div>
        <div id="body">
            <h2>About</h2>
            <div class="content">
                <div>
                    <h3>Let's get vaccinated!</h3>
                    <p>This web application looks for the vaccine slot availability in your respective District by selecting the age & available dose criteria - using the Co-WIN Public APIs, for more details about the API, <a href="https://apisetu.gov.in/public/marketplace/api/cowin" target="_blank">click here</a></p>
                    <br />
                    <p>Please note that this web application <b>does NOT book any slot on your behalf whatsoever </b>- it only provides valuable information to help the citizen to select the available center at that point in time.</p>
                    <h3>How it works</h3>
                    <p>The program run in an interval to query the provided Co-WIN Public APIs to look for an available center in your respective District, taking into consideration the age and available dose. </p>
                    <br />
                    <p>Once the program finds an available center, an email will be sent at the registered email address a complete report with all details which shows all the available centers along with the available dose at that point in time. </p>
                    <br />
                    <p>It is important to mention that upon receiving the report, it is highly recommended to book the slots on the <a href="https://www.cowin.gov.in/home" target="_blank">cowin.gov.in</a> website or using the Aarogya Setu mobile app.</p>
                    <h3>How it all started</h3>
                    <p>Getting the vaccine is not an easy task - either you go to a vaccination center early to get a token or if lucky try to get an available slot in the Cowin site/Aarogya Setu mobile app - this has been the same experience got by friends, relatives, and colleagues across the country.</p>
                    <br />
                    <p>Therefore using the Co-WIN Public APIs, I decided to provide this web application to help all my fellow citizens to get vaccinated!</p>
                </div>
                <img src="images/indian-flag.jpg" alt="Indian Flag" class="figure" />
            </div>
        </div>
        <div id="footer">
            <div>
                <span>Developed by <a href="mailto:fmourtaza@gmail.com" style="color: #99fa99;">Mourtaza Moise Fazlehoussen</a></span>
                <p>
                    &copy; This project is an <a href="https://github.com/fmourtaza/VaccineForAll" target="_blank" style="color: white;">Open Source</a> &amp; licensed under the MIT License.
                </p>
            </div>
        </div>
    </form>
</body>
</html>
