<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="VaccineForAll.WebApp.About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vaccine for All</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta name="description" content="This application's ultimate goal is to provide valuable information which helps fellow citizens to get vaccinated in their respective district, and for your information, the use of this application is free of cost.">
    <meta name="viewport" content="width=devfe-width, initial-scale=1">
    <link rel="canonical" href="https://vaccineforall.co.in/" />
    <link rel="shortcut icon" href="/images/favicon.ico" type="image/x-icon">
    <meta property="og:locale" content="en_US" />
    <meta property="og:type" content="website" />
    <meta property="og:title" content="VaccineForAll" />
    <meta property="og:description" content="This application's ultimate goal is to provide valuable information which helps fellow citizens to get vaccinated in their respective district, and for your information, the use of this application is free of cost." />
    <meta property="og:url" content="https://vaccineforall.co.in/" />
    <meta property="og:site_name" content="VaccineForAll" />
    <meta property="og:image" itemprop="image" content="/images/vaccine.jpg">
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:description" content="This application's ultimate goal is to provide valuable information which helps fellow citizens to get vaccinated in their respective district, and for your information, the use of this application is free of cost." data-react-helmet="true" />
    <meta name="twitter:title" content="VaccineForAll" />
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no">
    <meta name="robots" content="NOODP">
    <meta name="theme-color" content="#eeeeee">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="apple-mobile-web-app-title" content="VaccineForAll">
    <meta name="google" content="notranslate">
    <meta name="mobile-web-app-capable" content="yes">
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
                    <p>This application's ultimate goal is to provide valuable information which helps fellow citizens to get vaccinated in their respective district, and for your information, the use of this application is free of cost.</p>
                    <br />
                    <p>«<b><i>We believe that it is everyone's social responsibility to keep each other safe! </i></b>»</p>
                    <br />
                    <p>Please note that this web application <b>does NOT book any slot on your behalf whatsoever </b>- it only provides valuable information to help the citizen to select the available center at that point in time.</p>
                    <h3>How it works</h3>
                    <p>The program run in an interval from 6 AM to 8 PM to query the provided Co-WIN Public APIs to look for an available center in your respective District, taking into consideration the age and available dose, for more details about the API, <a href='https://apisetu.gov.in/public/marketplace/api/cowin' target='_blank'>click here.</a></p>
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
