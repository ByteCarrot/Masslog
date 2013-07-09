function showError(message) {
    $('#error-text').text(message);
    $('#error-box').show('blind', null, 500);
}

function hideError() {
    $('#error-box').hide('blind', null, 500);
}

$(function () {
    $('#error-box').css('display', 'none').click(hideError);
    $(document).ajaxError(function (event, jqxhr, options) {
        showError('Application was unable to get data from: ' + options.url);
    });
});