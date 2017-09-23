$('.chips').material_chip();
$("#savePost").hide();

$("#savePost").click(function () {
    var postTitle = $("#postTitle").val();
    var postSubTitle = $("#postSubTitle").val();
    var postDescription = $("#postDescription").val();
    var tags = $('.chips').material_chip('data').map(x => x.tag);
    $.post("http://localhost:56587/Posts/Create",
        {
            Title: postTitle,
            SubTitle: postSubTitle,
            Description: postDescription,
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

