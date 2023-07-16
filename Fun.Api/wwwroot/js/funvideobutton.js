class FunVideoButton extends HTMLElement {

    get thumbnailUrl() { return this.getAttribute("thumbnailurl"); }
    set thumbnailUrl(value) { this.setAttribute("thumbnailurl", value); }

    get apiBaseUrl() { return this.getAttribute("apiBaseUrl"); }
    set apiBaseUrl(value) { this.setAttribute("apiBaseUrl", value); }

    get videoFileName() { return this.getAttribute("videoFileName"); }
    set videoFileName(value) { this.setAttribute("videoFileName", value); }

    get title() { return this.getAttribute("title"); }
    set title(value) { this.setAttribute("title", value); }

    connectedCallback() {
        this.apiBaseUrl = this.apiBaseUrl ?? "";

        this.button = this.querySelector("button");
        this.button.style.backgroundImage = `url('${this.thumbnailUrl}')`;
        this.button.onclick = this.videoButton_onclick;

        this.titleBar = this.querySelector("span");
        this.titleBar.textContent = this.title;
    }

    videoButton_onclick = () => {
        this.sendPlayVideoRequest(this.videoFileName);
    }

    async sendPlayVideoRequest(videoFileName) {
        let url = this.apiBaseUrl + "api/video/play/" + videoFileName;
        await fetch(url);
    }
}

window.customElements.define("fun-video-button", FunVideoButton);