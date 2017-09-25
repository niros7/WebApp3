
    function shareToFacebook(x) {
        FB.ui(
            {
                method: 'share',
                hashtag: '#shauliBlog',
                quote: x,
                //                put in href the url of the blog
                href: 'https://www.nytimes.com/'
            }, function (response) { });
    }
    window.fbAsyncInit = function() {
        FB.init({
            appId: '2008650222702912',
            autoLogAppEvents: true,
            xfbml: true,
            version: 'v2.10'
        });

    FB.AppEvents.logPageView();
        };

        (function(d, s, id){
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) {return;}
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));


$(".fa").click(function () {
    var postId = $(this).attr("data-id");
    $.ajax({
        url: "http://localhost:56587/posts/" + postId,
        type: 'DELETE',
        success: function (result) {
            location.reload();
        }
    });
});

$('#searchButton').prop('disabled', function (i, v) { return true; });

var validate = function () {
    $('#searchButton').prop('disabled', function (i, v) { return true; });

    if ($("#subTitleSearch").val() ||
        $("#usernameSearch").val() ||
        $("#titleSearch").val()) {
        $('#searchButton').prop('disabled', function (i, v) { return false; });
    }
};

$('#subTitleSearch').bind('input propertychange', validate);
$('#usernameSearch').bind('input propertychange', validate);
$('#titleSearch').bind('input propertychange', validate);
