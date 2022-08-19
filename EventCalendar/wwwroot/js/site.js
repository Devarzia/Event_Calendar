// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('nav li a').click(function () {
    $('nav li.active').removeClass('active');
    var $parent = $(this).parent();
    $parent.addClass('active');
});

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