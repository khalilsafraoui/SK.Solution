window.initMap = function () {
    console.log("initMap called");

    const div = document.getElementById("map");
    if (!div) {
        console.error("Map div not found");
        return;
    }
    window.map = new google.maps.Map(div, {
        center: { lat: 34.0, lng: 9.0 },
        zoom: 6
    });
    
};

window.markers = [];
window.polylines = []; // Array to store all polyline references


window.addMarkers = function (customerLocations) {
    if (!window.map) {
        console.error("Map is not initialized. Call initMap first.");
        return;
    }

    customerLocations.forEach(function (customer) {
        const lat = parseFloat(customer.lat);
        const lng = parseFloat(customer.lng);

        if (isNaN(lat) || isNaN(lng)) {
            console.error("Invalid coordinates:", customer);
            return; // Skip this marker if coordinates are invalid
        }

        const marker = new google.maps.Marker({
            position: { lat: lat, lng: lng },
            map: window.map,
            label: customer.label
        });
        // Store the marker reference in the array
        window.markers.push(marker);
    });
};

window.clearMarkers = function () {
    // Loop through all markers and set map to null to remove them
    if (window.markers.length > 0) {
        window.markers.forEach(function (marker) {
            marker.setMap(null);
        });

        // Clear the array of markers after removal
        window.markers = [];
        console.log("Markers cleared");
    } else {
        console.log("No markers to clear.");
    }
};

window.drawPolylines = function (customerLocations) {
    if (!window.map) {
        console.error("Map is not initialized. Call initMap first.");
        return;
    }

    const path = customerLocations.map(p => ({
        lat: parseFloat(p.lat),
        lng: parseFloat(p.lng)
    }));

    const polyline = new google.maps.Polyline({
        path: path,
        geodesic: true,
        strokeColor: "#FF0000",
        strokeOpacity: 1.0,
        strokeWeight: 2
    });

    polyline.setMap(window.map);
    window.polylines.push(polyline); // Store the polyline reference
};

window.clearPolylines = function () {
    if (window.polylines.length > 0) {
        window.polylines.forEach(function (polyline) {
            polyline.setMap(null); // Remove polyline from the map
        });
        window.polylines = []; // Clear the array
        console.log("All polylines cleared");
    } else {
        console.log("No polylines to clear.");
    }
};


