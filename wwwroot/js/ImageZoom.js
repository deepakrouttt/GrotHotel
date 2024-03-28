$(document).ready(function () {
    $(".room-image").click(function () {
        $("#imageModal").css("display", "block");
        $("#fullImage").attr("src", $(this).attr("src"));
    });

    $("#imageModal .close").click(function () {
        $("#imageModal").css("display", "none");
    });
});