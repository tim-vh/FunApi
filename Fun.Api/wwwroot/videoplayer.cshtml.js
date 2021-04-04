class VideoPlayer {
    constructor() {
        this.connected = false;
        this.connection = new signalR.HubConnectionBuilder().withUrl("/videohub").build();
        this.connectButton = document.getElementById('connection-btn');
        this.videoplayer = document.getElementById('videoplayer');

        this.connection.on('PlayVideo', this.play);
        this.connection.on('StopVideo', this.stop);

        var connetionButton = document.querySelector("#connection-btn");
        connetionButton.onclick = this.connection_click;
    }

    play = (url) => {
        if (this.videoplayer.paused) {
            this.videoplayer.src = url;
            this.videoplayer.play();
        }
    }

    stop = () => {
        this.videoplayer.pause();
    }

    connection_click = () => {
        if (!this.connected) {
            this.connect();
        } else {
            this.disconnect();
        }
    }

    async connect() {
        await this.connection.start();
        this.connected = true;

        this.connectButton.classList.add('btn-outline-danger');
        this.connectButton.classList.remove('btn-outline-success');
        this.connectButton.textContent = 'disconnect';

        this.videoplayer.style.visibility = 'visible';
    }

    async disconnect() {
        await this.connection.stop();
        this.connected = false;

        this.connectButton.classList.add('btn-outline-success');
        this.connectButton.classList.remove('btn-outline-danger');
        this.connectButton.textContent = 'connect';

        this.videoplayer.style.visibility = 'hidden';
    }
}

let videoPlayer = new VideoPlayer();

document.addEventListener("DOMContentLoaded", initialize);

function initialize() {
    addButtonOnclickEvents();

    let videoFilter = document.querySelector("#video-filter");
    videoFilter.oninput = filterVideos;

    let filterClearButton = document.querySelector("#clear-filter-button");
    filterClearButton.onclick = clearFilter;
}

function addButtonOnclickEvents() {
    var buttons = document.querySelectorAll("#video-button-container button");
    buttons.forEach(button => {
        var url = encodeURIComponent(button.dataset.videoUrl);
        button.onclick = function () { sendRequest(url); };
    });
}

function filterVideos(event) {
    var filter = event.target.value;

    var buttons = document.querySelectorAll("#button-container button");
    buttons.forEach(button => {
        if (button.dataset.videoName.includes(filter)) {
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

function sendRequest(mediaFileName) {
    var url = 'api/video/play/' + mediaFileName;
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", url, true);
    xmlHttp.setRequestHeader('X-API-KEY', 'my-secret-key');
    xmlHttp.send();
}