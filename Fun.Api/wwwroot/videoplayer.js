var connected = false;
var connection = new signalR.HubConnectionBuilder().withUrl("/videohub").build();
var connectButton = document.getElementById('connection-btn');
var videoplayer = document.getElementById('videoplayer');

connection.on('PlayVideo', function (fileName) {
    play(fileName);
});

connection.on('StopVideo', function () {
    stop();
});

function play(fileName) {
    if (videoplayer.paused) {
        videoplayer.src = '/videos/' + fileName;
        videoplayer.play();
    }
}

function stop() {
    videoplayer.pause();
}

function connection_click() {
    if (!connected) {
        connect();
    } else {
        disconnect();
    }
}

function connect() {
    connection.start().then(function () {
        connected = true;
        
        connectButton.classList.add('btn-outline-danger');
        connectButton.classList.remove('btn-outline-success');
        connectButton.textContent = 'disconnect';

        videoplayer.style.visibility = 'visible';
    }).catch(function (err) {
        return alert(err.toString());
    });
}

function disconnect() {
    connection.stop().then(function () {
        connected = false;

        connectButton.classList.add('btn-outline-success');
        connectButton.classList.remove('btn-outline-danger');
        connectButton.textContent = 'connect';

        videoplayer.style.visibility = 'hidden';
    }).catch(function (err) {
        return alert(err.toString());
    });;
}