class FunVideoList extends HTMLElement {
    

    constructor() {
        super();
        this.apiBaseUrl = "http://localhost:5000/"; // TODO: config
    }

    connectedCallback() {
        this.setupVideoTable();
    }

    async setupVideoTable() {
        this.videoTable = this.querySelector("table");
        var tableBody = this.videoTable.querySelector("tbody");

        var videos = await this.getVideos();
        videos.forEach(video => {
            var tr = document.createElement("tr");

            var nameTd = document.createElement("td");
            nameTd.innerText = video.name;

            tr.appendChild(nameTd);
            tr.appendChild(document.createElement("td"));

            tableBody.appendChild(tr);
        });
    }

    async getVideos() {
        var result = await fetch(this.apiBaseUrl + "api/video");
        return await result.json();
    }
}

window.customElements.define("fun-video-list", FunVideoList);