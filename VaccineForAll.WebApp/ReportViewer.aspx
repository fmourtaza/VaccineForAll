<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="VaccineForAll.WebApp.ReportViewer" %>

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

    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>

    <script src="js/Metadata.js"></script>
    <script type="text/javascript">
        function HideLabel() {
            var seconds = 21;
            setTimeout(function () {
                document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            }, seconds * 1000);
        };

        function ShowSearchHideLabel() {
            document.getElementById("txtSearch").style.display = "block";
            document.getElementById("lblSaveSelection").style.cssText = "display: block; float: left";
            document.getElementById("chkSaveSelection").style.display = "block";
            HideLabel();
        };

        function ShowMessageOnSubmit() {
            alert("Form has been submitted successfully. You shall get a Welcome Mail soon !");
            HideLabel();
        }

        function ShowSaveSelectionControls() {
            var chkSaveSelection = document.getElementById("chkSaveSelection").checked;
            if (chkSaveSelection == true) {
                document.getElementById("email").style.display = "block";
                document.getElementById("email").required = true;
                document.getElementById("email").value = "";
                document.getElementById("btnSubmit").style.display = "inline";
            }
            else {
                document.getElementById("email").style.display = "none";
                document.getElementById("email").required = false;
                document.getElementById("btnSubmit").style.display = "none";
            }
        };

        function ResetControlValues() {
            document.getElementById("<%=lblMessage.ClientID %>").style.display = "none";
            document.getElementById("locality-dropdown").selectedIndex = "0";
            document.getElementById("district-dropdown").selectedIndex = "0";
            document.getElementById("age-dropdown").selectedIndex = "0";
            document.getElementById("dose-dropdown").selectedIndex = "0";
            document.getElementById('<%= GridView1.ClientID%>').innerText = null;
            document.getElementById("txtSearch").style.display = "none";
            document.getElementById("email").style.display = "none";
            document.getElementById("btnSubmit").style.display = "none";
            document.getElementById("lblSaveSelection").style.display = "none";
            document.getElementById("chkSaveSelection").style.display = "none";
            document.getElementById("chkSaveSelection").checked = false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="header">
            <h1><a href="Default.aspx">Vaccine for all! <span>Slot lookup</span></a></h1>
            <ul id="navigation">
                <li>
                    <a href="Default.aspx">Home</a>
                </li>
                <li class="current">
                    <a href="ReportViewer.aspx">Report Viewer</a>
                </li>
                <li>
                    <a href="About.aspx">About</a>
                </li>
            </ul>
        </div>
        <div id="body">

            <div id="tagline">
                <h1>Let's get vaccinated!
                    <br />
                    Report Viewer</h1>
                <p>This Report Viewer page shows the current data for a particular district.</p>
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
                        <option value="doseBoth">Both</option>
                    </select>
                    <input type="email" id="email" name="email" placeholder="Your email" runat="server" style="display: none;" />
                    <input type="submit" id="btnSearch" value="Search" runat="server" onclick="SetHiddenControlValue()" onserverclick="btnSearch_ServerClick" />
                    <input type="submit" id="btnReset" value="Reset" runat="server" onclick="ResetControlValues(); return false;" />
                    <input type="submit" id="btnSubmit" value="Submit" runat="server" onclick="SetHiddenControlValue()" onserverclick="btnSubmit_ServerClick" style="display: none;" />
                    <br />
                    <br />
                    <div style="margin-top: 10px; margin-bottom: 20px;">
                        <label id="lblSaveSelection" for="chkSaveSelection" style="display: none; float: left">Save this selection to get updates on your email?    </label>
                        <input type="checkbox" name="chkSaveSelection" id="chkSaveSelection" onchange="ShowSaveSelectionControls();" style="display: none;" />
                    </div>
                    <input type="text" id="txtSearch" name="Search" placeholder="Search within table" onkeyup="Search_Gridview(this, 'GridView1')" style="display: none;" runat="server" />
                    <input type="hidden" id="selectedStateID" runat="server" />
                    <input type="hidden" id="selectedDistrictName" runat="server" />
                    <input type="hidden" id="selectedDistrictID" runat="server" />
                    <input type="hidden" id="selectedAge" runat="server" />
                    <input type="hidden" id="selectedDose" runat="server" />
                </div>
            </div>
            <img src="images/indian-flag.jpg" alt="Indian Flag" class="figure" />
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="ServerClick" />
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="ServerClick" />
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="Sorting" />
                    <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="PreRender" />
                </Triggers>
                <ContentTemplate>
                    <div class="alert alert-success" style="float: left">
                        <asp:Label ID="lblMessage" ForeColor="Green" Font-Bold="true" Text="" runat="server" />
                        <br />
                        <br />
                    </div>
                    <asp:GridView ID="GridView1" CssClass="footable" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                        OnSorting="GridView1_Sorting" OnPreRender="GridView1_PreRender">
                        <Columns>
                            <asp:BoundField SortExpression="session_date" DataField="session_date" HeaderText="date" />
                            <asp:BoundField SortExpression="session.min_age_limit" DataField="session.min_age_limit" HeaderText="min_age_limit" />
                            <asp:BoundField SortExpression="session.available_capacity_dose1" DataField="session.available_capacity_dose1" HeaderText="dose1" />
                            <asp:BoundField SortExpression="session.available_capacity_dose2" DataField="session.available_capacity_dose2" HeaderText="dose2" />
                            <asp:BoundField SortExpression="session.vaccine" DataField="session.vaccine" HeaderText="vaccine" />
                            <asp:BoundField SortExpression="center.vaccine_fees" DataField="center.vaccine_fees" HeaderText="fees" />
                            <asp:BoundField SortExpression="center.district_name" DataField="center.district_name" HeaderText="district_name" />
                            <asp:BoundField SortExpression="center.address" DataField="center.address" HeaderText="address" />
                            <asp:BoundField SortExpression="center.pincode" DataField="center.pincode" HeaderText="pincode" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
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
            $('[id*=GridView1]').footable();
        })();

        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('[id*=GridView1]').footable();
        });

        function SetFootable() {
            $('[id*=GridView1]').footable();
            $('[id*=GridView1]').setAttribute("data-class", "expand");
        }

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
            Metadata.SetDropDownDistrict();
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
            let selectedDose = document.getElementById("dose-dropdown").value;
            let selText = document.getElementById("district-dropdown");
            let selectedDistrictName = selText.options[selText.selectedIndex].text;
            document.getElementById("selectedStateID").value = selectedStateID;
            document.getElementById("selectedDistrictID").value = selectedDistrictID;
            document.getElementById("selectedDistrictName").value = selectedDistrictName;
            document.getElementById("selectedAge").value = selectedAge;
            document.getElementById("selectedDose").value = selectedDose;
        }

        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }
    </script>
</body>
</html>
