class FunVideoPlayer extends HTMLElement {

    connectedCallback() {
        this.connected = false;
        this.createConnectionButton();
        this.createVideoElement();
        this.setupConnection();        
    }

    createConnectionButton() {
        this.connectionButton = document.createElement("fun-connection-button");
        this.connectionButton.onclick = this.connectionButton_onclick;
        this.appendChild(this.connectionButton);
    }

    createVideoElement() {
        this.videoPlayer = document.createElement("VIDEO");
        this.videoPlayer.setAttribute("controls", "controls");
        this.videoPlayer.style.visibility = "hidden";
        this.appendChild(this.videoPlayer);
    }

    setupConnection() {
        let url = this.getAttribute("url");
        if (url) {
            this.connection = new signalR.HubConnectionBuilder().withUrl(url).build();
            this.connection.on('PlayVideo', this.playVideo);
            this.connection.on('StopVideo', this.stopVideo);
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
            this.videoPlayer.style.visibility = 'visible';
        }
    }

    async disconnectFromHub() {
        if (this.connection) {
            await this.connection.stop();
            this.connected = false;
            this.videoPlayer.pause();
            this.connectionButton.setDisconnected();
            this.videoPlayer.style.visibility = 'hidden';
        }
    }
}

class FunConnectionButton extends HTMLElement {

    connectedCallback() {
        this.button = document.createElement("BUTTON");
        this.button.innerText = "Connect";
        this.button.classList.add('disconnected');
        this.appendChild(this.button);
    }

    setConnected() {
        this.button.classList.add('connected');
        this.button.classList.remove('disconnected');
        this.button.innerText = 'Disconnect';
    }

    setDisconnected() {
        this.button.classList.add('disconnected');
        this.button.classList.remove('connected');
        this.button.innerText = 'Connect';
    }
}

window.customElements.define("fun-video-player", FunVideoPlayer);
window.customElements.define("fun-connection-button", FunConnectionButton);