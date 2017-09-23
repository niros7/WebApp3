
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