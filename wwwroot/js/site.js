$(document).ready(function () {
    $("#Imagefile").hide();
    $("#RoomImageFile").hide();
    var state = true;

    //Edit Hotel with Images
    $("#btn-image").click(function () {
        if (state) {
            $("#Imagefile").show();
            $("#Image").hide();
            $('form').attr('action', '/Hotel/EditImage');
        } else {
            $("#Image").show();
            $("#Imagefile").hide();
            $('form').attr('action', '/Hotel/Edit');
        }
        state = !state;
    });

    //Edit HomeRooms with Images
    $("#btn-RoomImage").click(function () {
        if (state) {
            $("#RoomImageFile").show();
            $("#RoomImage").hide();
            $('form').attr('action', '/Hotel/EditRoomImage');
        } else {
            $("#RoomImage").show();
            $("#RoomImageFile").hide();
            $('form').attr('action', '/Hotel/EditRoom');
        }
        state = !state;
    });

    $("#IsExtraAdult").change(function () {
        if ($(this).prop("checked") == false) {
            $("#AdultRate").prop("disabled", true);
        }
        else {
            $("#AdultRate").prop("disabled", false);
        }
    });
    $("#IsChildAllow").change(function () {
        if ($(this).prop("checked") == false) {
            $("#childRate").prop("disabled", true);
        }
        else {
            $("#childRate").prop("disabled", false);
        }
    });

    //Exception on Create and Edit roomRate price
    $("#IsSingleEqualDouble").change(function () {
        if ($(this).prop("checked") == true) {
            $("#SingleRate").val($("#DoubleRate").val());
        }
        else {
            $("#childRate").prop("disabled", false);
            var value = ($("#SingleRate").val()) / 2;;
            $("#SingleRate").val(value);
        }
    });


    $(".showBlackOut").click(function () {
        $("#datepicker").datepicker('destroy');
        $(".popBlackDate").show();

        $(".popBlackDate").prevAll().not(".popBlackDate").css("filter", "blur(3px)");

        var RoomRateId = $(this).data("id");
        var dateFrom = $(this).closest('tr').find('.date-from').data('date');
        var dateTo = $(this).closest('tr').find('.date-to').data('date');

        $('#datepicker').datepicker({
            dateFormat: 'yy-mm-dd',
            minDate: dateFrom,
            maxDate: dateTo,
            onSelect: function (dateText, inst) {
                $(this).find('.ui-state-active').css('background-color', 'yellow');
            }
        });


        $.ajax({
            url: 'https://localhost:44309/api/HotelApi/GetBlackOutDate/' + RoomRateId,
            type: 'GET',
            success: function (dates) {
                $('#datepicker').datepicker('option', 'beforeShowDay', function (date) {
                    var stringDate = $.datepicker.formatDate('yy-mm-dd', date);
                    var isBlackout = (dates.blackOutDates.indexOf(stringDate) !== -1);
                    return [true, isBlackout ? 'blackout-date' : ''];
                });
            },
            error: function (error) {
                console.log(error);
                $("#datepicker").datepicker('option', 'beforeShowDay', function (date) {
                    return [true, ''];
                });
            },
            complete: function () {
                $("#datepicker").focus();

                $(document).on("click", ".AddBlackOutDate", function () {
                    var date = $("#datepicker").val();
                    var blackoutDate = {
                        "roomRateId": RoomRateId,
                        "dates": [{ "date": date }]
                    };
                    $.ajax({
                        url: 'https://localhost:44309/api/HotelApi/addBlackOutDate',
                        type: 'POST',
                        contentType: "application/json",
                        data: JSON.stringify(blackoutDate),
                        success: function (data) {
                            $(".popBlackDate").prevAll().not(".popBlackDate").css("filter", "none");
                            $(".popBlackDate").hide();
                            window.location.reload();
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                });

                $(document).on("click", ".RemoveBlackOutDate", function () {
                    var date = $("#datepicker").val();
                    $.ajax({
                        url: 'https://localhost:44309/api/HotelApi/DeleteBlackOutDate?date=' + date +'&id='+ RoomRateId,
                        type: 'Delete',
                        data: null,
                        success: function (data) {
                            $(".popBlackDate").prevAll().not(".popBlackDate").css("filter", "none");
                            $(".popBlackDate").hide();
                            window.location.reload();
                        },
                        error: function (error) {
                            console.log(error);
                        }
                    });
                });
            }
        });
    });

    //Close Pop Date Picker
    $(".closepopBlackOut").click(function () {
        $(".popBlackDate").prevAll().not(".popBlackDate").css("filter", "none");
        $(".popBlackDate").hide();
    })


    //Total Priceing 
    $(".priceContainerWrapper").each(function () {
        var total = 0;
        var $wrapper = $(this);
        $wrapper.find(".priceContainer .p-1 span").each(function () {
            var value = $(this).text().trim();
            if (value !== "N/A") {
                total += parseFloat(value);
            }
        });
        total = parseFloat(total).toLocaleString('en');
        $wrapper.parent().find(".TotalRate").text(total + ".00/-");
    });
});