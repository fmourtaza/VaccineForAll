<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VaccineForAll.WebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vaccine for All</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="user-scalable=0, width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <link rel="stylesheet" type="text/css" href="css/style.css">
    <link rel="stylesheet" type="text/css" href="css/mobile.css">
    <script type='text/javascript' src='js/mobile.js'></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="js/Metadata.js"></script>
    <script type="text/javascript">
        function ShowMessage() {
            document.getElementById("email").value = "";
            alert("Submitted successfully! for confirmation you shall get a Welcome Mail.");
        }
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
                    <a href="About.aspx">About</a>
                </li>
            </ul>
        </div>
        <div id="body">
            <div id="tagline">
                <h1>Let's get vaccinated!</h1>
                <p>This web application looks for the vaccine slot availability in your respective District by selecting the age & available dose criteria.</p>
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
                    <input type="email" id="email" name="email" placeholder="Your email.." required="required" runat="server" />
                    <input type="submit" id="btnSubmit" value="Submit" runat="server" onclick="SetHiddenControlValue()" onserverclick="btnSubmit_ServerClick" />
                    <input type="hidden" id="selectedStateID" runat="server" />
                    <input type="hidden" id="selectedDistrictName" runat="server" />
                    <input type="hidden" id="selectedDistrictID" runat="server" />
                    <input type="hidden" id="selectedAge" runat="server" />
                    <br />
                    <br />
                </div>
            </div>
            <img src="images/indian-flag.jpg" alt="Indian Flag" class="figure" />
        </div>
        <div id="footer">
            <div>
                <span>Mourtaza Moise Fazlehoussen | fmourtaza@gmail.com</span>
                <p>
                    &copy; This project is an Open Source&amp; is licensed under the MIT License.
                </p>
            </div>
        </div>
    </form>
    <script>
        // self executing function here
        (function () {
            let url_states = "https://cdn-api.co-vin.in/api/v2/admin/location/states";
            Metadata.FetchData(url_states, "states");
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
            let selText = document.getElementById("district-dropdown");
            let selectedDistrictName = selText.options[selText.selectedIndex].text;
            document.getElementById("selectedStateID").value = selectedStateID;
            document.getElementById("selectedDistrictID").value = selectedDistrictID;
            document.getElementById("selectedDistrictName").value = selectedDistrictName;
            document.getElementById("selectedAge").value = selectedAge;
        }
    </script>
</body>
</html>
