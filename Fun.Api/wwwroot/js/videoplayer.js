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

document.addEventListener("DOMContentLoaded", function () {
    new VideoPlayer();
});