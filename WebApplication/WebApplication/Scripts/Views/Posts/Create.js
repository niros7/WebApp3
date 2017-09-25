
var geocoder;
var map;
function initialize() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(-34.397, 150.644);
    var mapOptions = {
        zoom: 8,
        center: latlng
    }
    map = new google.maps.Map(document.getElementById('mapa'), mapOptions);
}

function codeAddress() {
    var address = document.getElementById('address').value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            //todo: instead of alert, save location to server
            alert(results[0].geometry.location);
            var lat = results[0].geometry.location.lat();
            var lng = results[0].geometry.location.lng();
            document.getElementById("lat").value = lat;
            document.getElementById("lng").value = lng;
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function initMap() {

    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 3,
        center: { lat: -28.024, lng: 140.887 }
    });


    var labels = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';

    var markers = locations.map(function (location, i) {
        return new google.maps.Marker({
            position: location,
            label: labels[i % labels.length]
        });
    });

    // Add a marker clusterer to manage the markers.
    var markerCluster = new MarkerClusterer(map, markers,
        { imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m' });
}

$('.chips').material_chip();
$("#savePost").hide();

$("#savePost").click(function () {
    var postTitle = $("#postTitle").val();
    var postSubTitle = $("#postSubTitle").val();
    var postDescription = $("#postDescription").val();
    var lat = $("#lat").val();
    var lng = $("#lng").val();
    var tags = $('.chips').material_chip('data').map(x => x.tag);
    $.post("http://localhost:56587/Posts/Create",
        {
            Title: postTitle,
            SubTitle: postSubTitle,
            Description: postDescription,
            Lat: lat,
            Lng: lng,
            Tags: tags
        }, function (data, status) {
            location.replace("/");
        });
});

var validate = function () {
    $("#savePost").hide();

    if ($("#postTitle").val() && $("#postSubTitle").val() && $("#postDescription").val() && $('.chips').material_chip('data').length) {
        $("#savePost").show();
    }
};

$('#postTitle').bind('input propertychange', validate);
$('#postSubTitle').bind('input propertychange', validate);
$('#postDescription').bind('input propertychange', validate);

$('.chips').on('chip.add', function (e, chip) {
    validate();
});

$('.chips').on('chip.delete', function (e, chip) {
    validate();
});


$('.chips').on('chip.select', function (e, chip) {
    validate();
});

