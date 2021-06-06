<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VaccineForAll.WebApp.Default" %>

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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="js/Metadata.js"></script>
    <script type="text/javascript">
        function ShowMessage() {
            document.getElementById("email").value = "";
            alert("Form has been submitted successfully. You shall get a Welcome Mail soon !");
        }

        function HideLabel() {
            document.getElementById("email").value = "";
            var seconds = 21;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <h1><a href="Default.aspx">Vaccine for all! <span>Slot lookup</span></a></h1>
            <ul id="navigation">
                <li class="current">
                    <a href="Default.aspx">Home</a>
                </li>
                <li>
                    <a href="ReportViewer.aspx">Report Viewer</a>
                </li>
                <li>
                    <a href="About.aspx">About</a>
                </li>
            </ul>
        </div>
        <div id="body">
            <div id="tagline">
                <h1>Let's get vaccinated!</h1>
                <%--<div style="background: url(../images/bg-separator.png) no-repeat center bottom; padding: 0 0 55px;">
                    <p>This application's ultimate goal is to provide valuable information which helps fellow citizens to get vaccinated in their respective district.</p>
                </div>
                <p style="color: red;">We regret to inform you that we are not taking any new request as of now due to the increasing demand. Please try after 24h.</p>--%>
                <p>This application's ultimate goal is to provide valuable information which helps fellow citizens to get vaccinated in their respective district.</p>
                <br />
                <div>
                    <select id="locality-dropdown" name="locality" onchange="FillDistricts()">
                        <option value="">Choose State</option>
                    </select>
                    <select id="district-dropdown" name="district">
                        <option value="">Choose District</option>
                    </select>
                    <select id="age-dropdown" name="age" required="required">
                        <option value="">Choose Age</option>
                    </select>
                    <select id="dose-dropdown" name="dose" required="required">
                        <option value="">Choose Dose</option>
                        <option value="dose1">Dose 1</option>
                        <option value="dose2">Dose 2</option>
                    </select>
                    <input type="email" id="email" name="email" placeholder="Your email" required="required" runat="server" />
                    <input type="submit" id="btnSubmit" value="Submit" runat="server" onclick="SetHiddenControlValue()" onserverclick="btnSubmit_ServerClick" />
                    <input type="hidden" id="selectedStateID" runat="server" />
                    <input type="hidden" id="selectedDistrictName" runat="server" />
                    <input type="hidden" id="selectedDistrictID" runat="server" />
                    <input type="hidden" id="selectedAge" runat="server" />
                    <input type="hidden" id="selectedDose" runat="server" />
                    <br />
                    <br />
                    <div class="alert alert-success">
                        <asp:Label ID="lblMessage" ForeColor="Green" Font-Bold="true" Text="Form has been submitted successfully. You shall get a Welcome Mail soon !" runat="server" Visible="false" />
                    </div>
                    <br />
                    <br />
                </div>
            </div>
            <img src="images/indian-flag.jpg" alt="Indian Flag" class="figure" />
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
    <script>
        // self executing function here
        (function () {
            let url_states = "https://cdn-api.co-vin.in/api/v2/admin/location/states";
            Metadata.FetchData(url_states, "states");
            Metadata.SetDropDownStates();
            SetSelectControlRequired('locality-dropdown');
            FillAge();
        })();

        function SetSelectControlRequired(controlId) {
            let dropdown = document.getElementById(controlId);
            dropdown.setAttribute('required', 'required');
            let x = document.getElementById(controlId).selectedIndex;
            let y = document.getElementById(controlId).options;
            y[x].value = "";
        }

        function FillDistricts() {
            let selectedState = document.getElementById("locality-dropdown").value;
            let url_districts = "https://cdn-api.co-vin.in/api/v2/admin/location/districts/" + selectedState;
            Metadata.FetchData(url_districts, "districts");
            Metadata.SetDropDownDistrict();
            SetSelectControlRequired('district-dropdown');
        }

        function FillAge() {
            let dropdown = document.getElementById('age-dropdown');
            for (i = 18; i <= 100; i++) {
                let defaultOption = document.createElement('option');
                defaultOption.text = i;
                defaultOption.value = i;
                dropdown.add(defaultOption);
            }
            dropdown.selectedIndex = 0;
        }

        function SetHiddenControlValue() {
            let selectedStateID = document.getElementById("locality-dropdown").value;
            let selectedDistrictID = document.getElementById("district-dropdown").value;
            let selectedAge = document.getElementById("age-dropdown").value;
            let selectedDose = document.getElementById("dose-dropdown").value;
            let selText = document.getElementById("district-dropdown");
            let selectedDistrictName = selText.options[selText.selectedIndex].text;
            document.getElementById("selectedStateID").value = selectedStateID;
            document.getElementById("selectedDistrictID").value = selectedDistrictID;
            document.getElementById("selectedDistrictName").value = selectedDistrictName;
            document.getElementById("selectedAge").value = selectedAge;
            document.getElementById("selectedDose").value = selectedDose;
        }
    </script>
</body>
</html>
