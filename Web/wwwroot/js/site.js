var context = {};

$(document).ready(function () {
    setTimeout(function () {
        $('.textarea').bind('keydown', function (event) {
            if (event.keyCode == 13 && $(this).val()) {
                sendMessage($(this).val());
            }
        });
    }, 1000);
});
sendMessage = function (text) {
    var data = {
        context: context,
        text: text
    };
    renderMessage(text, 'self');
    $.ajax({
        url: "api/chat/",
        type: 'post',
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (data) {
            context = data.context;
            renderMessage(data.text, 'other');
        }
    });
}

renderMessage = function (text, to) {
    var urlImage = '';
    if (to == 'self')
        urlImage = 'images/user.png';
    else
        urlImage = 'images/logo.png';
    var li = $('<li>', {
        class: to,
    });
    var img = $('<img>', {
        src: urlImage,
    });
    var avatar = $('<div>', {
        class: 'avatar',
    }).prepend(img);
    var date = new Date();
    var msg = $('<div>', {
        class: 'msg',
    }).prepend('<p>' + text + '</p><time>' + date.getHours() + ':' + date.getMinutes());
    li.prepend(avatar);
    li.prepend(msg);
    $('ol').append(li);
    $('ol').get(0).scrollTop = $('ol').get(0).scrollHeight;
    $('.textarea').val('');
}
