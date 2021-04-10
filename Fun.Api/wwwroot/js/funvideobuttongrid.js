class FunVideoButtonGrid extends HTMLElement {
    get columns() { return this.getAttribute("columns"); }
    set columns(value) { this.setAttribute("columns", value); }

    get apiBaseUrl() { return this.getAttribute("apiBaseUrl"); }
    set apiBaseUrl(value) { this.setAttribute("apiBaseUrl", value); }

    connectedCallback() {        
        this.setupFilterBar();
        this.setupColumnsConfigurator();
        this.createButtonContainer();
        this.addVideoButtons();
        this.setupStopButton();
    }

    setupFilterBar() {
        if (!this.apiBaseUrl) this.apiBaseUrl = "";
        if (!this.columns) this.columns = 4;

        this.videoFilter = this.querySelector("input.filter");
        this.videoFilter.oninput = this.videoFilter_oninput;
        
        this.clearFilterButton = this.querySelector("button.clear");
        this.clearFilterButton.onclick = this.clearFilterButton_onlick;
    }

    setupColumnsConfigurator() {
        this.columnsCounter = this.querySelector("input.colums-counter");
        this.columnsCounter.value = this.columns;
        this.columnsCounter.oninput = this.columnsCounter_oninput;
    }

    setupStopButton() {
        this.stopButton = this.querySelector("button.stop");
        this.stopButton.onclick = this.stopButton_onclick;
    }

    createButtonContainer() {
        this.buttonContainer = document.createElement("div");
        this.buttonContainer.style.display = "grid";
        this.setGridColumns();
        this.appendChild(this.buttonContainer);
    }

    setGridColumns() {
        this.buttonContainer.style.gridTemplateColumns = `repeat(${this.columns}, calc(100% / ${this.columns}))`;
    }

    clearFilterButton_onlick = () => {
        this.videoFilter.value = "";

        this.buttonContainer.childNodes.forEach(button => {
            button.style.display = "block";
        });
    }

    stopButton_onclick = () => {
        this.stopVideo();   
    }

    columnsCounter_oninput = () => {
        this.columns = this.columnsCounter.value;
        this.setGridColumns();
    }

    videoFilter_oninput = () => {
        this.buttonContainer.childNodes.forEach(button => {
            if (button.title.includes(this.videoFilter.value)) {
                button.style.display = "block";
            } else {
                button.style.display = "none";
            }
        });
    }

    async addVideoButtons() {
        let videos = await this.getVideos();
        let buttonTemplate = this.querySelector(".button-template");
        videos.forEach(video => {
            let button = buttonTemplate.content.cloneNode(true).firstElementChild;
            button.title = video.name;
            button.thumbnailUrl = video.thumbnail;
            button.videoUrl = video.url;
            button.apiBaseUrl = this.apiBaseUrl;
            this.buttonContainer.appendChild(button);
        });
    }

    async stopVideo() {
        let url = this.apiBaseUrl + "api/video/stop";
        await fetch(url);
    }

    async getVideos() {
        var result = await fetch(this.apiBaseUrl + "api/video");
        return await result.json();
    }
}

window.customElements.define("fun-video-button-grid", FunVideoButtonGrid);