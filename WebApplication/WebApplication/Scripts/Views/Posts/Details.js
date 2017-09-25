
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
window.fbAsyncInit = function () {
    FB.init({
        appId: '2008650222702912',
        autoLogAppEvents: true,
        xfbml: true,
        version: 'v2.10'
    });

    FB.AppEvents.logPageView();
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
function myFunction() {
    shareToFacebook(document.getElementById("ModelDescription").innerHTML);
}

$("#saveComment").hide();


$("#saveComment").click(function () {
    var postId = $(this).attr("data-id");
    var commentDescription = $("#commentDesc").val();
    $.post("http://localhost:56587/posts/" + postId + "/comment",
        {
            commentDescription: commentDescription
        }, function (data, status) {
            location.reload();
        });
});

$(".fa").click(function () {
    var commnetId = $(this).attr("data-id");
    $.ajax({
        url: "http://localhost:56587/comments/" + commnetId,
        type: 'DELETE',
        success: function (result) {
            location.reload();
        }
    });
});

$('#commentDesc').bind('input propertychange', function () {
    $("#saveComment").hide();

    if (this.value.length) {
        $("#saveComment").show();
    }
});