class Metadata {

    static SetDropDownStates() {
        let dropdown = document.getElementById('locality-dropdown');
        dropdown.length = 0;
        let defaultOption = document.createElement('option');
        defaultOption.text = 'Choose State';
        defaultOption.value = '';
        dropdown.add(defaultOption);
        dropdown.selectedIndex = 0;
    }

    static SetDropDownDistrict() {
        let dropdown = document.getElementById('district-dropdown');
        dropdown.length = 0;
        let defaultOption = document.createElement('option');
        defaultOption.text = 'Choose District';
        defaultOption.value = '';
        dropdown.add(defaultOption);
        dropdown.selectedIndex = 0;
    }

    static FetchData(url, info) {
        try {
            // 1. Create a new XMLHttpRequest object
            let xhr = new XMLHttpRequest();

            // 2. Configure it: GET-request for the URL /article/.../load
            xhr.open('GET', url);
            xhr.setRequestHeader('Accept', 'application/json');

            // 3. Send the request over the network
            xhr.send();

            // 4. This will be called after the response is received
            xhr.onload = function () {
                if (xhr.status != 200) { // analyze HTTP status of the response
                    console.log(`Error ${xhr.status}: ${xhr.statusText}`); // e.g. 404: Not Found
                } else { // show the result
                    console.log(`Done, got ${xhr.response.length} bytes`); // response is the server response
                    let data = JSON.parse(xhr.responseText);
                    if (info === "states") {
                        Metadata.FillStatesValues(data);
                    }
                    else
                        Metadata.FillDistrictsValues(data);

                }

            };

            xhr.onprogress = function (event) {
                if (event.lengthComputable) {
                    console.log(`Received ${event.loaded} of ${event.total} bytes`);
                } else {
                    console.log(`Received ${event.loaded} bytes`); // no Content-Length
                }

            };

            xhr.onerror = function () {
                console.log("Request failed");
            };
        } catch (e) {

        }

    }

    static FillStatesValues(obj) {
        for (var k in obj) {
            if (obj[k] instanceof Object) {
                Metadata.FillStatesValues(obj[k]);
            } else {
                let IsExist = document.getElementById('locality-dropdown').querySelector('[value="' + obj["state_id"] + '"]');
                console.log(IsExist);
                if (IsExist == null && obj["state_id"] != undefined) {
                    let dropdown = document.getElementById('locality-dropdown');
                    let option;
                    console.log('State: ' + obj["state_name"] + ' - ' + obj["state_id"]);
                    option = document.createElement('option');
                    option.text = obj["state_name"];
                    option.value = obj["state_id"];
                    dropdown.add(option);
                }
            };
        }
    };

    static FillDistrictsValues(obj) {
        for (var k in obj) {
            if (obj[k] instanceof Object) {
                Metadata.FillDistrictsValues(obj[k]);
            } else {
                let IsExist = document.getElementById('district-dropdown').querySelector('[value="' + obj["district_id"] + '"]');
                console.log(IsExist);
                if (IsExist == null && obj["district_id"] != undefined) {
                    let dropdown = document.getElementById('district-dropdown');
                    let option;
                    console.log('State: ' + obj["district_name"] + ' - ' + obj["district_id"]);
                    option = document.createElement('option');
                    option.text = obj["district_name"];
                    option.value = obj["district_id"];
                    dropdown.add(option);
                }
            };
        }
    };
}