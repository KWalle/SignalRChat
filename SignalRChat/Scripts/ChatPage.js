/// <reference path="jquery-1.6.4.js" />
/// <reference path="jquery.signalR-1.0.1.js" />
$(function() {
    $('#HiddenUserName').val(prompt('Ditt brukernavn: ', ''));  //Get username..not the best way but works..

    var chatHubProxy = $.connection.chatHub;

    chatHubProxy.client.sendMessage = function(usr, msg) {
        $('#Messages').append('<li>' + usr + ' sier: ' + msg + '</li>');
    };

    $.connection.hub.start().done(function () {
        var usrName = $('#HiddenUserName').val();
        $('#BtnSendMessage').click(function() {
            var msgToSend = $('#Message').val();
            chatHubProxy.server.broadcastMessage(usrName, msgToSend);
            $('#Message').val('');
        });
    });
});