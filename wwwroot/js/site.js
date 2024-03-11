// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.acc-toggle').on('click', function () {
    if ($('.acc-toggle input').is(':checked')) {
        $('.create-acc-body').slideDown();
    } else {
        $('.create-acc-body').slideUp();
    }
});

$('.ship-toggle').on('click', function () {
    if ($('.ship-toggle input').is(':checked')) {
        $('.ship-acc-body').slideDown();
    } else {
        $('.ship-acc-body').slideUp();
    }
});