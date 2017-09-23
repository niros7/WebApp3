$("#savePost").click(function () {
    var postTitle = $("#postTitle").val();
    var postSubTitle = $("#postSubTitle").val();
    var postDescription = $("#postDescription").val();
    $.post("http://localhost:56587/Posts/Create",
        {
            Title: postTitle,
            SubTitle: postSubTitle,
            Description: postDescription,
        }, function (data, status) {
            location.replace("/");
        });
});