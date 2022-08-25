// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $(".addEvent").click(function (e) {
        clearModal();
        checkAllDay();
        $("select#SocialEvent_CategoryID").empty();
        $.ajax({
            url: "/Event/AddSocialEvent"
        })
            .done(function (data) {
                console.log(data);
                $.each(data, function () {
                    $("select#SocialEvent_CategoryID").append($("<option />").val(this.eventCategoryID).text(this.eventCategoryName));
                });
            })
    })

    $(".editEvent").click(function (e) {
        checkAllDay();
        $("select#SocialEvent_CategoryID").empty();
        eventID = $(this).data('socialeventid');
        $.ajax({
            url: "/Event/EditSocialEvent?socialEventID=" + eventID
        })
            .done(function (data) {
                console.log(data);
                $(".eventName").text(data.socialEvent.socialEventName);
                $("input#SocialEvent_SocialEventID").val(eventID);
                $("input#SocialEvent_SocialEventName").val(data.socialEvent.socialEventName);
                $("textarea#SocialEvent_SocialEventDescription").val(data.socialEvent.socialEventDescription);
                $("input#SocialEvent_StartTime").val(data.socialEvent.startTime);
                if (data.socialEvent.allDay) {
                    $(".chkAllDay").attr("checked", true);
                    $(".txtEndTime").val("").prop("disabled", true);
                }
                else {
                    $(".chkAllDay").attr("checked", false);
                    $(".txtEndTime").val(data.socialEvent.endTime).removeAttr("disabled");
                }
                $.each(data.categories, function () {
                    $("select#SocialEvent_CategoryID").append($("<option />").val(this.eventCategoryID).text(this.eventCategoryName));
                });
                $("select#SocialEvent_CategoryID").val(data.socialEvent.categoryID);
            })
    })

    $(".viewModal").click(function () {
        $("select#SocialEvent_CategoryID").empty();
        eventID = $(this).data('socialeventid');
        $.ajax({
            url: "/Event/EditSocialEvent?socialEventID=" + eventID
        })
            .done(function (data) {
                console.log(data);
                $(".eventName").text(data.socialEvent.socialEventName);
                $("input#SocialEvent_SocialEventID").val(eventID);
                $("input#SocialEvent_SocialEventName").val(data.socialEvent.socialEventName);
                $("input#SocialEvent_Category").val(data.socialEvent.category);
                $("input#SocialEvent_SocialEventDescription").val(data.socialEvent.socialEventDescription);
                $("input#SocialEvent_StartTime").val(data.socialEvent.startTime);
                $("input#SocialEvent_EndTime").val(data.socialEvent.endTime);
                $("input#SocialEvent_DateModified").val(data.socialEvent.dateModified);
                $("input#SocialEvent_DateCreated").val(data.socialEvent.dateCreated);
                if (data.socialEvent.allDay) {
                    $("input#SocialEvent_AllDay").val("Yes");
                }
                else {
                    $("input#SocialEvent_AllDay").val("No");
                }
            })
    })

    $(".deleteEvent").click(function (e) {
        eventID = $(this).data('socialeventid');
        $.ajax({
            url: "/Event/GetSocialEventByID?socialEventID=" + eventID
        })
            .done(function (data) {
                $("input#SocialEvent_SocialEventID").val(eventID);
                $("input#SocialEvent_SocialEventName").val(data.socialEventName);
                $("textarea#SocialEvent_SocialEventDescription").val(data.socialEventDescription);
                $(".eventName").text(data.socialEventName);
            })
    })

    function clearModal() {
        $("input#SocialEvent_SocialEventName").val("");
        $("textarea#SocialEvent_SocialEventDescription").val("");
        $("select#SocialEvent_CategoryID").val("");
        $("input#SocialEvent_StartTime").val("");
        $(".txtEndTime").val("").prop("disabled", false);
        $(".chkAllDay").attr("checked", false);
    }

    function checkAllDay() {
        $(".chkAllDay").click(function () {
            let isChecked = $(".chkAllDay").is(":checked");
            if (isChecked) {
                $(".txtEndTime").prop("disabled", true);
            }
            else {
                $(".txtEndTime").removeAttr("disabled");
            }
        });
    }
})

document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl,
        {
            initialView: 'dayGridMonth',
            headerToolbar:
            {
                end: 'dayGridMonth,dayGridWeek,timeGridDay,list',
                center: 'title',
                start: 'prevYear,prev,next,nextYear today'
            },
            events: 'Event/GetEvents'
        });
    calendar.render();
});