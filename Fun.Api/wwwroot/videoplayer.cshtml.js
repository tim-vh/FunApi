class VideoPlayer {
    constructor() {
        this.connected = false;
        this.connection = new signalR.HubConnectionBuilder().withUrl("/videohub").build();
        this.connectButton = document.getElementById('connection-btn');
        this.videoplayer = document.getElementById('videoplayer');

        this.connection.on('PlayVideo', this.play);
        this.connection.on('StopVideo', this.stop);

        var connetionButton = document.querySelector("#connection-btn");
        connetionButton.onclick = this.connection_onclick;
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

    connection_onclick = () => {
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

class VideoPlayButtons {
    constructor() {
        this.addButtonOnclickEvents();

        let videoFilter = document.querySelector("#video-filter");
        videoFilter.oninput = this.filterVideos_oninput;

        let filterClearButton = document.querySelector("#clear-filter-button");
        filterClearButton.onclick = this.clearFilter_onlick;
    }

    addButtonOnclickEvents() {
        var buttons = document.querySelectorAll("#video-button-container button");
        buttons.forEach(button => {
            button.style.backgroundImage = `url('${button.dataset.videoThumbnail}')`;
            button.onclick = this.videoButton_onclick;
        });
    }

    videoButton_onclick = (event) => {
        let url = encodeURIComponent(event.target.dataset.videoUrl);
        this.sendRequest(url);
    }

    filterVideos_oninput = (event) => {
        let filter = event.target.value;

        let buttons = document.querySelectorAll("#video-button-container button");
        buttons.forEach(button => {
            if (button.dataset.videoName.includes(filter)) {
                button.style.display = "block";
            } else {
                button.style.display = "none";
            }
        });
    }

    clearFilter_onlick = () => {
        let videoFilter = document.querySelector("#video-filter");
        videoFilter.value = "";

        let buttons = document.querySelectorAll("#video-button-container button");
        buttons.forEach(button => {
            button.style.display = "block";
        });
    }

    sendRequest(mediaFileName) {
        let url = 'api/video/play/' + mediaFileName;
        let xmlHttp = new XMLHttpRequest();
        xmlHttp.open("GET", url, true);
        xmlHttp.setRequestHeader('X-API-KEY', 'my-secret-key');
        xmlHttp.send();
    }
}

document.addEventListener("DOMContentLoaded", function () {
    new VideoPlayer();
    new VideoPlayButtons();
});