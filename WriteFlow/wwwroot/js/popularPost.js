$(document).ready(function () {
    $.ajax({
        url: "/index/PopularPost",
        type: "get"
    }).done(function (data) {
        $("#popular_posts").html(data)
    })
})

