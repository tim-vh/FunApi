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

document.addEventListener("DOMContentLoaded", initialize);

function initialize() {
    addButtonOnclickEvents();

    var connetionButton = document.querySelector("#connection-btn");
    connetionButton.onclick = connection_click;

    let videoFilter = document.querySelector("#video-filter");
    videoFilter.oninput = filterVideos;

    let filterClearButton = document.querySelector("#clear-filter-button");
    filterClearButton.onclick = clearFilter;
}

function addButtonOnclickEvents() {
    var buttons = document.querySelectorAll("#button-container button");
    buttons.forEach(button => {
        button.onclick = function () { sendRequest(button.innerText); };
    });
}

function filterVideos(event) {
    var filter = event.target.value;

    var buttons = document.querySelectorAll("#button-container button");
    buttons.forEach(button => {
        if (button.dataset.mediaFileName.includes(filter)) {
            button.style.display = "block";
        } else {
            button.style.display = "none";
        }
    });
}

function clearFilter() {
    let videoFilter = document.querySelector("#video-filter");
    videoFilter.value = "";

    var buttons = document.querySelectorAll("#button-container button");
    buttons.forEach(button => {
        button.style.display = "block";
    });
}

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

function sendRequest(mediaFileName) {
    var url = 'api/media/play/' + mediaFileName;
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", url, true);
    xmlHttp.setRequestHeader('X-API-KEY', 'my-secret-key');
    xmlHttp.send();
}