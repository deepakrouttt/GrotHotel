$(document).ready(function () {
    // Event delegation for click events on room-image elements
    debugger
    $(".room-image").click(function () {
        $("#imageModal").css("display", "block");
        $("#fullImage").attr("src", $(this).attr("src"));
    });

    // Click event for closing the modal
    $("#imageModal .close").click(function () {
        $("#imageModal").css("display", "none");
    });
});