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