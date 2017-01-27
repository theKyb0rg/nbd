$(document).ready(function () {

    // Set the default position for global notificationsnotifications
    //$.notify.defaults({ globalPosition: 'bottom right' });

    // To override the defaults for bootstrapnotify
    $.notifyDefaults({
        placement: {
            from: 'bottom'
        }
    });

    // Test notifications click
    // API Documentation: https://notifyjs.com/
    //$("#btnNotification").click(function (e) {
    //    // Prevents from auto scrolling to the bottom of the page
    //    e.preventDefault();

    //    // Displays message, param1 = message, param2 = style
    //    // Available styles:
    //    // success, info, warn, error
    //    $.notify("Check out this awesome notification.", "success");
    //});

    // Click event for reset button on Default.aspx
    $('.reset').click(function () {
        $('#Body_txtUsername').val('');
        $('#Body_txtPassword').val('');
    });

    //Back to Top Script
    // browser window scroll (in pixels) after which the "back to top" link is shown
    var offset = 300,
        //browser window scroll (in pixels) after which the "back to top" link opacity is reduced
        offset_opacity = 1200,
        //duration of the top scrolling animation (in ms)
        scroll_top_duration = 700,
        //grab the "back to top" link
        $back_to_top = $('.cd-top');

    //hide or show the "back to top" link
    $(window).scroll(function () {
        ($(this).scrollTop() > offset) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
        if ($(this).scrollTop() > offset_opacity) {
            $back_to_top.addClass('cd-fade-out');
        }
    });

    //smooth scroll to top
    $back_to_top.on('click', function (event) {
        event.preventDefault();
        $('body,html').animate({
            scrollTop: 0,
        }, scroll_top_duration
        );
    });
});

function openModal() {
    $('#mdlShowDetails').modal('show');

    //this is the function for the pie percent
    $$('.pie').forEach(function (pie) {
        var p = pie.textContent;
        pie.style.animationDelay = '-' + parseFloat(p) + 's';
    });
}

//Function for the Pie Chart
function $$(selector, context) {
    context = context || document;
    var elements = context.querySelectorAll(selector);
    return Array.prototype.slice.call(elements);
}

