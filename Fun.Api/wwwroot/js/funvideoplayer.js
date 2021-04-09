class FunVideoPlayer extends HTMLElement {

    get url() {
        return this.getAttribute("url");
    }

    set url(value) {
        this.setAttribute("url", value);
    }

    connectedCallback() {
        this.connected = false;
        this.setupConnectionButton();
        this.createVideoElement();
        this.setupConnection();
    }

    setupConnectionButton() {
        this.connectionButton = this.querySelector("fun-connection-button");
        this.connectionButton.onclick = this.connectionButton_onclick;
    }

    createVideoElement() {
        this.videoPlayer = document.createElement("video");
        this.videoPlayer.setAttribute("controls", "controls");
        this.videoPlayer.style.visibility = "hidden";
        this.appendChild(this.videoPlayer);
    }

    setupConnection() {
        if (this.url) {
            this.connection = new signalR.HubConnectionBuilder().withUrl(this.url).build();
            this.connection.on("PlayVideo", this.playVideo);
            this.connection.on("StopVideo", this.stopVideo);
        }
    }

    connectionButton_onclick = () => {
        if (!this.connected) {
            this.connectToHub();
        } else {
            this.disconnectFromHub();
        }
    }

    playVideo = (url) => {
        if (this.videoPlayer.paused) {
            this.videoPlayer.src = url;
            this.videoPlayer.play();
        }
    }

    stopVideo = () => {
        this.videoPlayer.pause();
    }

    async connectToHub() {
        if (this.connection) {
            await this.connection.start();
            this.connected = true;
            this.connectionButton.setConnected();
            this.videoPlayer.style.visibility = "visible";
        }
    }

    async disconnectFromHub() {
        if (this.connection) {
            await this.connection.stop();
            this.connected = false;
            this.videoPlayer.pause();
            this.connectionButton.setDisconnected();
            this.videoPlayer.style.visibility = "hidden";
        }
    }
}

window.customElements.define("fun-video-player", FunVideoPlayer);